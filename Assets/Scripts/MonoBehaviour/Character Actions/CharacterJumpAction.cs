using UnityEngine;
using System.Collections.Generic;
using System;

public class CharacterJumpAction : EventTransceiverBehavior, ICharacterAction {
	
	public GameObject source = null;
	public float speedUPS = 1.0f;
	public bool canAirJump = false;

	private CharacterControllerAcceleration targetAcceleration = null;
	private CharacterController targetController = null;
	private bool jumpStarted = false, jumpEnded = false;
	private CharacterActionStatus status = CharacterActionStatus.Inactive;

	#region ICharacterAction implementation

	public CharacterActionStatus Status {
		get {
			return this.status;
		}
		set {
			this.status = value;
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
			status = CharacterActionStatus.Active;
		}
		else if (targetController.isGrounded && !jumpEnded)
		{
			status = CharacterActionStatus.Ended;
			CallEvent(1, this);
			jumpEnded = true;
			status = CharacterActionStatus.Inactive;
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
						status = CharacterActionStatus.Started;
						CallEvent(0, this);
						if (status != CharacterActionStatus.Cancelled)
							jumpStarted = true;
						else
							status = CharacterActionStatus.Inactive;
					}
				}
			}
		}
	}

	#endregion
}
