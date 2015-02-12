using UnityEngine;
using System.Collections.Generic;

public interface ITargeted<T> {
	List<T> Targets { get; }
}
