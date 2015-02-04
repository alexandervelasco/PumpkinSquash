using UnityEngine;
using System.Collections.Generic;

public static class IDictionaryExtensions {

	public static void Load<TKey, TValue>(this IDictionary<TKey, TValue> dict, IEnumerable<TKey> keys, IEnumerable<TValue> values)
	{
		dict.Clear();
		IEnumerator<TKey> keyEnumerator = keys.GetEnumerator();
		IEnumerator<TValue> valueEnumerator = values.GetEnumerator();
		while (keyEnumerator.MoveNext()) 
		{
			if (!dict.ContainsKey(keyEnumerator.Current) && valueEnumerator.MoveNext())
				dict.Add(keyEnumerator.Current, valueEnumerator.Current);
		}
	}

	public static void Save<TKey, TValue>(this IDictionary<TKey, TValue> dict, ICollection<TKey> keys, ICollection<TValue> values)
	{
		if (keys != null && values != null)
		{
			keys.Clear(); values.Clear();
			foreach (KeyValuePair<TKey, TValue> kvp in dict)
			{
				keys.Add(kvp.Key);
				values.Add(kvp.Value);
			}
		}

	}
}
