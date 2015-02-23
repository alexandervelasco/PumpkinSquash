using UnityEngine;
using System.Collections;

public class CharacterCollideAction : EventCallerBehavior, ICharacterAction, ITargeted<GameObject> {

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

	#region ITargeted implementation

	public System.Collections.Generic.List<GameObject> Targets {
		get {
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
}
