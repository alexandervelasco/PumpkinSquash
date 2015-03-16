using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using PathologicalGames;

public class RandomPositionPicker : MonoBehaviour {

	[Serializable]
	public class UnityEvent_RandomPositionPicker_1 : UnityEvent<Vector3> {}

	//serialized data
	public Vector3 minimumSpawnPosition = Vector3.zero;
	public Vector3 maximumSpawnPosition = Vector3.zero;
	public float minimumSpawnGapDistance = 0;
	public LayerMask avoidanceLayers;
	public UnityEvent_RandomPositionPicker_1 onPositionPick;

	// Use this for initialization
	public void Start () {
	}
	
	// Update is called once per frame
	public void Update () {
	}

	public void PickRandomPosition()
	{
		StartCoroutine(StartPickRandomPosition());
	}

	private IEnumerator StartPickRandomPosition()
	{
		ThreadSafeRandom r = new ThreadSafeRandom ();
		Vector3 randomPosition = Vector3.zero;
		bool hasNearbyColliders = false;
		do {
			randomPosition = new Vector3 (Mathf.Lerp(minimumSpawnPosition.x, maximumSpawnPosition.x, (float)r.NextDouble()),
			                              Mathf.Lerp(minimumSpawnPosition.y, maximumSpawnPosition.y, (float)r.NextDouble()),
			                              Mathf.Lerp(minimumSpawnPosition.z, maximumSpawnPosition.z, (float)r.NextDouble()));
			hasNearbyColliders = Physics.CheckSphere(randomPosition, minimumSpawnGapDistance, avoidanceLayers);
			yield return null;
		}
		while (hasNearbyColliders);
		onPositionPick.Invoke (randomPosition);
	}
}
