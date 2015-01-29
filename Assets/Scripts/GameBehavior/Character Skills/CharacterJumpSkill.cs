using UnityEngine;
using System.Collections;

public class CharacterJumpSkill : EventCallingGameBehavior {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region implemented abstract members of GameBehavior

	public override void ReceiveEvent (string eventName, object args, object sender)
	{
		throw new System.NotImplementedException ();
	}

	#endregion
}
