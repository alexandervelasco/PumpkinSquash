using UnityEngine;
using System.Collections;
using PathologicalGames;

public class PoolSpawner : EventCallerBehavior {

	//serialized data
	public string spawnPoolName = string.Empty;
	public GameObject spawnedPrefab = null;
	public float spawnRate = 0;
	public int maxSpawn = 0;
	public Vector3 spawnOffset = Vector3.zero;
	public float maxSpawnRadius = 0;

	private float spawnTimer = 0;
	private SpawnPool spawnPool = null;

	// Use this for initialization
	public override void Start () {
		spawnTimer = spawnRate;
		spawnPool = PoolManager.Pools[spawnPoolName];
	}
	
	// Update is called once per frame
	public override void Update () {
		spawnTimer -= Time.deltaTime;
		if (spawnTimer <= 0)
		{
			if (spawnPool != null && spawnPool.Count < maxSpawn)
			{
				ThreadSafeRandom r = new ThreadSafeRandom();
				Vector3 randomDirection = Vector3.Normalize(new Vector3((float)r.NextDouble(), 0, (float)r.NextDouble()));
				Transform spawn = spawnPool.Spawn(spawnedPrefab);
				spawn.Translate((randomDirection * (maxSpawnRadius * (float)r.NextDouble())) + spawnOffset);
			}
			spawnTimer = spawnRate;
		}
	}
}
