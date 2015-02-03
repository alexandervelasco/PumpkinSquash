using UnityEngine;
using System.Collections.Generic;

public interface IGameEventReceiver {
	IList<string> ReceivedEvents { get; }
}
