using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Diagnostics;

public static class GameDataManager {
	private static GameData current = null;
	private static GameData high = null;

	public static GameData Current
	{
		get
		{
			if (current == null)
				current = new GameData();
			return current;
		}
	}

	public static GameData High {
		get {
			if (high == null)
				high = new GameData();
			return high;
		}
	}

	public static void LoadHigh(string fileName)
	{
#if !UNITY_EDITOR
		if (File.Exists(string.Format ("{0}/{1}", Application.persistentDataPath, fileName)))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			using (FileStream file = File.Open(string.Format ("{0}/{1}", Application.persistentDataPath, fileName), FileMode.Open))
			{
				high = formatter.Deserialize(file) as GameData;
			}
		}
#endif
	}

	public static void SaveHigh(string fileName)
	{
		if (Current.Kills > High.Kills)
			High.Kills = Current.Kills;
		if (Current.TimeInSeconds > High.TimeInSeconds)
			High.TimeInSeconds = Current.TimeInSeconds;
#if !UNITY_EDITOR
		BinaryFormatter formatter = new BinaryFormatter();
		using (FileStream file = File.Open(string.Format ("{0}/{1}", Application.persistentDataPath, fileName), FileMode.Create))
		{
			formatter.Serialize(file, High);
		}
#endif
	}
}
