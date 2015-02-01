using UnityEngine;
using System.Collections.Generic;
using System;

public class CharacterJumpSkill : EventCallingGameBehavior {
	
	public GameObject target = null;
	public float speedUPS = 1.0f;
	public bool canAirJump = false;

	private CharacterControllerAcceleration targetAcceleration = null;
	private CharacterController targetController = null;
	private bool jumpStarted = false, jumpEnded = false;

	// Use this for initialization
	void Start () {
		if (target == null)
			target = gameObject;
		targetAcceleration = target.GetComponent<CharacterControllerAcceleration>();
		targetController = target.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (jumpStarted) 
		{
			Vector3 jumpVelocity = target.transform.up * speedUPS;
			targetAcceleration.currentVelocity = jumpVelocity;
			jumpStarted = false;
		}
		if (!targetController.isGrounded)
			jumpEnded = false;
		else if (targetController.isGrounded && !jumpEnded)
		{			
			CallEvent(1, null);
			jumpEnded = true;
		}
	}

	#region implemented abstract members of GameBehavior

	public override void ReceiveEvent (string eventName, object args, object sender)
	{
		if (eventName.Equals(Events[0]) && args is List<IGameTouch>)
		{
			List<IGameTouch> tapData = args as List<IGameTouch>;
			if (tapData[tapData.Count-1] is WorldTouch)
			{
				WorldTouch tapRelease = tapData[tapData.Count-1] as WorldTouch;
				if (tapRelease.Collider.gameObject.Equals(target) && 
				    targetController != null &&
				    (targetController.isGrounded || canAirJump))
				{
					jumpStarted = true;
					CallEvent(0, null);
				}
			}
		}
	}

	#endregion
}
