using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class EventSequence : MonoBehaviour {

	//serialized data
	public List<UnityEvent> eventSequence;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartSequence()
	{
		if (eventSequence != null)
			foreach (UnityEvent unityEvent in eventSequence)
				unityEvent.Invoke();
	}
}
