using UnityEngine;
using System.Collections;

public class RaycastBlocker : EventReceiverBehavior {

	//serialized data
	public float moveHeight = 0.0f;
	public float hideHeight = 0.0f;

	// Use this for initialization
	void Start () {
		Vector3 localPosition = transform.localPosition;
		localPosition.y = moveHeight;
		transform.localPosition = localPosition;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region implemented abstract members of EventReceiverBehavior

	public override void ReceiveEvent (string eventName, object args, object sender)
	{
		if (args is bool)
		{
			bool paused = (bool)args;
			Vector3 localPosition = transform.localPosition;
			localPosition.y = paused ? hideHeight : moveHeight;
			transform.localPosition = localPosition;
		}
	}

	#endregion
}
