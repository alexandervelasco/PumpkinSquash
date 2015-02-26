using UnityEngine;
using System.Collections.Generic;

public class TouchGestureController : EventTransceiverBehavior {

	private List<IGameTouch[]> gestureData = null;

	// Use this for initialization
	public void Start () {
	
	}
	
	// Update is called once per frame
	public void Update () {
	
	}

	#region implemented abstract members of GameBehavior

	public override void ReceiveEvent (string eventName, object args, object sender)
	{
		IGameTouch[] gameTouches = args as IGameTouch[];
		if (gameTouches != null && gameTouches.Length > 0) 
		{
			bool allTouchesStarted = true;
			foreach (IGameTouch gameTouch in gameTouches)
				allTouchesStarted = allTouchesStarted && (gameTouch.Phase == TouchPhase.Began);
			if (allTouchesStarted || gestureData == null)
				gestureData = new List<IGameTouch[]>();
			gestureData.Add(gameTouches);
			CallEvent(0, gestureData);
		}
	}

	#endregion
}
