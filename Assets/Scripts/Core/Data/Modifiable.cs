using System.Collections.Generic;

public class Modifiable<T> : IModifiable<T> {

	private ISingleOperandModifier<T> modifiers = new GetterModifier<T>();

	#region IModifiable implementation
	public TypedValue32<string> ID { get;set; }

	public ISingleOperandModifier<T> Modifiers {
		get {
			return this.modifiers;
		}
	}

	public T BaseValue { get;set; }

	public T FinalValue {
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
