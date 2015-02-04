using UnityEngine;
using System.Collections.Generic;

public class CharacterActionBooleanAnimator : EventReceiverBehavior {

	//serialized data
	public List<string> characterActionIDs = new List<string>();
	public List<string> booleanAnimationNames = new List<string>();

	private Dictionary<string, string> characterActionIDMappings = null;

	public Dictionary<string, string> CharacterActionIDMappings {
		get {
			return characterActionIDMappings;
		}
	}

	// Use this for initialization
	void Start () {
		this.characterActionIDMappings = new Dictionary<string, string>();
		CharacterActionIDMappings.Load(characterActionIDs, booleanAnimationNames);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region implemented abstract members of EventReceiverBehavior

	public override void ReceiveEvent (string eventName, object args, object sender)
	{
		if (args is ICharacterAction)
		{
		}
	}

	#endregion
}
