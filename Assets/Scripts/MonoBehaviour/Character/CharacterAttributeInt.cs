using UnityEngine;
using System.Collections;

public class CharacterAttributeInt : EventCallerBehavior, IModifiable<TypedValue32<int>> {

	//serialized data
	public string id = string.Empty;
	public int defaultValue = 0;

	private ISingleOperandModifier<TypedValue32<int>> modifiers = null;
	private TypedValue32<int> baseValue = 0;

	#region IModifiable implementation

	public TypedValue32<string> ID {
		get {
			return this.id;
		}
		set {
			this.id = value;
		}
	}

	public ISingleOperandModifier<TypedValue32<int>> Modifiers {
		get {
			return this.modifiers;
		}
	}

	public TypedValue32<int> BaseValue {
		get {
			return baseValue;
		}
		set {
			baseValue = value;
			CallEvent(1, this);
		}
	}

	public TypedValue32<int> FinalValue {
		get {
			CallEvent(0, this);
			return Modifiers.Resolve(BaseValue);
		}
	}

	#endregion

	// Use this for initialization
	void Start () {
		this.modifiers = new GetterModifier<TypedValue32<int>>();
		this.BaseValue = defaultValue;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
