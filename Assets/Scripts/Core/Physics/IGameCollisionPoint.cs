using UnityEngine;
using System.Collections;

public interface IGameCollisionPoint {
	Vector3 Normal { get; }
	Vector3 Point { get; }
	Collider Source { get; }
	Collider Target { get; }
}
