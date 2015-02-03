using System.Collections.Generic;
using System.Linq;

public static class IListExtensions {
	public static int Set<T> (this IList<T> list, int index, T item)
	{
		if (index < 0)
			list.Insert (0, item);
		else if (index >= list.Count)
			list.Add(item);
		else
			list[index] = item;

		return list.IndexOf(item);
	}

	public static bool HasValue<T> (this IList<T> list, int index)
	{
		bool result = true;

		if (index < 0 || index >= list.Count)
			result = false;
		else
			result = (list[index] != null);

		return result;
	}
}
