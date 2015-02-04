using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public abstract class EventTransceiverBehavior : MonoBehaviour, IGameEventCaller, IGameEventReceiver {

	//serialized data
	public List<string> registeredEvents;
	public List<string> calledEvents;
	
	private bool awakeDone = false, destroyDone = false;
	private GameEventReceiverService eventReceiver = null;
	private GameEventCallerService eventCaller = null;
	
	public IList<string> ReceivedEvents {
		get { return this.registeredEvents.ToList();}
	}

	public IList<string> CalledEvents {
		get {
			return calledEvents;
		}
	}
	
	public virtual void Awake() {
		if (!awakeDone) {
			eventReceiver = new GameEventReceiverService(registeredEvents, ReceiveEvent);
			eventCaller = new GameEventCallerService(calledEvents);
			awakeDone = true;
		}
	}
	
	public virtual void OnDestroy() {
		if (!destroyDone) {
			eventReceiver.Dispose();
			destroyDone = true;
		}
	}
	
	public abstract void ReceiveEvent (string eventName, object args, object sender);

	protected virtual void CallEvent(int eventIndex, object args, object sender = null)
	{
		eventCaller.CallEvent(eventIndex, args, sender);
	}
}
