using UnityEngine;
using System;
using System.Collections.Generic;

public class GameEventCallerService {
	private IList<string> calledEvents = null;

	private GameEventCallerService(){}
	public GameEventCallerService(IList<string> calledEvents)
	{
		if (calledEvents == null)
			throw new ArgumentNullException ("calledEvents");
		else
			this.calledEvents = calledEvents;
	}

	public void CallEvent(int index, object args, object sender = null)
	{
		if (index >= 0 && index < calledEvents.Count)
			GameEventManager.CallEvent (calledEvents [index], args, sender);
	}
}
