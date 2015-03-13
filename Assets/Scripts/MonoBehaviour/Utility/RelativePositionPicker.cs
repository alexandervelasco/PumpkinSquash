using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

public class RelativePositionPicker : MonoBehaviour, IGameObjectSource {

	[Serializable]
	public class UnityEvent_RelativePositionPicker_Vector3 : UnityEvent<Vector3> {}

	//serialized data
	public GameObject source = null;
	public Vector3 positionOffset = Vector3.zero;
	public UnityEvent_RelativePositionPicker_Vector3 onPositionPick;

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
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CalculateRelativePosition()
	{
		onPositionPick.Invoke(Source.transform.position + positionOffset);
	}
}
