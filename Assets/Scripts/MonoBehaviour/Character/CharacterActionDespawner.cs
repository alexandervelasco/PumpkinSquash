using UnityEngine;
using System.Collections;
using PathologicalGames;

public class CharacterActionDespawner : EventTransceiverBehavior {

	//serialized data
	public string spawnPoolName = string.Empty;
	public CharacterActionID characterActionID = CharacterActionID.None;

	private SpawnPool spawnPool = null;
	private Transform transform = null;

	// Use this for initialization
	public override void Start () {
		spawnPool = PoolManager.Pools[spawnPoolName];
		transform = gameObject.transform;
	}
	
	// Update is called once per frame
	public override void Update () {
	
	}

	#region implemented abstract members of EventReceiverBehavior

	public override void ReceiveEvent (string eventName, object args, object sender)
	{
		ICharacterAction characterAction = args as ICharacterAction;
		if (characterAction != null && characterAction.Source.transform == transform && characterAction.ID == characterActionID &&
		    spawnPool != null && spawnPool.IsSpawned(transform)) 
		{
			CallEvent(0, this);
			spawnPool.Despawn(transform);
		}
	}

	#endregion
}
