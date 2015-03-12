using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using PathologicalGames;

public class RandomPositionPicker : EventTransceiverBehavior {

	//serialized data
	public string spawnPoolName = string.Empty;
	public GameObject spawnedPrefab = null;
	public int maximumSpawnAmount = 0;
	public Vector3 spawnOffset = Vector3.zero;
	public Vector3 maximumSpawnDistance = Vector3.zero;
	public float minimumSpawnGapRadius = 0;

	private SpawnPool spawnPool = null;

	// Use this for initialization
	public void Start () {
		spawnPool = PoolManager.Pools[spawnPoolName];
	}
	
	// Update is called once per frame
	public void Update () {
	}

	#region implemented abstract members of EventTransceiverBehavior

	public override void ReceiveEvent (string eventName, object args, object sender)
	{
		StartCoroutine(SpawnInRandomPosition(spawnedPrefab));
	}

	#endregion

	public void StartSpawn()
	{
		StartCoroutine(SpawnInRandomPosition(spawnedPrefab));
	}

	private IEnumerator SpawnInRandomPosition (GameObject spawnedPrefab)
	{
		ThreadSafeRandom r = new ThreadSafeRandom ();
		Vector3 randomPosition = Vector3.zero;
		bool hasNearbyInteractables = false;
		do {
			randomPosition = (new Vector3 (maximumSpawnDistance.x * (float)r.NextDouble (), maximumSpawnDistance.y * (float)r.NextDouble (), maximumSpawnDistance.z * (float)r.NextDouble ())) + spawnOffset;
			IEnumerable<Transform> nearbyInteractables = spawnPool.Where(
				t => Vector3.Distance(randomPosition, t.position) <= minimumSpawnGapRadius && spawnedPrefab.layer == t.gameObject.layer);
			hasNearbyInteractables = nearbyInteractables.Count() > 0;
			yield return null;
		}
		while (hasNearbyInteractables);
		Transform spawn = spawnPool.Spawn(spawnedPrefab);
		spawn.position = randomPosition;
	}
}
