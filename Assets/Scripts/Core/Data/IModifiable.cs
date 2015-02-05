public interface IModifiable<T> {
	TypedValue32<string> ID { get;set; }
	ISingleOperandModifier<T> Modifiers { get; }
	T BaseValue { get; set; }
	T FinalValue { get; }
}
