using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class KillCounter : EventReceiverBehavior {

	private Text text;

	// Use this for initialization
	void Start () {
		text = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region implemented abstract members of EventReceiverBehavior

	public override void ReceiveEvent (string eventName, object args, object sender)
	{
		GameData gameData = args as GameData;
		if (gameData != null && text != null)
			text.text = gameData.Kills.ToString();
	}

	#endregion
}
