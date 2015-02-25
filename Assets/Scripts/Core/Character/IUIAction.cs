using UnityEngine;
using System;
using System.Collections.Generic;

public enum UIActionID
{
	None,
	WorldReset,
	WorldPause,
	HelpOverlay
}

[Flags]
public enum UIActionStatus
{
	None = 0,
	Disabled = 1,
	Enabled = 2,
	Highlighted = 4,
	Clicked = 8,
	Changed = 16
};

public interface IUIAction : IGameObjectSource {
	UIActionID ID { get; set; }
	UIActionStatus Status { get; set; }
}
