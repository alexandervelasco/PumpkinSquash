using UnityEngine;
using System.Collections.Generic;

public interface IGameEventCaller {
	IList<string> CalledEvents { get; }
}
