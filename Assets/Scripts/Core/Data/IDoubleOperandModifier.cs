using UnityEngine;
using System;
using System.Collections.Generic;

public interface IDoubleOperandModifier<T> where T : struct {
	int AddModifier (int priorityGroup, Func<T, T, T> modifier);
	void SetModifier (int priorityGroup, int id, Func<T, T, T> modifier);
	void RemoveModifier (int priorityGroup, int id);
	T Resolve (T operand1, T operand2);
}
