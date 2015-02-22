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
	}
	
	// Update is called once per frame
	void Update () {
	
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
