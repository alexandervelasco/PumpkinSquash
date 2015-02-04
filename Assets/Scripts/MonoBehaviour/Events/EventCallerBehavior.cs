using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EventCallerBehavior : MonoBehaviour, IGameEventCaller {

	//serialized data
	public List<string> calledEvents;
	
	private bool awakeDone = false;
	private GameEventCallerService eventCaller = null;
	
	public IList<string> CalledEvents {
		get {
			return calledEvents;
		}
	}
	
	public virtual void Awake() {
		if (!awakeDone) {
			eventCaller = new GameEventCallerService(calledEvents);
			awakeDone = true;
		}
	}

	protected virtual void CallEvent(int eventIndex, object args, object sender = null)
	{
		eventCaller.CallEvent(eventIndex, args, sender);
	}
}
