using UnityEngine;

public class WorldTouch : IWorldTouch {

	private IGameTouch gameTouch;
	private Vector3 barycentricCoordinate;
	private Collider collider;
	private float distance;
	private Vector2 lightmapCoord;
	private Vector3 normal;
	private Vector3 point;
	private Rigidbody rigidbody;
	private Vector2 textureCoord;
	private Vector2 textureCoord2;
	private Transform transform;
	private int triangleIndex;

	public Vector2 DeltaPosition {
		get {
			return gameTouch.DeltaPosition;
		}
		set {
			gameTouch.DeltaPosition = value;
		}
	}
	
	public float DeltaTime {
		get {
			return gameTouch.DeltaTime;
		}
		set {
			gameTouch.DeltaTime = value;
		}
	}
	
	public int FingerId {
		get {
			return gameTouch.FingerId;
		}
		set {
			gameTouch.FingerId = value;
		}
	}
	
	public TouchPhase Phase {
		get {
			return gameTouch.Phase;
		}
		set {
			gameTouch.Phase = value;
		}
	}
	
	public Vector2 Position {
		get {
			return gameTouch.Position;
		}
		set {
			gameTouch.Position = value;
		}
	}
	
	public Vector2 RawPosition {
		get {
			return gameTouch.RawPosition;
		}
		set {
			gameTouch.RawPosition = value;
		}
	}
	
	public int TapCount {
		get {
			return gameTouch.TapCount;
		}
		set {
			gameTouch.TapCount = value;
		}
	}

	public virtual Vector3 BarycentricCoordinate {
		get {
			return barycentricCoordinate;
		}
		set {
			barycentricCoordinate = value;
		}
	}

	public virtual Collider Collider {
		get {
			return collider;
		}
		set {
			collider = value;
		}
	}

	public virtual float Distance {
		get {
			return distance;
		}
		set {
			distance = value;
		}
	}

	public virtual Vector2 LightmapCoord {
		get {
			return lightmapCoord;
		}
		set {
			lightmapCoord = value;
		}
	}

	public virtual Vector3 Normal {
		get {
			return normal;
		}
		set {
			normal = value;
		}
	}

	public virtual Vector3 Point {
		get {
			return point;
		}
		set {
			point = value;
		}
	}

	public virtual Rigidbody Rigidbody {
		get {
			return rigidbody;
		}
		set {
			rigidbody = value;
		}
	}

	public virtual Vector2 TextureCoord {
		get {
			return textureCoord;
		}
		set {
			textureCoord = value;
		}
	}

	public virtual Vector2 TextureCoord2 {
		get {
			return textureCoord2;
		}
		set {
			textureCoord2 = value;
		}
	}

	public virtual Transform Transform {
		get {
			return transform;
		}
		set {
			transform = value;
		}
	}

	public virtual int TriangleIndex {
		get {
			return triangleIndex;
		}
		set {
			triangleIndex = value;
		}
	}

	private WorldTouch(){}

	public WorldTouch(IGameTouch gameTouch)
	{
		this.gameTouch = gameTouch;
		this.BarycentricCoordinate = Vector3.zero;
		this.Collider = null;
		this.Distance = 0;
		this.LightmapCoord = Vector2.zero;
		this.Normal = Vector3.zero;
		this.Point = Vector3.zero;
		this.Rigidbody = null;
		this.TextureCoord = Vector2.zero;
		this.TextureCoord2 = Vector2.zero;
		this.Transform = null;
		this.TriangleIndex = 0;
	}

	public WorldTouch(IGameTouch gameTouch, RaycastHit raycastHit)
	{
		this.gameTouch = gameTouch;
		this.BarycentricCoordinate = raycastHit.barycentricCoordinate;
		this.Collider = raycastHit.collider;
		this.Distance = raycastHit.distance;
		this.LightmapCoord = raycastHit.lightmapCoord;
		this.Normal = raycastHit.normal;
		this.Point = raycastHit.point;
		this.Rigidbody = raycastHit.rigidbody;
		this.TextureCoord = raycastHit.textureCoord;
		this.TextureCoord2 = raycastHit.textureCoord2;
		this.Transform = raycastHit.transform;
		this.TriangleIndex = raycastHit.triangleIndex;
	}
}
