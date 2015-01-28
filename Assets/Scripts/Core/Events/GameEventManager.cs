using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public static class GameEventManager {
	private static Dictionary<string, Action<string, object, object>> eventDict = new Dictionary<string, Action<string, object, object>>();
	
	public static void CallEvent(string key, object args, object sender = null)
	{
		if (eventDict.ContainsKey(key))
			eventDict[key](key, args, sender);
	}
	
	public static void RegisterEventReceiver(string key, Action<string, object, object> eventReceiver)
	{
		if (!eventDict.ContainsKey(key))
			eventDict.Add(key, eventReceiver);
		else
			eventDict[key] += eventReceiver;
	}
	
	public static void UnregisterEventReceiver(string key, Action<string, object, object> eventReceiver)
	{
		if (eventDict.ContainsKey(key))
		{
			eventDict[key] -= eventReceiver;
			if (eventDict[key] == null || eventDict[key].GetInvocationList().Length == 0)
				eventDict.Remove(key);
		}
	}
}
