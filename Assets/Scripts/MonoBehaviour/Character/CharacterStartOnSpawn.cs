using UnityEngine;
using System.Collections;

public class CharacterStartOnSpawn : MonoBehaviour {

	//serialized data
	public bool broadcast = true;

	private bool initialStartCalled = false;

	// Use this for initialization
	void Start () {
		initialStartCalled = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnSpawned() {
		if (initialStartCalled && broadcast)
			gameObject.BroadcastMessage("Start");
		else if (initialStartCalled && !broadcast)
			gameObject.SendMessage("Start");
	}
}
