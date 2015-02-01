using UnityEngine;
using System.Collections;
using System;

public class CharacterMoveSkill : EventCallingGameBehavior {

	public GameObject target = null;
	public float speedUPS = 1.0f;
	public string destinationTag = "Terrain";
	public float minimumDistance = 0.1f;

	private Vector3 destination;
	private bool moving = false;
	private CharacterControllerAcceleration targetAcceleration = null;
	private int currentFingerId = -1;

	// Use this for initialization
	void Start () {
		destination = Vector3.zero;
		if (target == null)
			target = gameObject;
		targetAcceleration = target.GetComponent<CharacterControllerAcceleration>();
	}
	
	// Update is called once per frame
	void Update () {
		if (moving)
		{
			Transform targetTransform = target.transform;
			Vector3 lookPoint = new Vector3(destination.x, targetTransform.position.y, destination.z);
			if (targetAcceleration != null)
			{
				if (Vector3.Distance(targetTransform.position, lookPoint) > minimumDistance)
				{
					targetTransform.LookAt(lookPoint);
					Vector3 moveVelocity = targetTransform.forward * speedUPS * Time.deltaTime;
					targetAcceleration.absoluteVelocity += moveVelocity;
					moving = true;
				}
				else
				{
					CallEvent(1, null);
					moving = false;
				}
			}
		}
	}

	#region implemented abstract members of GameBehavior

	public override void ReceiveEvent (string eventName, object args, object sender)
	{
		if (args is WorldTouch[])
		{
			WorldTouch[] worldTouches = args as WorldTouch[];
			WorldTouch firstTouch = worldTouches[0];
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
				CallEvent(0, null);
				moving = true;
			}
		}
	}

	#endregion
}
