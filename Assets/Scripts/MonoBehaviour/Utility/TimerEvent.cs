using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class TimerEvent : EventTransceiverBehavior {

	//serialized data
	public bool repeat = false;
	public float time = 0.0f;
	public bool triggerOnStart = true;
	public UnityEvent onTime = null;

	private float currentTime = 0.0f;

	// Use this for initialization
	void Start () {
		if (triggerOnStart)
			currentTime = time;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentTime > 0.0f)
		{
			currentTime -= Time.deltaTime;
			if (currentTime <= 0.0f)
			{
				onTime.Invoke();
				CallEvent(0, null, this);
				if (repeat)
					currentTime = time;
			}
		}
	}

	#region implemented abstract members of EventTransceiverBehavior

	public override void ReceiveEvent (string eventName, object args, object sender)
	{
		if (currentTime <= 0.0f)
			currentTime = time;
	}

	#endregion
}
