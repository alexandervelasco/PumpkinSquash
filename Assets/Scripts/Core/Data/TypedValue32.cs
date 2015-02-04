using System;

public struct TypedValue32<T>
where T : struct {
	public int Type { get; private set; }
	public T Value { get; private set; }

	public TypedValue32 (int type, T value) : this()
	{
		this.Type = type; this.Value = value;
	}

	public TypedValue32 (T value) : this()
	{
		this.Type = 0; this.Value = value;
	}

	public override bool Equals (object obj)
	{
		bool result = false;
		if (obj is TypedValue32<T>)
			result = this == (TypedValue32<T>)obj;
		return result;
	}

	public static implicit operator T (TypedValue32<T> typedValue)
	{
		return typedValue.Value;
	}

	public static implicit operator TypedValue32<T>(T value)
	{
		return new TypedValue32<T>(value);
	}

	public static bool operator == (TypedValue32<T> op1, TypedValue32<T> op2)
	{
		return (op1.Type == op2.Type && op1.Value.Equals(op2.Value));
	}

	public static bool operator != (TypedValue32<T> op1, TypedValue32<T> op2)
	{
		return (op1.Type != op2.Type || !op1.Value.Equals(op2.Value));
	}

	public static TypedValue32<T> operator & (TypedValue32<T> op1, int op2)
	{
		return new TypedValue32<T>(op1.Type & op2, op1.Value);
	}

	public static TypedValue32<T> operator | (TypedValue32<T> op1, int op2)
	{
		return new TypedValue32<T>(op1.Type | op2, op1.Value);
	}

	public static TypedValue32<T> operator ^ (TypedValue32<T> op1, int op2)
	{
		return new TypedValue32<T>(op1.Type ^ op2, op1.Value);
	}

	public static TypedValue32<T> operator << (TypedValue32<T> op1, int op2)
	{
		return new TypedValue32<T>(op1.Type << op2, op1.Value);
	}

	public static TypedValue32<T> operator >> (TypedValue32<T> op1, int op2)
	{
		return new TypedValue32<T>(op1.Type >> op2, op1.Value);
	}
}
