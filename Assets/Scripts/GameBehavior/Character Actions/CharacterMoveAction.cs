using UnityEngine;
using System.Collections;
using System;

public class CharacterMoveAction : EventCallingGameBehavior, ICharacterAction {

	public GameObject source = null;
	public float speedUPS = 1.0f;
	public string destinationTag = "Terrain";
	public float minimumDistance = 0.1f;

	private Vector3 destination;
	private bool moving = false;
	private CharacterControllerAcceleration targetAcceleration = null;
	private int currentFingerId = -1;
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
		destination = Vector3.zero;
		if (source == null)
			source = gameObject;
		targetAcceleration = source.GetComponent<CharacterControllerAcceleration>();
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
					Vector3 moveVelocity = targetTransform.forward * speedUPS * Time.deltaTime;
					targetAcceleration.absoluteVelocity += moveVelocity;
					moving = true;
					status = CharacterActionStatus.Active;
				}
				else
				{
					status = CharacterActionStatus.Ended;
					CallEvent(1, this);
					moving = false;
					status = CharacterActionStatus.Inactive;
				}
			}
		}
	}

	#region implemented abstract members of GameBehavior

	public override void ReceiveEvent (string eventName, object args, object sender)
	{
		if (args is IWorldTouch[])
		{
			IWorldTouch[] worldTouches = args as IWorldTouch[];
			IWorldTouch firstTouch = worldTouches[0];
			if (firstTouch.Collider != null)
			{
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
					status = CharacterActionStatus.Started;
					CallEvent(0, this);
					if (status != CharacterActionStatus.Cancelled)
						moving = true;
					else
						status = CharacterActionStatus.Inactive;
				}
			}
		}
	}

	#endregion
}
