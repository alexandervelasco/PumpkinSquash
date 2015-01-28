using UnityEngine;

public interface IGameTouch {

	Vector2 DeltaPosition {get;set;}	
	float DeltaTime {get;set;}	
	int FingerId {get;set;}	
	TouchPhase Phase {get;set;}	
	Vector2 Position {get;set;}	
	Vector2 RawPosition {get;set;}	
	int TapCount {get;set;}
}
