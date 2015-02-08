using System;
using System.Collections.Generic;

public class Modifiable<T> : IModifiable<T> where T : IConvertible {

	private ISingleOperandModifier<TypedValue32<ModifiableType,T>> modifiers = new GetterModifier<TypedValue32<ModifiableType,T>>();

	#region IModifiable implementation
	public ModifiableID ID { get;set; }

	public ISingleOperandModifier<TypedValue32<ModifiableType,T>> Modifiers {
		get {
			return this.modifiers;
		}
	}

	public TypedValue32<ModifiableType,T> BaseValue { get;set; }

	public TypedValue32<ModifiableType,T> FinalValue {
		get {
			return Modifiers.Resolve(BaseValue);
		}
	}
	#endregion

	public Modifiable (T initialBaseValue)
	{
		BaseValue = initialBaseValue;
	}
}
