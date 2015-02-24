using UnityEngine;
using System.Collections.Generic;

public enum GameCollisionState
{
	None,
	CollisionEnter,
	CollisionExit,
	CollisionStay,
	TriggerEnter,
	TriggerExit,
	TriggerStay
}

public interface IGameCollision : IGameObjectSource {
	GameCollisionState Status { get; set; }
	Collider SourceCollider { get; }
	Collider TargetCollider { get; }
	IEnumerable<IGameCollisionPoint> CollisionPoints { get; }
	GameObject TargetGameObject { get; }
	Vector3 RelativeVelocity { get; }
	Rigidbody TargetRigidBody { get; }
	Transform TargetTransform { get; }
}
