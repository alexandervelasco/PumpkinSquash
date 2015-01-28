using UnityEngine;
using System.Collections;

public class ConsoleEventWriter : GameBehavior {
	
	#region implemented abstract members of GameBehavior

	public override void ReceiveEvent (string eventName, object args, object sender)
	{
		Debug.Log("Event received: " + eventName);
		if (args != null)
		{
			Debug.Log("Arguments");
			Debug.Log (args.ToString());
		}
	}

	#endregion
}
