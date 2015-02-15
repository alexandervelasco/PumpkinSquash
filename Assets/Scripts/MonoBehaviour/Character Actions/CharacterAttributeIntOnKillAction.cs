using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CharacterAttributeIntOnKillAction : EventTransceiverBehavior, ICharacterAction {

	//serialized data
	public GameObject source = null;
	public int defaultAttributeAmount = 0;
	public CharacterActionID triggerActionID = CharacterActionID.None;
	public CharacterActionID deathActionID = CharacterActionID.None;
	public ModifiableID sourceAttributeID = ModifiableID.None;
	public CharacterActionID id;
	
	private IModifiable<int> attributeAmount = null;
	private ICharacterAction triggerAction = null;
	private CharacterActionStatus status = CharacterActionStatus.Inactive;
	private bool deathActive = false;

	private TypedValue32<ModifiableType, int> BaseAttributeAmount
	{
		set { CallEvent (2, this.attributeAmount, this); this.attributeAmount.BaseValue = value; }
	}
	private TypedValue32<ModifiableType, int> FinalAttributeAmount
	{
		get { CallEvent (1, this.attributeAmount, this); return this.attributeAmount.FinalValue; }
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

	// Use this for initialization
	void Start () {
		if (source == null)
			source = gameObject;
		attributeAmount = new Modifiable<int>(defaultAttributeAmount);
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
				if (action.Status == CharacterActionStatus.Active)
					triggerAction = action;
				else
					triggerAction = null;
			}
			else if (action.ID == deathActionID)
			{
				if (!deathActive && action.Status != CharacterActionStatus.Active)
				{
					ITargeted<GameObject> triggerTargets = triggerAction as ITargeted<GameObject>;
					if (triggerTargets != null && triggerTargets.Targets.Contains(action.Source))
					{
						Status = CharacterActionStatus.Started;
						if ((Status & CharacterActionStatus.Cancelled) != CharacterActionStatus.Cancelled)
						{
							Status = CharacterActionStatus.Active;
							deathActive = true;
							CharacterAttributeInt[] sourceAttributesInt = source.GetComponents<CharacterAttributeInt>();
							CharacterAttributeInt sourceAttribute = sourceAttributesInt.FirstOrDefault(attribute => attribute.ID == sourceAttributeID);
							if (sourceAttribute != null)
								sourceAttribute.BaseValue += FinalAttributeAmount;
						}
					}
				}
				else if (deathActive && action.Status != CharacterActionStatus.Active)
					deathActive = false;
			}
		}
	}

	#endregion
}
