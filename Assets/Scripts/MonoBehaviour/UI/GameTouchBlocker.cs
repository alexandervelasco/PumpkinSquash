using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(RectTransform))]
public class GameTouchBlocker : EventReceiverBehavior {

	private RectTransform rectTransform = null;
	private Canvas canvas = null;
	private CanvasScaler scaler = null;

	// Use this for initialization
	void Start () {
		rectTransform = GetComponent<RectTransform>();
		canvas = GetComponentInParent<Canvas>();
		scaler = canvas.GetComponent<CanvasScaler>();
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
				if (canvas != null && ScreenPointInRectTransform(gameTouch.Position, rectTransform))
					toBeRemoved.Add(gameTouch);
			}
			foreach (IGameTouch gameTouch in toBeRemoved)
				gameTouches.Remove(gameTouch);
		}
	}

	#endregion

	private bool ScreenPointInRectTransform(Vector2 point, RectTransform rectTransform)
	{
		Rect absoluteRect = new Rect(rectTransform.rect);
		absoluteRect.center = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, rectTransform.transform.position);
		if (scaler != null)
		{
			Vector2 resolutionScale = new Vector2((float)Screen.width / scaler.referenceResolution.x,
			                                      (float)Screen.height / scaler.referenceResolution.y);
			absoluteRect.width *= resolutionScale.x;
			absoluteRect.height *= resolutionScale.y;
		}
		Debug.Log(this);
		Debug.Log(absoluteRect);
		Debug.Log(point);

		return absoluteRect.Contains(point);
	}
}
