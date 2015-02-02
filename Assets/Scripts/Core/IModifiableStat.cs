using UnityEngine;
using System;
using System.Collections.Generic;

public interface IModifiableStat<T> where T : struct {
	T Base { get;set; }
	T Modified { get;set;}
	int AddModifier (int priorityGroup, Func<T, T, T> modifier);
	void SetModifier (int priorityGroup, int modifierId, Func<T, T, T> modifier);
	void RemoveModifier (int priorityGroup, int modifierId);
}
