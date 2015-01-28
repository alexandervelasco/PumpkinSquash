using UnityEngine;
using System.Collections;

public class TestBehavior : GameBehavior {

	// Use this for initialization
	void Start () {
		foreach (string registeredEvent in registeredEvents)
			GameEventManager.CallEvent(registeredEvent, null);
	}
	
	// Update is called once per frame
	void Update () {
	}

	#region implemented abstract members of GameBehavior

	public override void ReceiveEvent (string eventName, object args, object sender)
	{
		Debug.Log("Event " + eventName + " received.");
	}

	#endregion
}
