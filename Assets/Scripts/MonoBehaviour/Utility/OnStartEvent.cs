using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class OnStartEvent : MonoBehaviour {

	//serialized data
	public UnityEvent onStart;

	// Use this for initialization
	void Start () {
		onStart.Invoke();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
