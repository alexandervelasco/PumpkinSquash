using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

public class FindGameObjectsWithTag : MonoBehaviour {

	[Serializable]
	public class UnityEvent_FindGameObjectByTag_1 : UnityEvent<GameObject> {}

	[Serializable]
	public class UnityEvent_FindGameObjectByTag_2 : UnityEvent<GameObject[]> {}

	//serialized data
	public string gameObjectTag = string.Empty;
	public bool triggerOnStart = false;
	public UnityEvent_FindGameObjectByTag_1 onGameObjectFindEach;
	public UnityEvent_FindGameObjectByTag_2 onGameObjectFindAll;

	// Use this for initialization
	void Start () {
		if (triggerOnStart)
			Find();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Find()
	{
		Find(this.gameObjectTag);
	}

	public void Find(string tag)
	{
		GameObject[] result = GameObject.FindGameObjectsWithTag(tag);
		onGameObjectFindAll.Invoke(result);
		foreach (GameObject gameObject in result)
			onGameObjectFindEach.Invoke(gameObject);
	}
}
