using UnityEngine;

public class GameTouch : IGameTouch {

	private Vector2 deltaPosition;
	private float deltaTime;
	private int fingerId;
	private TouchPhase phase;
	private Vector2 position;
	private Vector2 rawPosition;
	private int tapCount;

	public Vector2 DeltaPosition {
		get {
			return deltaPosition;
		}
		set {
			deltaPosition = value;
		}
	}

	public float DeltaTime {
		get {
			return deltaTime;
		}
		set {
			deltaTime = value;
		}
	}

	public int FingerId {
		get {
			return fingerId;
		}
		set {
			fingerId = value;
		}
	}

	public TouchPhase Phase {
		get {
			return phase;
		}
		set {
			phase = value;
		}
	}

	public Vector2 Position {
		get {
			return position;
		}
		set {
			position = value;
		}
	}

	public Vector2 RawPosition {
		get {
			return rawPosition;
		}
		set {
			rawPosition = value;
		}
	}

	public int TapCount {
		get {
			return tapCount;
		}
		set {
			tapCount = value;
		}
	}

	public GameTouch()
	{
		this.DeltaPosition = Vector2.zero;
		this.DeltaTime = 0;
		this.FingerId = 0;
		this.Phase = TouchPhase.Began;
		this.Position = Vector2.zero;
		this.RawPosition = Vector2.zero;
		this.TapCount = 0;
	}

	public GameTouch(Touch unityTouch)
	{
		this.DeltaPosition = unityTouch.deltaPosition;
		this.DeltaTime = unityTouch.deltaTime;
		this.FingerId = unityTouch.fingerId;
		this.Phase = unityTouch.phase;
		this.Position = unityTouch.position;
		this.RawPosition = unityTouch.rawPosition;
		this.TapCount = unityTouch.tapCount;
	}
}
