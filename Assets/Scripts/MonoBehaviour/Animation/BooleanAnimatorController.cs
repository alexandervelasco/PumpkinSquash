using UnityEngine;
using System.Collections.Generic;

public class BooleanAnimatorController : EventReceiverBehavior {

	public GameObject target = null;	
	public List<string> booleanParameterNames;

	private Animator targetAnimator = null;
	private Dictionary<string, bool> isTrueState = null;
	private Dictionary<string, string> animationNames = null;

	// Use this for initialization
	void Start () {
		if (target == null)
			target = gameObject;
		targetAnimator = target.GetComponent<Animator>();
		isTrueState = new Dictionary<string, bool>();
		animationNames = new Dictionary<string, string> ();
		for (int i = 0; i < ReceivedEvents.Count; i++) 
		{
			isTrueState.Add (ReceivedEvents[i], i % 2 == 0);
			animationNames.Add(ReceivedEvents[i], booleanParameterNames[i / 2]);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region implemented abstract members of GameBehavior

	public override void ReceiveEvent (string eventName, object args, object sender)
	{
		if (targetAnimator != null && isTrueState.ContainsKey(eventName) && animationNames.ContainsKey(eventName))
			targetAnimator.SetBool(Animator.StringToHash(animationNames[eventName]), isTrueState[eventName]);
	}

	#endregion
}
