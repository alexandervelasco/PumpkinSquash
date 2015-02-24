using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public enum CharacterAttackActionProperties
{
	None,
	Damage,
	WindupTime,
	RecoveryTime,
	Range
}

public class CharacterAttackAction : EventTransceiverBehavior, ICharacterAction, ITargeted<GameObject> {
	
	//serialized data
	public GameObject source = null;
	public int defaultDamage = 0;
	public float defaultWindupTime = 0;
	public float defaultRecoveryTime = 0;
	public float defaultRange = 0;
	public CharacterActionID id;
	public ModifiableID targetAttributeID = ModifiableID.None;
	public Vector3 originOffset = Vector3.zero;
	
	private IModifiable<int> damage = null;
	private IModifiable<float> windupTime = null;
	private IModifiable<float> recoveryTime = null;
	private IModifiable<float> range = null;
	private CharacterActionStatus status = CharacterActionStatus.Inactive;
	private bool attacking = false;
	private float currentTime = 0;
	private List<GameObject> targets = null;

	private TypedValue32<ModifiableType, int> BaseDamage
	{
		set { CallEvent (2, this.damage, this); this.damage.BaseValue = value; }
	}
	private TypedValue32<ModifiableType, int> FinalDamage
	{
		get { CallEvent (1, this.damage, this); return this.damage.FinalValue; }
	}

	private TypedValue32<ModifiableType, float> BaseWindupTime
	{
		set { CallEvent (2, this.windupTime, this); this.windupTime.BaseValue = value; }
	}
	private TypedValue32<ModifiableType, float> FinalWindupTime
	{
		get { CallEvent (1, this.windupTime, this); return this.windupTime.FinalValue; }
	}

	private TypedValue32<ModifiableType, float> BaseRecoveryTime
	{
		set { CallEvent (2, this.recoveryTime, this); this.recoveryTime.BaseValue = value; }
	}
	private TypedValue32<ModifiableType, float> FinalRecoveryTime
	{
		get { CallEvent (1, this.recoveryTime, this); return this.recoveryTime.FinalValue; }
	}

	private TypedValue32<ModifiableType, float> BaseRange
	{
		set { CallEvent (2, this.range, this); this.range.BaseValue = value; }
	}
	private TypedValue32<ModifiableType, float> FinalRange
	{
		get { CallEvent (1, this.range, this); return this.range.FinalValue; }
	}

	#region ICharacterAction implementation


	public U GetProperty<T, U> (T propertyId) where T : System.IConvertible
	{
		U result = default(U);
		
		CharacterAttackActionProperties id = (CharacterAttackActionProperties)(object)propertyId;
		Type propertyType = typeof(U);
		switch (id) 
		{
		case CharacterAttackActionProperties.Damage:
		{
			if (propertyType.Equals(typeof(int)) || propertyType.Equals(typeof(TypedValue32<ModifiableType, int>)))
			{
				result = (U)(object)this.FinalDamage;
			}
			break;
		}
		case CharacterAttackActionProperties.WindupTime:
		{
			if (propertyType.Equals(typeof(float)) || propertyType.Equals(typeof(TypedValue32<ModifiableType, float>)))
			{
				result = (U)(object)this.FinalWindupTime;
			}
			break;
		}
		case CharacterAttackActionProperties.RecoveryTime:
		{
			if (propertyType.Equals(typeof(float)) || propertyType.Equals(typeof(TypedValue32<ModifiableType, float>)))
			{
				result = (U)(object)this.FinalRecoveryTime;
			}
			break;
		}
		case CharacterAttackActionProperties.Range:
		{
			if (propertyType.Equals(typeof(float)) || propertyType.Equals(typeof(TypedValue32<ModifiableType, float>)))
			{
				result = (U)(object)this.FinalRange;
			}
			break;
		}
		}
		
		return result;
	}


