using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class CharacterTimedSuicideBombAction : EventCallerBehavior, ICharacterAction {

	//serialized data
	public GameObject source = null;
	public CharacterActionID id;
	public ModifiableID sourceAttributeID = ModifiableID.None, targetAttributeID = ModifiableID.None;
	public int minimumAttributeAmount = 0, maximumAttributeAmount = 0;
	public float minimumDelayTime = 0, maximumDelayTime = 0;
	public float effectRadius = 0;
	
	private CharacterActionStatus status = CharacterActionStatus.Inactive;
	private float delayTime = 0;
	private CharacterAttributeInt sourceAttribute = null;
	private TypedValue32<ModifiableType, int> startAttributeAmount = 0;

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
	public void Start () {
		ThreadSafeRandom r = new ThreadSafeRandom();
		if (source == null)
			source = gameObject;
		CharacterAttributeInt[] sourceAttributesInt = source.GetComponents<CharacterAttributeInt>();
		sourceAttribute = sourceAttributesInt.FirstOrDefault(attribute => attribute.ID == targetAttributeID);
		if (sourceAttribute != null && sourceAttribute.ID == sourceAttributeID)
		{
			sourceAttribute.BaseValue = r.Next(minimumAttributeAmount, maximumAttributeAmount);
			startAttributeAmount = sourceAttribute.FinalValue;
		}
		delayTime = minimumDelayTime + ((maximumDelayTime - minimumDelayTime) * (float)r.NextDouble());
		Status = CharacterActionStatus.Started;
	}
	
	// Update is called once per frame
	public void Update () {
		if (Status == CharacterActionStatus.Started)
			Status = CharacterActionStatus.Windup;
		if (Status == CharacterActionStatus.Windup)
		{
			delayTime -= Time.deltaTime;
			if (delayTime <= 0) 
			{
				Status = CharacterActionStatus.Active;
				TypedValue32<ModifiableType, int> finalAttributeAmount = sourceAttribute.FinalValue;
				if (finalAttributeAmount.Value > 0)
				{
					IModifiable<int> damage = new Modifiable<int> (new TypedValue32<ModifiableType, int> (
						ModifiableType.Damage | finalAttributeAmount.Type, startAttributeAmount.Value - finalAttributeAmount.Value));
					CallEvent (1, damage, this);
					Collider[] effectHit = Physics.OverlapSphere (source.transform.position, effectRadius);
					foreach (Collider collider in effectHit)
					{
						CharacterAttributeInt[] targetAttributesInt = collider.gameObject.GetComponents<CharacterAttributeInt>();
						CharacterAttributeInt targetAttribute = targetAttributesInt.FirstOrDefault(attribute => attribute.ID == targetAttributeID);
						if (targetAttribute != null && targetAttribute.ID == targetAttributeID)
						{
							targetAttribute.BaseValue = new TypedValue32<ModifiableType, int> (
								targetAttribute.BaseValue.Type, targetAttribute.BaseValue.Value - damage.FinalValue.Value);
						}

					}
				}
				Status = CharacterActionStatus.Ended;
				sourceAttribute.BaseValue = 0;
				Status = CharacterActionStatus.Inactive;
			}
			else
				CallEvent(0, this);
		}
	}
}
