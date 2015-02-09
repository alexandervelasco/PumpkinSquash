using UnityEngine;
using System.Collections;

public class TouchInputController : EventCallerBehavior {

	// Use this for initialization
	public override void Start () {
	
	}
	
	// Update is called once per frame
	public override void Update () {
		if (Input.touchCount > 0)
		{
			GameTouch[] gameTouches = new GameTouch[Input.touchCount];
			for (int i = 0; i < Input.touchCount; i++)
				gameTouches[i] = new GameTouch(Input.touches[i]);
			CallEvent(0, gameTouches);
		}
	}
}
