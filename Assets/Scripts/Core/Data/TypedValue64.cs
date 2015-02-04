using System;

public struct TypedValue64<T> where T : struct {
	public ulong Type { get; private set; }
	public T Value { get; private set; }

	public TypedValue64 (ulong type, T value) : this()
	{
		this.Type = type; this.Value = value;
	}

	public TypedValue64 (T value) : this()
	{
		this.Type = 0; this.Value = value;
	}

	public static implicit operator T (TypedValue64<T> typedValue)
	{
		return typedValue.Value;
	}

	public static implicit operator TypedValue64<T> (T value)
	{
		return new TypedValue64<T>(value);
	}
}
