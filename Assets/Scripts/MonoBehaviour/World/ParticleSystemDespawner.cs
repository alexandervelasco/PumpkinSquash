using UnityEngine;
using System.Collections;
using PathologicalGames;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleSystemDespawner : MonoBehaviour {

	//serialized data
	public string spawnPoolName = string.Empty;
	public float checkInterval = 0.25f;

	private ParticleSystem ps = null;
	private SpawnPool spawnPool = null;

	// Use this for initialization
	void Start () {
		ps = GetComponent<ParticleSystem>();
		if (!string.IsNullOrEmpty(spawnPoolName))
			spawnPool = PoolManager.Pools [spawnPoolName];
		StartCoroutine(Despawn());
	}
	
	// Update is called once per frame
	void Update () {
	}

	private IEnumerator Despawn()
	{
		bool psIsAlive = ps.IsAlive();
		while (psIsAlive)
		{
			yield return new WaitForSeconds(checkInterval);
			psIsAlive = ps.IsAlive();
		}
		if (spawnPool != null)
			spawnPool.Despawn(transform);
	}
}
