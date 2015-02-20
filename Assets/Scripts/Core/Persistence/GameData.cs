using UnityEngine;
using System;

[Serializable]
public class GameData {

	[SerializeField]
	private int kills = 0;
	[SerializeField]
	private int timeInSeconds = 0;

	public int Kills {
		get {
			return kills;
		}
		set {
			kills = value;
		}
	}

	public int TimeInSeconds {
		get {
			return timeInSeconds;
		}
		set {
			timeInSeconds = value;
		}
	}

	public void Reset()
	{
		Kills = 0; TimeInSeconds = 0;
	}
}
