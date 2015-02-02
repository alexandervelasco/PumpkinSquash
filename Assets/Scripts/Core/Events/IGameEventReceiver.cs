using UnityEngine;
using System.Collections;

public interface IGameEventReceiver {	
	void ReceiveEvent (string eventName, object args, object sender);
}
