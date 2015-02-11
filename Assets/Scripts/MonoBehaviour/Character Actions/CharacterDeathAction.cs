using UnityEngine;
using System.Collections.Generic;
using System;

public class CharacterDeathAction : EventTransceiverBehavior, ICharacterAction {

	//serialized data
	public ModifiableID modifiableID = ModifiableID.None;
	public int deathThreshold = 0;
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
			if (source == null)
				source = gameObject;
			return this.source;
		}
		set {
			this.source = value;
		}
	}
	#endregion

	// Use this for initialization
	public void Start () {
		if (source == null)
			source = gameObject;
	}
	
	// Update is called once per frame
	public void Update () {
	
	}

	#region implemented abstract members of EventTransceiverBehavior

	public override void ReceiveEvent (string eventName, object args, object sender)
	{
		IModifiable<int> modifiable = args as IModifiable<int>;
		MonoBehaviour sourceBehavior = sender as MonoBehaviour;
		ICharacterAction characterAction = args as ICharacterAction;
		if (modifiable != null && sourceBehavior != null && sourceBehavior.gameObject == Source &&
						modifiable.ID == modifiableID) {
						if (modifiable.FinalValue.Value <= deathThreshold) {
								Status = CharacterActionStatus.Started;
								Status = CharacterActionStatus.Active;
						} else if (modifiable.FinalValue.Value <= deathThreshold && Status != CharacterActionStatus.Ended) {
								Status = CharacterActionStatus.Ended;
								Status = CharacterActionStatus.Inactive;
						}
				} else if (characterAction != null && characterAction != this && Status == CharacterActionStatus.Active)
						characterAction.Status = CharacterActionStatus.Cancelled;
	}

	#endregion
}
