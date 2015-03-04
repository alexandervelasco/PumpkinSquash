using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(RectTransform))]
public class GameTouchBlocker : EventReceiverBehavior {

	private RectTransform rectTransform = null;

	// Use this for initialization
	void Start () {
		rectTransform = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region implemented abstract members of EventReceiverBehavior

	public override void ReceiveEvent (string eventName, object args, object sender)
	{
		IList<IGameTouch> gameTouches = args as IList<IGameTouch>;
		if (gameTouches != null)
		{
			List<IGameTouch> toBeRemoved = new List<IGameTouch>();
			foreach (IGameTouch gameTouch in gameTouches)
			{
				if (RectTransformUtility.RectangleContainsScreenPoint(rectTransform, gameTouch.Position, null))
					toBeRemoved.Add(gameTouch);
			}
			foreach (IGameTouch gameTouch in toBeRemoved)
				gameTouches.Remove(gameTouch);
		}
	}

	#endregion
}
