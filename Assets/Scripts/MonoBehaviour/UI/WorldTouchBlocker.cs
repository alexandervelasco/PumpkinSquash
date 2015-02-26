using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(EventSystem))]
public class WorldTouchBlocker : EventReceiverBehavior {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region implemented abstract members of EventReceiverBehavior

	public override void ReceiveEvent (string eventName, object args, object sender)
	{
		IList<IWorldTouch> worldTouches = args as IList<IWorldTouch>;
		List<IWorldTouch> toBeRemoved = new List<IWorldTouch>();
		EventSystem eventSystem = EventSystem.current;
		if (worldTouches != null)
		{
			if (eventSystem.IsPointerOverGameObject())
			{
				worldTouches.Clear();
			}
			else
			{
				foreach (IWorldTouch worldTouch in worldTouches)
				{
					if (eventSystem.IsPointerOverGameObject(worldTouch.FingerId))
						toBeRemoved.Add(worldTouch);
				}
				foreach (IWorldTouch worldTouchToRemove in toBeRemoved)
					worldTouches.Remove(worldTouchToRemove);
			}
		}
	}

	#endregion
}
