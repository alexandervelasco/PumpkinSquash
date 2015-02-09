using System;

public enum ModifiableID
{
	None,
	AttributeHP
}

[Flags]
public enum ModifiableType
{
	None = 0,
	Damage = 1
}

public interface IModifiable<T> where T : IConvertible {
	ModifiableID ID { get;set; }
	ISingleOperandModifier<TypedValue32<ModifiableType,T>> Modifiers { get; }
	TypedValue32<ModifiableType,T> BaseValue { get; set; }
	TypedValue32<ModifiableType,T> FinalValue { get; }
}
