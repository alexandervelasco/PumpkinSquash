using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class CharacterCollider : EventCallerBehavior {

	private Collider sourceCollider = null;

	void Start()
	{
		sourceCollider = GetComponent<Collider>();
	}

	void OnCollisionEnter (Collision collision)
	{
		CallEvent (0, new GameCollision (collision, GameCollisionState.CollisionEnter, sourceCollider));
	}

	void OnCollisionExit (Collision collision)
	{
		CallEvent (0, new GameCollision (collision, GameCollisionState.CollisionExit, sourceCollider));
	}

	void OnCollisionStay (Collision collision)
	{
		CallEvent (0, new GameCollision (collision, GameCollisionState.CollisionStay, sourceCollider));
	}

	void OnTriggerEnter (Collider target)
	{
		CallEvent (0, new GameCollision (target, GameCollisionState.TriggerEnter, sourceCollider));
	}

	void OnTriggerExit (Collider target)
	{
		CallEvent (0, new GameCollision (target, GameCollisionState.TriggerExit, sourceCollider));
	}

	void OnTriggerStay (Collider target)
	{
		CallEvent (0, new GameCollision (target, GameCollisionState.TriggerStay, sourceCollider));
	}

	void OnControllerColliderHit (ControllerColliderHit collision)
	{
		CallEvent (0, new GameCollision (collision, GameCollisionState.CollisionEnter, sourceCollider));
	}
}
