using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class CharacterControllerVelocityAggregate : MonoBehaviour {

	public Vector3 currentVelocityUPF = Vector3.zero;

	private CharacterController characterController = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (characterController == null)
			characterController = gameObject.GetComponent<CharacterController>();
		characterController.Move(currentVelocityUPF);
		currentVelocityUPF = Vector3.zero;
	}
}
