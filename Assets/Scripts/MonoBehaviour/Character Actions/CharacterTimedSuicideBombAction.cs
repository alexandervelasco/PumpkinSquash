using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class CharacterTimedSuicideBombAction : EventCallerBehavior, ICharacterAction {

	//serialized data
	public GameObject source = null;
	public CharacterActionID id;
	public ModifiableID sourceAttributeID = ModifiableID.None, randomValueAttributeID = ModifiableID.None, targetAttributeID = ModifiableID.None;
	public int defaultMinimumAttributeAmount = 0, defaultMaximumAttributeAmount = 0;
	public float defaultMinimumDelayTime = 0, defaultMaximumDelayTime = 0;
	public float effectRadius = 0;
	
	private CharacterActionStatus status = CharacterActionStatus.Inactive;
	private float delayTime = 0;
	private IModifiable<int> sourceAttribute = null;
	private IModifiable<int> randomValueAttribute = null;
	private IModifiable<int> minimumAttributeAmount = null;
	private IModifiable<int> maximumAttributeAmount = null;
	private IModifiable<float> minimumDelayTime = null;
	private IModifiable<float> maximumDelayTime = null;
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
		Status = CharacterActionStatus.Inactive;
		ThreadSafeRandom r = new ThreadSafeRandom();
		if (source == null)
			source = gameObject;
		MonoBehaviour[] behaviors = source.GetComponents<MonoBehaviour>();
		IEnumerable<IModifiable<int>> attributes = behaviors.OfType<IModifiable<int>>();
		sourceAttribute = attributes.FirstOrDefault(t => t.ID == sourceAttributeID);
		randomValueAttribute = attributes.FirstOrDefault(t => t.ID == randomValueAttributeID);
		if (sourceAttribute != null && randomValueAttribute != null)
		{
			minimumAttributeAmount = new Modifiable<int>(defaultMinimumAttributeAmount);
			maximumAttributeAmount = new Modifiable<int>(defaultMaximumAttributeAmount);
			minimumAttributeAmount.ID = ModifiableID.ModifiableRandomMinimum;
			maximumAttributeAmount.ID = ModifiableID.ModifiableRandomMaximum;
			CallEvent(1, minimumAttributeAmount, this);
			CallEvent(1, maximumAttributeAmount, this);
			randomValueAttribute.BaseValue = r.Next(minimumAttributeAmount.FinalValue, maximumAttributeAmount.FinalValue);
			sourceAttribute.BaseValue = randomValueAttribute.FinalValue;
			startAttributeAmount = sourceAttribute.FinalValue;
		}
		minimumDelayTime = new Modifiable<float>(defaultMinimumDelayTime);
		maximumDelayTime = new Modifiable<float>(defaultMaximumDelayTime);
		minimumDelayTime.ID = ModifiableID.ModifiableRandomMinimum;
		maximumDelayTime.ID = ModifiableID.ModifiableRandomMaximum;
		CallEvent(1, minimumDelayTime, this);
		CallEvent(1, maximumDelayTime, this);
		delayTime = minimumDelayTime.FinalValue + ((maximumDelayTime.FinalValue - minimumDelayTime.FinalValue) * (float)r.NextDouble());
		Status = CharacterActionStatus.Started;
	}
	
	// Update is called once per frame
	public void Update () {
		if ((Status & CharacterActionStatus.Started) == CharacterActionStatus.Started)
			Status = CharacterActionStatus.Windup;
		if ((Status & CharacterActionStatus.Windup) == CharacterActionStatus.Windup)
		{
			delayTime -= Time.deltaTime;
			if (delayTime <= 0) 
			{
				Status = CharacterActionStatus.Active;
				TypedValue32<ModifiableType, int> finalAttributeAmount = sourceAttribute.FinalValue;
				if (finalAttributeAmount.Value > 0)
				{
					IModifiable<int> damage = new Modifiable<int> (startAttributeAmount.Value - finalAttributeAmount.Value);
					damage.ID = ModifiableID.Damage;
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
				sourceAttribute.BaseValue = int.MinValue;
				Status = CharacterActionStatus.Inactive;
			}
			else
				CallEvent(0, this);
		}
	}
}
