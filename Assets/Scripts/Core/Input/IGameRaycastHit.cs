using UnityEngine;

public interface IGameRaycastHit {
	Vector3 BarycentricCoordinate { get; set;}
	Collider Collider { get; set;}
	float Distance { get; set;}
	Vector2 LightmapCoord { get; set;}
	Vector3 Normal { get; set;}
	Vector3 Point { get; set;}
	Rigidbody Rigidbody { get; set;}
	Vector2 TextureCoord { get; set;}
	Vector2 TextureCoord2 { get; set;}
	Transform Transform { get; set;}
	int TriangleIndex { get; set;}
}
