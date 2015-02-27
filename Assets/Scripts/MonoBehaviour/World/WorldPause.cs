using UnityEngine;
using System.Collections;

public class WorldPause : EventTransceiverBehavior {

	//serialized data
	public UIActionID pauseActionID = UIActionID.None;

	private bool paused = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region implemented abstract members of EventTransceiverBehavior

	public override void ReceiveEvent (string eventName, object args, object sender)
	{
		IUIAction action = args as IUIAction;
		if (action != null && action.ID == pauseActionID && action.Status == UIActionStatus.Clicked)
		{
			paused = !paused;
			Time.timeScale = paused ? 0.0f : 1.0f;
			CallEvent(0, paused);
		}
	}

	#endregion
}
