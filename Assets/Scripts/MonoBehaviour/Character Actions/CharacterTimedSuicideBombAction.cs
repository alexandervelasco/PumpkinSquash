using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterAttributeInt))]
[RequireComponent(typeof(Collider))]
public class CharacterTimedSuicideBombAction : EventTransceiverBehavior, ICharacterAction {

	//serialized data
	public GameObject source = null;
	public CharacterActionID id;
	
	private CharacterActionStatus status = CharacterActionStatus.Inactive;

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
			return id;
		}
		set {
			id = value;
		}
	}
	
	public CharacterActionStatus Status {
		get {
			return this.status;
		}
		set {
			if (this.status != value)
			{
				this.status = value;
				CallEvent(0, this);
			}
		}
	}
	
	public GameObject Source {
		get {
			return this.source;
		}
		set {
			this.source = value;
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
