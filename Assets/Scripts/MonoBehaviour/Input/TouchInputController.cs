using UnityEngine;
using System.Collections.Generic;

public class TouchInputController : EventCallerBehavior {

	// Use this for initialization
	public void Start () {
	
	}
	
	// Update is called once per frame
	public void Update () {
		if (Input.touchCount > 0)
		{
			List<GameTouch> gameTouches = new List<GameTouch>();
			for (int i = 0; i < Input.touchCount; i++)
				gameTouches.Add (new GameTouch(Input.touches[i]));
			CallEvent(0, gameTouches);
			CallEvent(1, gameTouches.ToArray());
		}
	}
}
