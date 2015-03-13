using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class CharacterActionEventRelay : EventReceiverBehavior, IGameObjectSource {

	//serialized data
	public List<CharacterActionID> characterActionIDs = null;
	public List<CharacterActionStatus> characterActionStatuses = null;
	public UnityEvent onCharacterAction;
	public GameObject source = null;
	public bool matchSource = true;

	#region IGameObjectSource implementation

	public GameObject Source {
		get {
			return source;
		}
		set {
			source = value;
		}
	}

	#endregion

	// Use this for initialization
	void Start () {
		if (matchSource && source == null)
			source = gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region implemented abstract members of EventReceiverBehavior

	public override void ReceiveEvent (string eventName, object args, object sender)
	{
		ICharacterAction action = args as ICharacterAction;
		if (action != null && characterActionIDs.Contains(action.ID) && characterActionStatuses.Contains(action.Status) &&
		    (!matchSource || action.Source == Source))
			onCharacterAction.Invoke();
	}

	#endregion
}
