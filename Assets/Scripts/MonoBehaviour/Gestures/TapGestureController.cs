using UnityEngine;
using System.Collections.Generic;

public class TapGestureController : EventTransceiverBehavior {

	//serialized data
	public float tapSensitivity = 0.1f;
	public float tapMovementTolerance = 0.1f;

	private Dictionary<int, float> totalTimes = new Dictionary<int, float>();
	private Dictionary<int, List<IGameTouch>> tapGestureData = new Dictionary<int, List<IGameTouch>>();
	private Dictionary<int, bool> isStationary = new Dictionary<int, bool>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region implemented abstract members of GameBehavior

	public override void ReceiveEvent (string eventName, object args, object sender)
	{
		List<IGameTouch[]> gestureData = args as List<IGameTouch[]>;
		if (gestureData != null)
		{
			IGameTouch[] currentTouches = gestureData[gestureData.Count-1];
			foreach (IGameTouch currentTouch in currentTouches)
			{
				int fingerId = currentTouch.FingerId;
				if (currentTouch.Phase == TouchPhase.Began)
				{
					totalTimes[fingerId] = 0;
					tapGestureData[fingerId] = new List<IGameTouch>();
					tapGestureData[fingerId].Add(currentTouch);
					isStationary[fingerId] = true;
				}
				else if (currentTouch.Phase != TouchPhase.Ended &&
				         currentTouch.Phase != TouchPhase.Canceled)
				{
					float distance = Vector2.Distance(currentTouch.Position, tapGestureData[fingerId][0].Position);
					if (distance < tapMovementTolerance)
					{
						totalTimes[fingerId] += Time.deltaTime;
						tapGestureData[fingerId].Add(currentTouch);
					}
					else
						isStationary[fingerId] = false;
				}
				else if (currentTouch.Phase == TouchPhase.Canceled)
				{
					isStationary[fingerId] = false;
				}
				else if (currentTouch.Phase == TouchPhase.Ended &&
				         isStationary[fingerId] &&
				         totalTimes[fingerId] <= tapSensitivity)
				{					
					tapGestureData[fingerId].Add(currentTouch);
					CallEvent(0, tapGestureData[fingerId]);
					tapGestureData.Remove(fingerId);
				}

			}
		}
	}

	#endregion
}
