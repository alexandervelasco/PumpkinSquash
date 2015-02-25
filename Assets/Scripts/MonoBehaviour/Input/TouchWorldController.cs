using UnityEngine;
using System.Collections.Generic;

public class TouchWorldController : EventTransceiverBehavior {

	//serialized data
	public Camera referenceCamera;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region implemented abstract members of GameBehavior

	public override void ReceiveEvent (string eventName, object args, object sender)
	{
		IGameTouch[] gameTouches = args as IGameTouch[];
		if (gameTouches != null && referenceCamera != null) 
		{
			List<IWorldTouch> worldTouches = new List<IWorldTouch>();
			for (int i = 0; i < gameTouches.Length; i++)
			{
				IGameTouch gameTouch = gameTouches[i];
				IWorldTouch worldTouch = null;
				if (!(gameTouch is IWorldTouch))
				{
					Ray ray = referenceCamera.ScreenPointToRay(new Vector3(gameTouch.Position.x,
					                                                       gameTouch.Position.y,
					                                                       0));
					RaycastHit hit;
					if (Physics.Raycast(ray, out hit))
						worldTouch = new WorldTouch(gameTouch, hit);
					else
						worldTouch = new WorldTouch(gameTouch);
				}
				else
					worldTouch = gameTouch as IWorldTouch;
				worldTouches.Add(worldTouch);
			}
			CallEvent(0, worldTouches);
			CallEvent(1, worldTouches.ToArray());
		}
	}

	#endregion
}
