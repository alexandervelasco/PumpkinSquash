using UnityEngine;
using System;
using System.Collections.Generic;

public class GetterModifier<T> : ISingleOperandModifier<T> {

	private Dictionary<Guid, Func<T, T>> modifierDict = new Dictionary<Guid, Func<T, T>>();
	private List<List<Guid>> priorityGroups = new List<List<Guid>>();
	private Dictionary<Guid, int> priorityGroupDict = new Dictionary<Guid, int>();
	private Dictionary<Guid, bool> removeOnResolve = new Dictionary<Guid, bool> ();

	#region ISingleOperandModifier implementation

	public void SetModifier (int priorityGroup, Guid id, Func<T, T> modifier, bool removeOnResolve = false)
	{
		int priorityGroupIndex = priorityGroup;
		if (!priorityGroups.HasValue(priorityGroup))
			priorityGroupIndex = priorityGroups.Set(priorityGroup, new List<Guid>());
		if (!priorityGroups[priorityGroupIndex].Contains(id))
			priorityGroups[priorityGroupIndex].Add(id);
		modifierDict[id] = modifier;
		priorityGroupDict[id] = priorityGroupIndex;
		this.removeOnResolve[id] = removeOnResolve;
	}
	public void RemoveModifier(Guid id)
	{
		modifierDict.Remove(id);
		priorityGroups[priorityGroupDict[id]].Remove(id);
		priorityGroupDict.Remove(id);
		removeOnResolve.Remove(id);
	}
	public T Resolve (T operand1)
	{
		List<Guid> toBeRemoved = new List<Guid> ();
		T currentValue = operand1;
		foreach (List<Guid> priorityGroup in priorityGroups)
		{
			foreach (Guid modifierId in priorityGroup)
			{
				currentValue = modifierDict[modifierId](currentValue);
				if (removeOnResolve[modifierId])
					toBeRemoved.Add(modifierId);
			}
		}
		foreach (Guid id in toBeRemoved)
			RemoveModifier(id);
		return currentValue;
	}
	#endregion
}
