using UnityEngine;
using System.Collections.Generic;

public abstract class EventCallingGameBehavior : GameBehavior {

	public List<string> calledEvents;

	protected virtual void CallEvent(int eventIndex, object args, object sender = null)
	{
		if (eventIndex >= 0 && eventIndex < calledEvents.Count)
			GameEventManager.CallEvent(calledEvents[eventIndex], args, sender);
	}
}
