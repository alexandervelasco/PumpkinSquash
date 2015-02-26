using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class TestBehavior : EventReceiverBehavior {

	public UnityEvent testEvent;

	// Use this for initialization
	public void Start () {
		foreach (string registeredEvent in registeredEvents)
			GameEventManager.CallEvent(registeredEvent, null);
	}
	
	// Update is called once per frame
	public void Update () {
	}

	#region implemented abstract members of GameBehavior

	public override void ReceiveEvent (string eventName, object args, object sender)
	{
		Debug.Log("Event " + eventName + " received.");
	}

	#endregion
}
