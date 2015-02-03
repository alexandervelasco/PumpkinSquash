using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameEventReceiverService : IDisposable {

	private IList<string> receivedEvents = null;
	private Action<string, object, object> eventReceiver = null;

	private GameEventReceiverService(){}
	public GameEventReceiverService(IList<string> receivedEvents, Action<string, object, object> eventReceiver)
	{
		if (receivedEvents == null)
			throw new ArgumentNullException ("receivedEvents");
		else if (eventReceiver == null)
			throw new ArgumentNullException ("eventReceiver");
		else 
		{
			this.receivedEvents = receivedEvents.ToList ();
			this.eventReceiver = eventReceiver;
			GameEventManager.RegisterEventReceiver (receivedEvents, eventReceiver);
		}
	}

	~GameEventReceiverService()
	{
		Dispose();
	}

	#region IDisposable implementation

	public void Dispose ()
	{
		GameEventManager.UnregisterEventReceiver(receivedEvents, eventReceiver);
	}

	#endregion
}
