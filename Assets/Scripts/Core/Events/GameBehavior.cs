using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class GameBehavior : MonoBehaviour {

	public List<string> registeredEvents;

	private string[] eventsCopy = null;
	private bool awakeDone = false, destroyDone = false;

	public virtual void Awake() {
		if (!awakeDone) {
			eventsCopy = new string[registeredEvents.Count];
			registeredEvents.CopyTo(eventsCopy, 0);
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

	void RegisterEvents(string[] registeredEvents)
	{
		if (registeredEvents != null && registeredEvents.Length > 0) {
			foreach (string registeredEvent in registeredEvents) {
				if (!string.IsNullOrEmpty (registeredEvent))
					GameEventManager.RegisterEventReceiver(registeredEvent, ReceiveEvent);
			}
		}
	}

	private void UnregisterEvents(string[] registeredEvents) {
		if (registeredEvents != null && registeredEvents.Length > 0) {
			foreach (string registeredEvent in registeredEvents) {
				if (!string.IsNullOrEmpty (registeredEvent))
					GameEventManager.UnregisterEventReceiver(registeredEvent, ReceiveEvent);
			}
		}
	}
}
