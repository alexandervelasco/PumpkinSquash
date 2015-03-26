using UnityEngine;
using System;
using System.Collections;

public class GameDataTracker : EventTransceiverBehavior {

	//serialized data
	public UIActionID resetActionID = UIActionID.WorldReset;
	public UIActionStatus resetActionStatus = UIActionStatus.None;
	public GameObject trackedGameObject = null;
	public CharacterActionID killActionID = CharacterActionID.None;
	public CharacterActionStatus killActionStatus = CharacterActionStatus.None;
	public CharacterActionID saveActionID = CharacterActionID.None;
	public CharacterActionStatus saveActionStatus = CharacterActionStatus.None;
	public string fileName = String.Empty;

	private DateTime sessionTime;

	// Use this for initialization
	void Start () {
		sessionTime = DateTime.Now;
		GameDataManager.LoadHigh(fileName);
		CallEvent(1, GameDataManager.High);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus)
			GameDataManager.SaveHigh(fileName);
	}

	void OnApplicationFocus(bool focusStatus)
	{
		if (!focusStatus)
			GameDataManager.SaveHigh(fileName);
	}

	void OnApplicationQuit()
	{
		GameDataManager.SaveHigh(fileName);
	}

	#region implemented abstract members of EventTransceiverBehavior

	public override void ReceiveEvent (string eventName, object args, object sender)
	{
		IUIAction uiAction = args as IUIAction;
		ICharacterAction characterAction = args as ICharacterAction;
		if (characterAction != null && characterAction.Source == trackedGameObject)
		{
			if (characterAction.ID == killActionID && (characterAction.Status & killActionStatus) == killActionStatus)
			{
				GameDataManager.Current.Kills++;
				if (GameDataManager.UpdateHigh())
					CallEvent(1, GameDataManager.High);
				CallEvent(0, GameDataManager.Current);
			}
			else if (characterAction.ID == saveActionID && (characterAction.Status & saveActionStatus) == saveActionStatus)
			{
				GameDataManager.Current.TimeInSeconds = (int)(DateTime.Now - sessionTime).TotalSeconds;
				CallEvent(0, GameDataManager.Current);
				GameDataManager.SaveHigh(fileName);
			}
		}
		else if (uiAction != null && uiAction.ID == resetActionID && (uiAction.Status & resetActionStatus) == resetActionStatus)
		{
			GameDataManager.Current.Reset();
			sessionTime = DateTime.Now;
			CallEvent(0, GameDataManager.Current);
		}
	}

	#endregion
}
