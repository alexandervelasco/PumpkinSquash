using System.Collections.Generic;
using System.Linq;

public static class IEnumerableExtensions {

	public static T Random<T>(this IEnumerable<T> enumerable)
	{
		T result = default(T);

		ThreadSafeRandom r = new ThreadSafeRandom();
		var list = enumerable as IList<T> ?? enumerable.ToList();
		if (list.Count > 0)
			result = list.ElementAt(r.Next(0, list.Count()));

		return result;
	}
}
