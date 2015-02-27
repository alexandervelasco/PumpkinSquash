using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PathologicalGames;

public class PoolSpawner : EventCallerBehavior {

	//serialized data
	public string spawnPoolName = string.Empty;
	public GameObject spawnedPrefab = null;
	public float spawnRate = 0;
	public int maximumSpawnAmount = 0;
	public Vector3 spawnOffset = Vector3.zero;
	public Vector3 maximumSpawnDistance = Vector3.zero;
	public float minimumSpawnGapRadius = 0;

	private float spawnTimer = 0;
	private SpawnPool spawnPool = null;

	// Use this for initialization
	public void Start () {
		spawnTimer = spawnRate;
		spawnPool = PoolManager.Pools[spawnPoolName];
	}
	
	// Update is called once per frame
	public void Update () {
		spawnTimer -= Time.deltaTime;
		if (spawnTimer <= 0)
		{
			if (spawnPool != null && spawnPool.Count < maximumSpawnAmount)
			{
				StartCoroutine(SpawnInRandomPosition(spawnedPrefab));
			}
			spawnTimer = spawnRate;
		}
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
