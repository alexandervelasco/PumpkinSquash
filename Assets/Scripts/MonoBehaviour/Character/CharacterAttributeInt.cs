using UnityEngine;
using System.Collections;

public class CharacterAttributeInt : EventCallerBehavior, IModifiable<int> {

	//serialized data
	public ModifiableID id;
	public int defaultValue = 0;

	private ISingleOperandModifier<TypedValue32<ModifiableType, int>> modifiers = null;
	private TypedValue32<ModifiableType, int> baseValue = 0;

	#region IModifiable implementation

	public ModifiableID ID {
		get {
			return this.id;
		}
		set {
			this.id = value;
		}
	}

	public ISingleOperandModifier<TypedValue32<ModifiableType, int>> Modifiers {
		get {
			return this.modifiers;
		}
	}

	public TypedValue32<ModifiableType, int> BaseValue {
		get {
			return baseValue;
		}
		set {
			baseValue = value;
			CallEvent(1, this, this);
		}
	}

	public TypedValue32<ModifiableType, int> FinalValue {
		get {
			CallEvent(0, this, this);
			return Modifiers.Resolve(BaseValue);
		}
	}

	#endregion

	// Use this for initialization
	public void Start () {
		this.modifiers = new GetterModifier<TypedValue32<ModifiableType, int>>();
		this.BaseValue = defaultValue;
	}
	
	// Update is called once per frame
	public void Update () {
	
	}
}
