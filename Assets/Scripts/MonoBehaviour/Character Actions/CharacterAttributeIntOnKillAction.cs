using UnityEngine;
using System.Collections;

public class CharacterAttributeIntOnKillAction : EventTransceiverBehavior, ICharacterAction {
	#region ICharacterAction implementation
	public U GetProperty<T, U> (T propertyId) where T : System.IConvertible
	{
		throw new System.NotImplementedException ();
	}
	public void SetProperty<T, U> (T propertyId, U propertyValue) where T : System.IConvertible
	{
		throw new System.NotImplementedException ();
	}
	public CharacterActionID ID {
		get {
			throw new System.NotImplementedException ();
		}
		set {
			throw new System.NotImplementedException ();
		}
	}
	public CharacterActionStatus Status {
		get {
			throw new System.NotImplementedException ();
		}
		set {
			throw new System.NotImplementedException ();
		}
	}
	#endregion
	#region IGameObjectSource implementation
	public GameObject Source {
		get {
			throw new System.NotImplementedException ();
		}
		set {
			throw new System.NotImplementedException ();
		}
	}
	#endregion

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region implemented abstract members of EventTransceiverBehavior

	public override void ReceiveEvent (string eventName, object args, object sender)
	{
		throw new System.NotImplementedException ();
	}

	#endregion
}
