using UnityEngine;
using System.Collections.Generic;
using PathologicalGames;

public class PoolSpawner : EventTransceiverBehavior {

	//serialized data
	public string spawnPoolName = string.Empty;
	public GameObject spawnedPrefab = null;

	private SpawnPool spawnPool = null;

	// Use this for initialization
	void Start () {
		if (!string.IsNullOrEmpty(spawnPoolName))
			spawnPool = PoolManager.Pools[spawnPoolName];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region implemented abstract members of EventTransceiverBehavior

	public override void ReceiveEvent (string eventName, object args, object sender)
	{
		if (args is Vector3)
			Spawn((Vector3)args);
		else
			Spawn();
	}

	#endregion

	public void Spawn()
	{
		Spawn(transform.position);
	}

	public void Spawn(Vector3 position)
	{
		if (spawnPool != null)
		{
			Transform instance = spawnPool.Spawn(spawnedPrefab, position, Quaternion.identity);
			CallEvent(0, instance, this);
		}
	}
}
