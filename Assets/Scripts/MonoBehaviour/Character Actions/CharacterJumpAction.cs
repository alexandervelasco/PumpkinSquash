using UnityEngine;
using System.Collections.Generic;
using System;

public class CharacterJumpAction : EventTransceiverBehavior, ICharacterAction {

	//serialized data
	public GameObject source = null;
	public float speedUPS = 1.0f;
	public bool canAirJump = false;
	public string id = String.Empty;
	public ulong type = 0;

	private CharacterControllerAcceleration targetAcceleration = null;
	private CharacterController targetController = null;
	private bool jumpStarted = false, jumpEnded = false;
	private CharacterActionStatus status = CharacterActionStatus.Inactive;

	#region ICharacterAction implementation

	public string ID {
		get {
			return id;
		}
		set {
			id = value;
		}
	}

	public ulong Type {
		get {
			return type;
		}
		set {
			type = value;
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

	#endregion

	// Use this for initialization
	void Start () {
		if (source == null)
			source = gameObject;
		targetAcceleration = source.GetComponent<CharacterControllerAcceleration>();
		targetController = source.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (jumpStarted) 
		{
			Vector3 jumpVelocity = source.transform.up * speedUPS;
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
		if (eventName.Equals(ReceivedEvents[0]) && args is List<IGameTouch>)
		{
			List<IGameTouch> tapData = args as List<IGameTouch>;
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
