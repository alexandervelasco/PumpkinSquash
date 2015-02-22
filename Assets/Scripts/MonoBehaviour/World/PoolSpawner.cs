using UnityEngine;
using System.Collections;
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
				ThreadSafeRandom r = new ThreadSafeRandom();
				Vector3 randomPosition = Vector3.zero;
				Transform spawn = spawnPool.Spawn(spawnedPrefab);
				bool nearbyInteractables = false;
				do
				{
					randomPosition = (new Vector3(maximumSpawnDistance.x * (float)r.NextDouble(),
					                              maximumSpawnDistance.y * (float)r.NextDouble(),
					                              maximumSpawnDistance.z * (float)r.NextDouble())) + spawnOffset;
					nearbyInteractables = Physics.CheckSphere(randomPosition, minimumSpawnGapRadius, 1 << spawn.gameObject.layer);
				} while (nearbyInteractables);
				spawn.position = randomPosition;
				spawn.BroadcastMessage("Start");
			}
			spawnTimer = spawnRate;
		}
	}
}
