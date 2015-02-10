using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public abstract class EventReceiverBehavior : MonoBehaviour, IGameEventReceiver {

	//serialized data
	public List<string> registeredEvents;

	private bool awakeDone = false, destroyDone = false;
	private GameEventReceiverService eventReceiver = null;

	public IList<string> ReceivedEvents {
		get { return this.registeredEvents.ToList();}
	}

	public virtual void Awake() {
		if (!awakeDone) {
			eventReceiver = new GameEventReceiverService(registeredEvents, ReceiveEvent);
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
}
