using System;

[Flags]
public enum ModifiableType
{
	None = 0
}

public interface IModifiable<T> where T : IConvertible {
	string ID { get;set; }
	ISingleOperandModifier<TypedValue32<ModifiableType,T>> Modifiers { get; }
	TypedValue32<ModifiableType,T> BaseValue { get; set; }
	TypedValue32<ModifiableType,T> FinalValue { get; }
}
