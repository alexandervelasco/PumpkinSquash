using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class GameBehavior : MonoBehaviour {

	public List<string> registeredEvents;

	private IList<string> eventsCopy = null;
	private bool awakeDone = false, destroyDone = false;

	protected IList<string> Events {
		get {
			return eventsCopy;
		}
	}

	public virtual void Awake() {
		if (!awakeDone) {
			eventsCopy = new List<string>(registeredEvents).AsReadOnly();
			RegisterEvents(eventsCopy);
			awakeDone = true;
		}
	}

	public virtual void OnDestroy() {
		if (!destroyDone) {
			UnregisterEvents(eventsCopy);
			destroyDone = true;
		}
	}

	public abstract void ReceiveEvent (string eventName, object args, object sender);

	void RegisterEvents(IList<string> registeredEvents)
	{
		if (registeredEvents != null && registeredEvents.Count > 0) {
			foreach (string registeredEvent in registeredEvents) {
				if (!string.IsNullOrEmpty (registeredEvent))
					GameEventManager.RegisterEventReceiver(registeredEvent, ReceiveEvent);
			}
		}
	}

	private void UnregisterEvents(IList<string> registeredEvents) {
		if (registeredEvents != null && registeredEvents.Count > 0) {
			foreach (string registeredEvent in registeredEvents) {
				if (!string.IsNullOrEmpty (registeredEvent))
					GameEventManager.UnregisterEventReceiver(registeredEvent, ReceiveEvent);
			}
		}
	}
}