	public void SetProperty<T, U> (T propertyId, U propertyValue) where T : System.IConvertible
	{
		CharacterAttackActionProperties id = (CharacterAttackActionProperties)(object)propertyId;
		Type propertyType = typeof(U);
		switch (id) 
		{
		case CharacterAttackActionProperties.Damage:
		{
			if (propertyType.Equals(typeof(int)))
				this.BaseDamage = (int)(object)propertyValue;
			else if (propertyType.Equals(typeof(TypedValue32<ModifiableType, int>)))
				this.BaseDamage = (TypedValue32<ModifiableType, int>)(object)propertyValue;	
			break;
		}
		case CharacterAttackActionProperties.WindupTime:
		{
			if (propertyType.Equals(typeof(float)))
				this.BaseWindupTime = (float)(object)propertyValue;
			else if (propertyType.Equals(typeof(TypedValue32<ModifiableType, float>)))
				this.BaseWindupTime = (TypedValue32<ModifiableType, float>)(object)propertyValue;	
			break;
		}
		case CharacterAttackActionProperties.RecoveryTime:
		{
			if (propertyType.Equals(typeof(float)))
				this.BaseRecoveryTime = (float)(object)propertyValue;
			else if (propertyType.Equals(typeof(TypedValue32<ModifiableType, float>)))
				this.BaseRecoveryTime = (TypedValue32<ModifiableType, float>)(object)propertyValue;	
			break;
		}
		case CharacterAttackActionProperties.Range:
		{
			if (propertyType.Equals(typeof(float)))
				this.BaseRange = (float)(object)propertyValue;
			else if (propertyType.Equals(typeof(TypedValue32<ModifiableType, float>)))
				this.BaseRange = (TypedValue32<ModifiableType, float>)(object)propertyValue;	
			break;
		}
		}
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
			if (targets == null)
				targets = new List<GameObject>();
			return targets;
		}
	}

	#endregion

	// Use this for initialization
	void Start () {
		if (source == null)
			source = gameObject;
		damage = new Modifiable<int>(defaultDamage);
		windupTime = new Modifiable<float>(defaultWindupTime);
		recoveryTime = new Modifiable<float>(defaultRecoveryTime);
		range = new Modifiable<float>(defaultRange);
		Status = CharacterActionStatus.Inactive;
	}
	
	// Update is called once per frame
	void Update () {
		if (attacking)
		{
			GameObject target = null;
			if (Targets.Count > 0)
				target = Targets[0];
			if ((Status & CharacterActionStatus.Started) == CharacterActionStatus.Started)
			{
				currentTime = 0;
				if (target != null)
					Source.transform.LookAt(new Vector3(target.transform.position.x, Source.transform.position.y, target.transform.position.z));
				Status = CharacterActionStatus.Windup;
			}
			else if ((Status & CharacterActionStatus.Windup) == CharacterActionStatus.Windup)
			{
				currentTime += Time.deltaTime;
				if (currentTime >= FinalWindupTime)
				{
					Status = CharacterActionStatus.Active;
					currentTime = 0;
					RaycastHit hitCheck;
					if (Physics.Raycast(Source.transform.position + originOffset, Source.transform.forward, out hitCheck, FinalRange, 1 << Source.layer))
					{
						if (hitCheck.collider.gameObject == target)
						{
							CharacterAttributeInt[] targetAttributesInt = target.GetComponents<CharacterAttributeInt>();
							CharacterAttributeInt targetAttribute = targetAttributesInt.FirstOrDefault(attribute => attribute.ID == targetAttributeID);
							if (targetAttribute != null)
								targetAttribute.BaseValue -= FinalDamage;
						}
					}
					Status = CharacterActionStatus.Recovery;
				}
			}
			else if ((Status & CharacterActionStatus.Recovery) == CharacterActionStatus.Recovery)
			{
				currentTime += Time.deltaTime;
				if (currentTime >= FinalRecoveryTime)
				{
					attacking = false;
					Targets.Clear();
					Status = CharacterActionStatus.Ended;
					Status = CharacterActionStatus.Inactive;
				}
			}
		}
	}

	#region implemented abstract members of EventTransceiverBehavior

	public override void ReceiveEvent (string eventName, object args, object sender)
	{
		List<IGameTouch> tapData = args as List<IGameTouch>;
		ICharacterAction characterAction = args as ICharacterAction;
		if (tapData != null)
		{
			IGameRaycastHit tapRelease = tapData[tapData.Count-1] as IGameRaycastHit;
			if (tapRelease != null && tapRelease.Collider != null)
			{
				GameObject target = tapRelease.Collider.gameObject;
				int targetLayerMask = 1 << target.layer;
				int sourceLayerMask = 1 << Source.layer;
				if (!target.Equals(Source) && (targetLayerMask & sourceLayerMask) == sourceLayerMask &&
				    (Status & (CharacterActionStatus.Started | CharacterActionStatus.Active | CharacterActionStatus.Windup)) == CharacterActionStatus.None)
				{
					float distance = Vector3.Distance(Source.transform.position + originOffset, target.transform.position);
					if (distance <= FinalRange)
					{
						Status = CharacterActionStatus.Started;
						if ((Status & CharacterActionStatus.Cancelled) != CharacterActionStatus.Cancelled)
						{
							Targets.Clear();
							Targets.Add(target);
							attacking = true;
						}
						else
						{
							attacking = false;
							Targets.Clear();
							target = null;
							Status = CharacterActionStatus.Inactive;
						}
					}
					else
						CallEvent(3, target.transform.position);
				}
			}
		}
		else if (characterAction != null && characterAction != this && 
		         characterAction.Status == CharacterActionStatus.Started && Status != CharacterActionStatus.Inactive)
		{			
			attacking = false;
			Targets.Clear();
			Status = CharacterActionStatus.Ended;
			Status = CharacterActionStatus.Inactive;
		}
	}

	#endregion
}
