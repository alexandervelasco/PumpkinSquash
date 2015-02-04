using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
public class CharacterActionBooleanAnimator : EventReceiverBehavior {

	//serialized data
	public List<string> characterActionIDs = new List<string>();
	public List<string> booleanAnimationNames = new List<string>();
	public List<CharacterActionStatus> enableStates = new List<CharacterActionStatus>();

	private Dictionary<string, string> characterActionIDMappings = null;
	private Animator animator = null;

	public Dictionary<string, string> CharacterActionIDMappings {
		get {
			return characterActionIDMappings;
		}
	}

	public List<CharacterActionStatus> EnableStates {
		get {
			return enableStates;
		}
	}

	// Use this for initialization
	void Start () {
		this.characterActionIDMappings = new Dictionary<string, string>();
		CharacterActionIDMappings.Load(characterActionIDs, booleanAnimationNames);
		animator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region implemented abstract members of EventReceiverBehavior

	public override void ReceiveEvent (string eventName, object args, object sender)
	{
		if (args is ICharacterAction)
		{
			ICharacterAction characterAction = args as ICharacterAction;
			if (CharacterActionIDMappings.ContainsKey(characterAction.ID))
				animator.SetBool(Animator.StringToHash(CharacterActionIDMappings[characterAction.ID]), 
				                 EnableStates.Contains(characterAction.Status));
		}
	}

	#endregion

	private CharacterActionStatus getEffectiveStatus(IEnumerable<CharacterActionStatus> statusItems)
	{
		CharacterActionStatus result = CharacterActionStatus.None;
		foreach (CharacterActionStatus status in statusItems)
			result = result | status;
		return result;
	}
}
