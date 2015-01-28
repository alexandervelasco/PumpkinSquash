using UnityEngine;
using System.Collections;

public class TouchInputController : EventCallingGameBehavior {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0)
		{
			GameTouch[] gameTouches = new GameTouch[Input.touchCount];
			for (int i = 0; i < Input.touchCount; i++)
				gameTouches[i] = new GameTouch(Input.touches[i]);
			CallEvent(0, gameTouches);
		}
	}

	#region implemented abstract members of GameBehavior

	public override void ReceiveEvent (string eventName, object args, object sender)
	{
		throw new System.NotImplementedException ();
	}

	#endregion
}
