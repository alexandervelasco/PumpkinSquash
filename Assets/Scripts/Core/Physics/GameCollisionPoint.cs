using UnityEngine;
using System.Collections;

public class GameCollisionPoint : IGameCollisionPoint {

	private ContactPoint internalContactPoint;
	private ControllerColliderHit controllerCollision = null;

	#region IGameCollisionPoint implementation
	public Vector3 Normal {
		get {
			if (controllerCollision != null) return controllerCollision.normal;
			else return internalContactPoint.normal;
		}
	}
	public Vector3 Point {
		get {
			if (controllerCollision != null) return controllerCollision.point;
			else return internalContactPoint.point;
		}
	}
	public Collider Source {
		get {
			if (internalContactPoint.thisCollider != null) return internalContactPoint.thisCollider;
			else if (controllerCollision != null) return controllerCollision.controller;
			else return null;
		}
	}
	public Collider Target {
		get {
			if (internalContactPoint.otherCollider != null) return internalContactPoint.otherCollider;
			else if (controllerCollision != null) return controllerCollision.collider;
			else return null;
		}
	}
	#endregion

	private GameCollisionPoint(){}

	public GameCollisionPoint(ContactPoint contactPoint)
	{
		this.internalContactPoint = contactPoint;
	}

	public GameCollisionPoint(ControllerColliderHit collision)
	{
		this.controllerCollision = collision;
	}
}
