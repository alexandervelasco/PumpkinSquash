using UnityEngine;
using System.Collections;

public class GameObjectLoader : EventReceiverBehavior {

	//serialized data
	public GameObject[] dataToLoad;
	public int frameDelay = 2;
	public int nextScene = 1;
	public bool persistLoadedObjects = true;
	
	// Use this for initialization
	public override void Start () {
		BeginLoad();
	}

	#region implemented abstract members of GameBehavior

	public override void ReceiveEvent (string eventName, object args, object sender)
	{
	}

	#endregion
	
	private void BeginLoad()
	{
		StartCoroutine(Load());
	}
	
	private IEnumerator Load()
	{
		while (frameDelay != 0)
		{
			frameDelay--;
			yield return null;
		}
		foreach (GameObject data in dataToLoad)
		{
			GameObject instance = (GameObject)GameObject.Instantiate(data);
			if (persistLoadedObjects)
				DontDestroyOnLoad(instance);
			yield return null;
		}
		Application.LoadLevel(nextScene);
	}
}
