using UnityEngine;
using System.Collections;

public class RaycastBlocker : EventReceiverBehavior {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region implemented abstract members of EventReceiverBehavior

	public override void ReceiveEvent (string eventName, object args, object sender)
	{
		if (args is bool)
		{
			bool paused = (bool) args;
			gameObject.transform.localScale = paused ? Vector3.one : Vector3.zero;
		}
	}

	#endregion
}
