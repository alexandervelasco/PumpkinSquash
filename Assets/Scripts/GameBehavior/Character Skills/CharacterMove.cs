using UnityEngine;
using System.Collections;

public class CharacterMove : EventCallingGameBehavior {

	public GameObject target = null;
	public float minimumDistance = 0.1f;
	public float SpeedMPS = 1.0f;
	public string destinationTag = "Terrain";

	private Vector3 destination;
	private bool moving = false;

	// Use this for initialization
	void Start () {
		destination = Vector3.zero;
		if (target == null)
			target = gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (moving)
		{
			Transform targetTransform = target.transform;
			CharacterController targetController = target.GetComponent<CharacterController>();
			if (targetController != null)
			{
				Vector3 lookPoint = new Vector3(destination.x, targetTransform.position.y, destination.z);
				targetTransform.LookAt(lookPoint);
				if (Vector3.Distance(targetTransform.position, destination) > minimumDistance)
					targetController.SimpleMove(targetTransform.forward * SpeedMPS);
				else
				{
					targetController.SimpleMove(targetTransform.forward * minimumDistance);
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
			if (firstTouch.Transform.gameObject.CompareTag(destinationTag))
			{
				destination = firstTouch.Point;
				moving = true;
			}
		}
	}

	#endregion
}
