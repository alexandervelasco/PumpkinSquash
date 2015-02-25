using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CharacterAttributeIntOnKillAction : EventTransceiverBehavior, ICharacterAction, ITargeted<GameObject> {

	//serialized data
	public GameObject source = null;
	public float defaultAttributeMultiplier = 0;
	public CharacterActionID triggerActionID = CharacterActionID.None;
	public CharacterActionID deathActionID = CharacterActionID.None;
	public ModifiableID sourceAttributeID = ModifiableID.None;
	public ModifiableID targetAttributeID = ModifiableID.None;
	public CharacterActionID id;
	
	private IModifiable<float> attributeMultiplier = null;
	private ICharacterAction triggerAction = null;
	private CharacterActionStatus status = CharacterActionStatus.Inactive;
	private List<GameObject> targets = null;

	private TypedValue32<ModifiableType, float> BaseAttributeMultiplier
	{
		set { this.attributeMultiplier.BaseValue = value; CallEvent (2, this.attributeMultiplier, this); }
	}
	private TypedValue32<ModifiableType, float> FinalAttributeMultiplier
	{
		get { CallEvent (1, this.attributeMultiplier, this); return this.attributeMultiplier.FinalValue; }
	}

	#region ICharacterAction implementation
	public U GetProperty<T, U> (T propertyId) where T : System.IConvertible
	{
		throw new System.NotImplementedException ();
	}
	public void SetProperty<T, U> (T propertyId, U propertyValue) where T : System.IConvertible
	{
		throw new System.NotImplementedException ();
	}

	public CharacterActionID ID {
		get {
			return id;
		}
		set {
			id = value;
		}
	}
	
	public CharacterActionStatus Status {
		get {
			return this.status;
		}
		set {
			if (this.status != value)
			{
				this.status = value;
				CallEvent(0, this);
			}
		}
	}
	
	public GameObject Source {
		get {
			if (source == null)
				source = gameObject;
			return this.source;
		}
		set {
			this.source = value;
		}
	}

	#endregion

	#region ITargeted implementation

	public List<GameObject> Targets {
		get {
			return targets;
		}
	}

	#endregion

	// Use this for initialization
	void Start () {
		if (source == null)
			source = gameObject;
		attributeMultiplier = new Modifiable<float>(defaultAttributeMultiplier);
		Status = CharacterActionStatus.Inactive;
		targets = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region implemented abstract members of EventTransceiverBehavior

	public override void ReceiveEvent (string eventName, object args, object sender)
	{
		ICharacterAction action = args as ICharacterAction;
		if (action != null)
		{
			if (action.ID == triggerActionID && action.Source == Source)
			{
				if ((action.Status & CharacterActionStatus.Active) == CharacterActionStatus.Active)
					triggerAction = action;
				else
					triggerAction = null;
			}
			else if (action.ID == deathActionID && (action.Status & CharacterActionStatus.Active) == CharacterActionStatus.Active)
			{
				ITargeted<GameObject> triggerTargets = triggerAction as ITargeted<GameObject>;
				if (triggerTargets != null && triggerTargets.Targets.Contains(action.Source))
				{
					Status = CharacterActionStatus.Started;
					if ((Status & CharacterActionStatus.Cancelled) != CharacterActionStatus.Cancelled)
					{
						Status = CharacterActionStatus.Active;
						Targets.Clear();
						Targets.AddRange(triggerTargets.Targets);
						CharacterAttributeInt[] sourceAttributesInt = source.GetComponents<CharacterAttributeInt>();
						CharacterAttributeInt sourceAttribute = sourceAttributesInt.FirstOrDefault(attribute => attribute.ID == sourceAttributeID);
						if (sourceAttribute != null)
						{
							float totalAttribute = 0;
							foreach (GameObject target in Targets)
							{
								CharacterAttributeIntClampModifier maximumAttributeModifier = target.GetComponents<MonoBehaviour>()
									.OfType<CharacterAttributeIntClampModifier>()
										.FirstOrDefault(t => t.ID == ModifiableID.AttributeClampMaximum && t.TargetModifiableID == targetAttributeID);
								if (maximumAttributeModifier != null)
								{
									float finalAttributeMultiplier = FinalAttributeMultiplier;
									totalAttribute = totalAttribute + ((float)maximumAttributeModifier.FinalValue.Value * finalAttributeMultiplier);
								}
							}
							sourceAttribute.BaseValue += (int)totalAttribute;
						}
						Status = CharacterActionStatus.Ended;
						Status = CharacterActionStatus.Inactive;
					}
				}
			}
		}
	}

	#endregion
}
