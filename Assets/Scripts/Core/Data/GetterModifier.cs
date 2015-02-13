using UnityEngine;
using System;
using System.Collections.Generic;

public class GetterModifier<T> : ISingleOperandModifier<T> {

	private Dictionary<Guid, Func<T, T>> modifierDict = new Dictionary<Guid, Func<T, T>>();
	private List<List<Guid>> priorityGroups = new List<List<Guid>>();
	private Dictionary<Guid, int> priorityGroupDict = new Dictionary<Guid, int>();

	#region ISingleOperandModifier implementation

	public void SetModifier (int priorityGroup, Guid id, Func<T, T> modifier)
	{
		int priorityGroupIndex = priorityGroup;
		if (!priorityGroups.HasValue(priorityGroup))
			priorityGroupIndex = priorityGroups.Set(priorityGroup, new List<Guid>());
		priorityGroups[priorityGroupIndex].Add(id);
		modifierDict[id] = modifier;
		priorityGroupDict[id] = priorityGroupIndex;
	}
	public void RemoveModifier(Guid id)
	{
		modifierDict.Remove(id);
		priorityGroups[priorityGroupDict[id]].Remove(id);
		priorityGroupDict.Remove(id);
	}
	public T Resolve (T operand1)
	{
		T currentValue = operand1;
		foreach (List<Guid> priorityGroup in priorityGroups)
			foreach (Guid modifierId in priorityGroup)
				currentValue = modifierDict[modifierId](currentValue);

		return currentValue;
	}
	#endregion
}
