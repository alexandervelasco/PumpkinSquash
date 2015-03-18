using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Events;

public class RandomGameObjectPicker : MonoBehaviour {

	[Serializable]
	public class UnityEvent_RandomGameObjectPicker_1 : UnityEvent<GameObject> {}

	//serialized data
	public List<GameObject> gameObjects;
	public UnityEvent_RandomGameObjectPicker_1 onGameObjectPick;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PickRandomGameObject()
	{
		if (gameObjects != null && gameObjects.Count > 0)
		{
			ThreadSafeRandom r = new ThreadSafeRandom();
			onGameObjectPick.Invoke(gameObjects[r.Next(gameObjects.Count)]);
		}
	}
}
