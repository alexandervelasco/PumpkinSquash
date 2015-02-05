using UnityEngine;
using System;
using System.Collections.Generic;

public class ChainModifier<T> : IDoubleOperandModifier<T> {

	private List<List<Func<T, T, T>>> modifiers = new List<List<Func<T, T, T>>>();

	#region IDoubleOperandModifier implementation
	public int AddModifier (int priorityGroup, System.Func<T, T, T> modifier)
	{
		int priorityGroupIndex = priorityGroup;
		if (!modifiers.HasValue (priorityGroup))
			priorityGroupIndex = modifiers.Set(priorityGroup, new List<Func<T, T, T>>());
		modifiers [priorityGroupIndex].Add (modifier);
		return modifiers [priorityGroupIndex].IndexOf (modifier);
	}
	public void SetModifier (int priorityGroup, int id, System.Func<T, T, T> modifier)
	{
		int priorityGroupIndex = priorityGroup;
		if (!modifiers.HasValue (priorityGroup))
			priorityGroupIndex = modifiers.Set(priorityGroup, new List<Func<T, T, T>>());
		modifiers [priorityGroupIndex] [id] = modifier;
	}
	public void RemoveModifier (int priorityGroup, int id)
	{
		if (modifiers.HasValue (priorityGroup) && modifiers [priorityGroup].HasValue (id))
			modifiers [priorityGroup].RemoveAt (id);
	}
	public T Resolve (T operand1, T operand2)
	{
		T currentValue = operand1;
		foreach (List<Func<T, T, T>> priorityGroup in modifiers)
						foreach (Func<T, T, T> modifier in priorityGroup)
								currentValue = modifier (currentValue, operand2);

		return currentValue;
	}
	#endregion
}
