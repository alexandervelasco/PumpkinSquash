using UnityEngine;
using System.Collections;
using PathologicalGames;

public class CharacterActionDespawner : EventTransceiverBehavior {

	//serialized data
	public string spawnPoolName = string.Empty;
	public CharacterActionID characterActionID = CharacterActionID.None;

	private SpawnPool spawnPool = null;
	private Transform localTransform = null;

	// Use this for initialization
	public void Start () {
		spawnPool = PoolManager.Pools[spawnPoolName];
		localTransform = gameObject.transform;
	}
	
	// Update is called once per frame
	public void Update () {
	
	}

	#region implemented abstract members of EventReceiverBehavior

	public override void ReceiveEvent (string eventName, object args, object sender)
	{
		ICharacterAction characterAction = args as ICharacterAction;
		if (characterAction != null && characterAction.ID == characterActionID && characterAction.Source.transform == localTransform &&
		    spawnPool != null && spawnPool.IsSpawned(localTransform)) 
		{
			CallEvent(0, this);
			spawnPool.Despawn(localTransform);
		}
	}

	#endregion
}
