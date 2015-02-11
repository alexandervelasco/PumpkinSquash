using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CharacterActionTriggerAnimator : EventReceiverBehavior {
		
	//serialized data
	public List<CharacterActionID> characterActionIDs = new List<CharacterActionID>();
	public List<string> triggerAnimationNames = new List<string>();
	public List<CharacterActionStatus> triggerStates = new List<CharacterActionStatus>();

	private Dictionary<CharacterActionID, string> characterActionIDMappings = null;
	private Animator animator = null;
	
	public Dictionary<CharacterActionID, string> CharacterActionIDMappings {
		get {
			return characterActionIDMappings;
		}
	}
	
	public List<CharacterActionStatus> TriggerStates {
		get {
			return triggerStates;
		}
	}

	// Use this for initialization
	void Start () {
		this.characterActionIDMappings = new Dictionary<CharacterActionID, string>();
		CharacterActionIDMappings.Load(characterActionIDs, triggerAnimationNames);
		animator = gameObject.GetComponent<Animator>();	
	}
	
	// Update is called once per frame
	void Update () {
	}

	#region implemented abstract members of EventReceiverBehavior

	public override void ReceiveEvent (string eventName, object args, object sender)
	{
		ICharacterAction characterAction = args as ICharacterAction;
		if (characterAction != null)
		{
			CharacterActionStatus effectiveStatus = GetEffectiveStatus(TriggerStates);
			if (characterAction.Source == gameObject && CharacterActionIDMappings != null && CharacterActionIDMappings.ContainsKey(characterAction.ID) &&
			    (effectiveStatus & characterAction.Status) == characterAction.Status)
			{
				StartCoroutine(Trigger(Animator.StringToHash(CharacterActionIDMappings[characterAction.ID])));
			}
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

	private IEnumerator Trigger (int id)
	{
		animator.SetTrigger(id);
		yield return null;
		animator.ResetTrigger(id);
	}
}
