using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

public class ITargetedGameObjectRelay : MonoBehaviour {
	
	[Serializable]
	public class UnityEvent_ITargetedGameObjectRelay_1 : UnityEvent<GameObject> {}

	//serialized data
	public UnityEvent_ITargetedGameObjectRelay_1 onTargetSelect;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SelectTargets(object input)
	{
		ITargeted<GameObject> targets = input as ITargeted<GameObject>;
		if (targets != null)
			foreach (GameObject target in targets.Targets)
				onTargetSelect.Invoke(target);
	}

}
