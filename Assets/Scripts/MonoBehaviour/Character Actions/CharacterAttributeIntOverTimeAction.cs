using UnityEngine;
using System.Collections;

public class CharacterAttributeIntOverTimeAction : EventCallerBehavior, ICharacterAction {

	//serialized data
	public GameObject source = null;
	public CharacterActionID id;
	public ModifiableID sourceAttributeID = ModifiableID.None;
	public int amountPerTick = 0;
	public float tickInterval = 0;
	
	private CharacterActionStatus status = CharacterActionStatus.Inactive;
	private IModifiable<int> modifiableAmount = null;
	private float currentTick = 0;
	private CharacterAttributeInt sourceAttribute = null;
	
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
		sourceAttribute = source.GetComponent<CharacterAttributeInt>();
		modifiableAmount = new Modifiable<int>(amountPerTick);
		if (sourceAttribute != null && sourceAttribute.ID == sourceAttributeID)
		{
			currentTick = tickInterval;
			Status = CharacterActionStatus.Started;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Status == CharacterActionStatus.Started)
				Status = CharacterActionStatus.Windup;
		if (Status == CharacterActionStatus.Windup) 
		{
			currentTick -= Time.deltaTime;
			if (currentTick <= 0) {
				Status = CharacterActionStatus.Active;
				CallEvent (1, modifiableAmount, this);
				TypedValue32<ModifiableType, int> finalAmount = modifiableAmount.FinalValue;
				sourceAttribute.BaseValue = new TypedValue32<ModifiableType, int>(
					sourceAttribute.BaseValue.Type | finalAmount.Type, 
					sourceAttribute.BaseValue.Value + finalAmount.Value);
				currentTick = tickInterval;
				Status = CharacterActionStatus.Windup;
			} 
			else
				CallEvent (0, this);
		} 
		else 
		{
			Status = CharacterActionStatus.Ended;
			Status = CharacterActionStatus.Inactive;
		}
	}
}
