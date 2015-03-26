using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class EventToggle : MonoBehaviour {

	//serialized data
	[SerializeField]
	private List<UnityEvent> eventToggle;

	private int current = 0;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void Toggle()
	{
		if (eventToggle != null && eventToggle.Count > 0)
		{
			eventToggle[current].Invoke();
			current = (current + 1) % eventToggle.Count;
		}
	}
}
