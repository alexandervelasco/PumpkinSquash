using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class WorldReset : EventTransceiverBehavior, ITargeted<GameObject> {

	public List<GameObject> targets = null;
	public UIActionID actionID = UIActionID.WorldReset;

	#region ITargeted implementation
	public List<GameObject> Targets {
		get {
			if (targets == null)
				targets = new List<GameObject>();
			return targets;
		}
	}
	#endregion

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region implemented abstract members of EventTransceiverBehavior

	public override void ReceiveEvent (string eventName, object args, object sender)
	{
		IUIAction action = args as IUIAction;
		if (action != null && action.ID == actionID)
		{
			foreach (GameObject target in Targets)
			{
				CharacterAttributeInt[] attributes = target.GetComponents<CharacterAttributeInt>();
				CharacterAttributeInt attribute = attributes.FirstOrDefault(t => t.ID == ModifiableID.AttributeHP);
				if (attribute != null)
				{
					attribute.BaseValue = new TypedValue32<ModifiableType, int>(attribute.FinalValue.Type, int.MaxValue);
					CallEvent(0, null);
				}
			}
		}
	}

	#endregion
}
