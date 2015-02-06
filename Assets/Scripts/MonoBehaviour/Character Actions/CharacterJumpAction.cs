using UnityEngine;
using System.Collections.Generic;
using System;

public enum CharacterJumpActionProperties
{
	None,
	SpeedUPS
}

public class CharacterJumpAction : EventTransceiverBehavior, ICharacterAction {

	//serialized data
	public GameObject source = null;
	public float defaultSpeedUPS = 1.0f;
	public bool canAirJump = false;
	public string id = String.Empty;

	private IModifiable<float> speedUPS = null;
	private CharacterControllerAcceleration targetAcceleration = null;
	private CharacterController targetController = null;
	private bool jumpStarted = false, jumpEnded = false;
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
		
		CharacterJumpActionProperties id = (CharacterJumpActionProperties)(object)propertyId;
		switch (id) 
		{
		case CharacterJumpActionProperties.SpeedUPS:
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
		CharacterJumpActionProperties id = (CharacterJumpActionProperties)(object)propertyId;
		switch (id) 
		{
		case CharacterJumpActionProperties.SpeedUPS:
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
		if (source == null)
			source = gameObject;
		targetAcceleration = source.GetComponent<CharacterControllerAcceleration>();
		targetController = source.GetComponent<CharacterController>();
		speedUPS = new Modifiable<float>(defaultSpeedUPS);
	}
	
	// Update is called once per frame
	void Update () {
		if (jumpStarted) 
		{
			Vector3 jumpVelocity = source.transform.up * FinalSpeedUPS;
			targetAcceleration.currentVelocity = jumpVelocity;
			jumpStarted = false;
		}
		if (!targetController.isGrounded)
		{
			jumpEnded = false;
			Status = CharacterActionStatus.Active;
		}
		else if (targetController.isGrounded && !jumpEnded)
		{
			Status = CharacterActionStatus.Ended;
			jumpEnded = true;
			Status = CharacterActionStatus.Inactive;
		}
	}

	#region implemented abstract members of GameBehavior

	public override void ReceiveEvent (string eventName, object args, object sender)
	{
		List<IGameTouch> tapData = args as List<IGameTouch>;
		if (eventName.Equals(ReceivedEvents[0]) && tapData != null)
		{
			if (tapData[tapData.Count-1] is IGameRaycastHit)
			{
				IGameRaycastHit tapRelease = tapData[tapData.Count-1] as IGameRaycastHit;
				if (tapRelease.Collider != null)
				{
					if (tapRelease.Collider.gameObject.Equals(source) && 
					    targetController != null &&
					    (targetController.isGrounded || canAirJump))
					{
						Status = CharacterActionStatus.Started;
						if (Status != CharacterActionStatus.Cancelled)
							jumpStarted = true;
						else
						{
							Status = CharacterActionStatus.Inactive;
						}
					}
				}
			}
		}
	}

	#endregion
}
