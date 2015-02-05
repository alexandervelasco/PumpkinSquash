using System;

public struct TypedValue32<T, U> : IConvertible
where T : IConvertible
where U : IConvertible {
	public T Type { get; private set; }
	public U Value { get; private set; }

	public TypedValue32 (T type, U value) : this()
	{
		if (!typeof(T).IsEnum)
			throw new Exception("T must be an enum");
		else
			this.Type = type;
		this.Value = value;
	}

	public TypedValue32 (U value) : this()
	{		
		this.Type = default(T); this.Value = value;
	}

	public override bool Equals (object obj)
	{
		bool result = false;
		if (obj is TypedValue32<T,U>)
			result = this == (TypedValue32<T,U>)obj;
		return result;
	}

	public static implicit operator U (TypedValue32<T,U> typedValue)
	{
		return typedValue.Value;
	}

	public static implicit operator TypedValue32<T,U>(U value)
	{
		return new TypedValue32<T,U>(value);
	}

	public static bool operator == (TypedValue32<T,U> op1, TypedValue32<T,U> op2)
	{
		return (op1.Type.Equals(op2.Type) && op1.Value.Equals(op2.Value));
	}

	public static bool operator != (TypedValue32<T,U> op1, TypedValue32<T,U> op2)
	{
		return (!op1.Type.Equals(op2.Type) || !op1.Value.Equals(op2.Value));
	}

	#region IConvertible implementation

	public TypeCode GetTypeCode ()
	{
		return Value.GetTypeCode();
	}

	public bool ToBoolean (IFormatProvider provider)
	{
		return Value.ToBoolean(provider);
	}

	public char ToChar (IFormatProvider provider)
	{
		return Value.ToChar(provider);
	}

	public sbyte ToSByte (IFormatProvider provider)
	{
		return Value.ToSByte(provider);
	}

	public byte ToByte (IFormatProvider provider)
	{
		return Value.ToByte(provider);
	}

	public short ToInt16 (IFormatProvider provider)
	{
		return Value.ToInt16(provider);
	}

	public ushort ToUInt16 (IFormatProvider provider)
	{
		return Value.ToUInt16(provider);
	}

	public int ToInt32 (IFormatProvider provider)
	{
		return Value.ToInt32(provider);
	}

	public uint ToUInt32 (IFormatProvider provider)
	{
		return Value.ToUInt32(provider);
	}

	public long ToInt64 (IFormatProvider provider)
	{
		return Value.ToInt64(provider);
	}

	public ulong ToUInt64 (IFormatProvider provider)
	{
		return Value.ToUInt64(provider);
	}

	public float ToSingle (IFormatProvider provider)
	{
		return Value.ToSingle(provider);
	}

	public double ToDouble (IFormatProvider provider)
	{
		return Value.ToDouble(provider);
	}

	public decimal ToDecimal (IFormatProvider provider)
	{
		return Value.ToDecimal(provider);
	}

	public DateTime ToDateTime (IFormatProvider provider)
	{
		return Value.ToDateTime(provider);
	}

	public string ToString (IFormatProvider provider)
	{
		return Value.ToString(provider);
	}

	public object ToType (System.Type conversionType, IFormatProvider provider)
	{
		return Value.ToType(conversionType, provider);
	}

	#endregion
}
