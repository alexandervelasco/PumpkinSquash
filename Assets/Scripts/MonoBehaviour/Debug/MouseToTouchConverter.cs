using UnityEngine;
using System.Collections.Generic;

public class MouseToTouchConverter : EventCallerBehavior {

	public int mouseButtonId = 0;

	private Vector3 previousMousePosition = Vector3.zero;

	// Use this for initialization
	public void Start () {
		if (mouseButtonId < 0 || mouseButtonId > 2)
			mouseButtonId = 0;
	}
	
	// Update is called once per frame
	public void Update () {
		if (Input.GetMouseButton (mouseButtonId) ||
			Input.GetMouseButtonDown (mouseButtonId) ||
			Input.GetMouseButtonUp (mouseButtonId)) 
		{
			GameTouch mouseTouch = new GameTouch();
			mouseTouch.Position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			mouseTouch.FingerId = mouseButtonId;

			if (Input.GetMouseButtonDown (mouseButtonId))
			{
				mouseTouch.Phase = TouchPhase.Began;
			}
			else if (Input.GetMouseButton (mouseButtonId))
			{
				mouseTouch.DeltaTime = Time.deltaTime;
				if (Input.mousePosition == previousMousePosition)
					mouseTouch.Phase = TouchPhase.Stationary;
				else
					mouseTouch.Phase = TouchPhase.Moved;
			}
			else if (Input.GetMouseButtonUp (mouseButtonId))
			{
				mouseTouch.Phase = TouchPhase.Ended;
			}
			List<IGameTouch> gameTouches = new List<IGameTouch>{mouseTouch};
			CallEvent(0, gameTouches);
			if (gameTouches.Count > 0)
				CallEvent(1, gameTouches.ToArray());
		}

		previousMousePosition = Input.mousePosition;
	}
}
