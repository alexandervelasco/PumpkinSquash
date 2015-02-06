using UnityEngine;
using System.Collections;
using System;

public enum CharacterMoveActionProperties
{
	None,
	SpeedUPS,
	Destination
}

public class CharacterMoveAction : EventTransceiverBehavior, ICharacterAction {

	//serialized data
	public GameObject source = null;
	public float defaultSpeedUPS = 1.0f;
	public string destinationTag = "Terrain";
	public float minimumDistance = 0.1f;
	public string id = String.Empty;

	private IModifiable<float> speedUPS = null;
	private Vector3 destination;
	private bool moving = false;
	private CharacterControllerAcceleration targetAcceleration = null;
	private int currentFingerId = -1;
	private CharacterActionStatus status = CharacterActionStatus.Inactive;

	private TypedValue32<ModifiableType, float> BaseSpeedUPS
	{
		set
		{			
			CallEvent(2, this.speedUPS, this);
			this.speedUPS.BaseValue = value;
		}
	}

	private TypedValue32<ModifiableType, float> FinalSpeedUPS
	{
		get
		{
			CallEvent(1, this.speedUPS, this);
			return speedUPS.FinalValue;		
		}
	}

	#region ICharacterAction implementation

	public string ID {
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
			return this.source;
		}
		set {
			this.source = value;
		}
	}

	public U GetProperty<T, U>(T propertyId) where T : IConvertible
	{
		U result = default(U);
			
		CharacterMoveActionProperties id = (CharacterMoveActionProperties)(object)propertyId;
		switch (id) 
		{
		case CharacterMoveActionProperties.Destination:
		{
			if (typeof(U).Equals(typeof(Vector3)))
				result = (U)(object)this.destination;
			break;
		}
		case CharacterMoveActionProperties.SpeedUPS:
		{
			Type propertyType = typeof(U);
			if (propertyType.Equals(typeof(float)) || propertyType.Equals(typeof(TypedValue32<ModifiableType, float>)))
			{
				result = (U)(object)this.FinalSpeedUPS;
			}
			break;
		}
		}

		return result;
	}

	public void SetProperty<T, U>(T propertyId, U propertyValue) where T : IConvertible
	{
		CharacterMoveActionProperties id = (CharacterMoveActionProperties)(object)propertyId;
		switch (id) 
		{
		case CharacterMoveActionProperties.Destination:
		{
			if (typeof(U).Equals(typeof(Vector3)))
				this.destination = (Vector3)(object)propertyValue;
			break;
		}
		case CharacterMoveActionProperties.SpeedUPS:
		{
			Type propertyType = typeof(U);
			if (propertyType.Equals(typeof(float)))
				this.BaseSpeedUPS = (float)(object)propertyValue;
			else if (propertyType.Equals(typeof(TypedValue32<ModifiableType, float>)))
				this.BaseSpeedUPS = (TypedValue32<ModifiableType, float>)(object)propertyValue;	
			break;
		}
		}
	}

	#endregion

	// Use this for initialization
	void Start () {
		destination = Vector3.zero;
		if (source == null)
			source = gameObject;
		targetAcceleration = source.GetComponent<CharacterControllerAcceleration>();
		speedUPS = new Modifiable<float>(defaultSpeedUPS);
	}
	
	// Update is called once per frame
	void Update () {
		if (moving)
		{
			Transform targetTransform = source.transform;
			Vector3 lookPoint = new Vector3(destination.x, targetTransform.position.y, destination.z);
			if (targetAcceleration != null)
			{
				if (Vector3.Distance(targetTransform.position, lookPoint) > minimumDistance)
				{
					targetTransform.LookAt(lookPoint);
					Vector3 moveVelocity = targetTransform.forward * FinalSpeedUPS * Time.deltaTime;
					targetAcceleration.absoluteVelocity += moveVelocity;
					moving = true;
					Status = CharacterActionStatus.Active;
				}
				else
				{
					Status = CharacterActionStatus.Ended;
					moving = false;
					Status = CharacterActionStatus.Inactive;
				}
			}
		}
	}

	#region implemented abstract members of GameBehavior

	public override void ReceiveEvent (string eventName, object args, object sender)
	{
		IWorldTouch[] worldTouches = args as IWorldTouch[];
		if (worldTouches != null)
		{
			IWorldTouch firstTouch = worldTouches[0];
			if (firstTouch.Phase == TouchPhase.Began &&
			    firstTouch.Collider.gameObject.CompareTag(destinationTag))
			{
				currentFingerId = firstTouch.FingerId;
			}
			else if (firstTouch.Phase == TouchPhase.Ended &&
			         currentFingerId == firstTouch.FingerId)
				currentFingerId = -1;
			if (currentFingerId == firstTouch.FingerId &&
			         firstTouch.Collider.gameObject.CompareTag(destinationTag))
			{
				destination = firstTouch.Point;
				Status = CharacterActionStatus.Started;
				if (Status != CharacterActionStatus.Cancelled)
					moving = true;
				else
				{
					Status = CharacterActionStatus.Inactive;
				}
			}
		}
	}

	#endregion
}
