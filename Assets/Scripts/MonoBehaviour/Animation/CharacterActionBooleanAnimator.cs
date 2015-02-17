using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
public class CharacterActionBooleanAnimator : EventReceiverBehavior, IGameObjectSource {

	//serialized data
	public GameObject source = null;
	public List<CharacterActionID> characterActionIDs = new List<CharacterActionID>();
	public List<string> booleanAnimationNames = new List<string>();
	public List<CharacterActionStatus> enableStates = new List<CharacterActionStatus>();

	private Dictionary<CharacterActionID, string> characterActionIDMappings = null;
	private Animator animator = null;

	public Dictionary<CharacterActionID, string> CharacterActionIDMappings {
		get {
			return characterActionIDMappings;
		}
	}

	public List<CharacterActionStatus> EnableStates {
		get {
			return enableStates;
		}
	}

	#region IGameObjectSource implementation

	public GameObject Source {
		get {
			if (source == null)
				source = gameObject;
			return source;
		}
		set {
			source = value;
		}
	}

	#endregion

	// Use this for initialization
	public void Start () {
		this.characterActionIDMappings = new Dictionary<CharacterActionID, string>();
		CharacterActionIDMappings.Load(characterActionIDs, booleanAnimationNames);
		animator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	public void Update () {
	
	}

	#region implemented abstract members of EventReceiverBehavior

	public override void ReceiveEvent (string eventName, object args, object sender)
	{
		ICharacterAction characterAction = args as ICharacterAction;
		if (characterAction != null)
		{
			CharacterActionStatus effectiveStatus = GetEffectiveStatus(EnableStates);
			if (characterAction.Source == Source && CharacterActionIDMappings != null && CharacterActionIDMappings.ContainsKey(characterAction.ID))
				animator.SetBool(Animator.StringToHash(CharacterActionIDMappings[characterAction.ID]), 
				                 (effectiveStatus & characterAction.Status) == characterAction.Status);
		}
	}

	#endregion

	private CharacterActionStatus GetEffectiveStatus(IEnumerable<CharacterActionStatus> statusItems)
	{
		CharacterActionStatus result = CharacterActionStatus.None;
		foreach (CharacterActionStatus status in statusItems)
			result = result | status;
		return result;
	}
}
