using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GameCollision : IGameCollision {

	private Collision internalCollision = null;
	private ControllerColliderHit controllerCollision = null;
	private Collider sourceCollider = null;
	private Collider triggerTarget = null;

	#region IGameCollision implementation
	public GameCollisionState Status { get; set; }
	public Collider SourceCollider {
		get {
			return sourceCollider;
		}
	}
	public Collider TargetCollider {
		get {
			if (internalCollision != null)
				return internalCollision.collider;
			else if (controllerCollision != null)
				return controllerCollision.collider;
			else if (triggerTarget != null)
				return triggerTarget;
			else return null;
		}
	}
	public IEnumerable<IGameCollisionPoint> CollisionPoints {
		get {
			if (internalCollision != null)
				return internalCollision.contacts.Select(t => new GameCollisionPoint(t) as IGameCollisionPoint);
			else if (controllerCollision != null)
				return new List<IGameCollisionPoint>{ new GameCollisionPoint(controllerCollision) };
			else return null;
		}
	}
	public GameObject TargetGameObject {
		get {
			if (internalCollision != null)
				return internalCollision.gameObject;
			else if (controllerCollision != null)
				return controllerCollision.collider.gameObject;
			else if (triggerTarget != null)
				return triggerTarget.gameObject;
			else return null;
		}
	}
	public Vector3 RelativeVelocity {
		get {
			if (internalCollision != null)
				return internalCollision.relativeVelocity;
			else if (controllerCollision != null)
				return Vector3.Normalize(controllerCollision.moveDirection) * controllerCollision.moveLength;
			else return Vector3.zero;
		}
	}
	public Rigidbody TargetRigidBody {
		get {
			if (internalCollision != null)
				return internalCollision.rigidbody;
			else return null;
		}
	}
	public Transform TargetTransform {
		get {
			if (internalCollision != null)
				return internalCollision.transform;
			else if (controllerCollision != null)
				return controllerCollision.transform;
			else if (triggerTarget != null)
				return triggerTarget.transform;
			else return null;
		}
	}
	#endregion

	#region IGameObjectSource implementation
	public GameObject Source { get; set; }
	#endregion

	private GameCollision(){}

	public GameCollision(Collision collision, GameCollisionState state, Collider source)
	{
		sourceCollider = source;
		Source = source.gameObject;
		internalCollision = collision;
	}

	public GameCollision(Collider target, GameCollisionState state, Collider source)
	{
		sourceCollider = source;
		Source = source.gameObject;
		triggerTarget = target;
	}

	public GameCollision(ControllerColliderHit collision, GameCollisionState state, Collider source)
	{
		sourceCollider = source;
		Source = source.gameObject;
		controllerCollision = collision;
	}
}
