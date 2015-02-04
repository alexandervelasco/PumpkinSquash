using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

[RequireComponent(typeof(CharacterController))]
public class CharacterControllerAcceleration : MonoBehaviour {

	//serialized data
	public Vector3 characterGravityUPF2 = Vector3.zero;
	public Vector3 absoluteVelocity = Vector3.zero;
	public Vector3 currentVelocity = Vector3.zero;

	private CharacterController characterController = null;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (characterController == null)
			characterController = gameObject.GetComponent<CharacterController>();
		if (!characterController.isGrounded)
			currentVelocity = currentVelocity + characterGravityUPF2;
		characterController.Move (absoluteVelocity + currentVelocity);
		absoluteVelocity = Vector3.zero;
	}
}
