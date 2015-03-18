using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

public class GameObjectTransformRelay : MonoBehaviour {

	[Serializable]
	public class UnityEvent_GameObjectTransformRelay_1 : UnityEvent<Transform> {}

	//serialized data
	public UnityEvent_GameObjectTransformRelay_1 onTransformRelay;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GetTransform(GameObject gameObject)
	{
		onTransformRelay.Invoke (gameObject.transform);
	}
}
