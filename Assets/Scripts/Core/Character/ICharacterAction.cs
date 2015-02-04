﻿using UnityEngine;
using System;
using System.Collections.Generic;

[Flags]
public enum CharacterActionStatus
{
	None = 0,
	Inactive = 1,
	Started = 2,
	Active = 4,
	Cancelled = 8,
	Ended = 16
};

public interface ICharacterAction {
	string ID { get;set; }
	ulong Type { get;set; }
	CharacterActionStatus Status { get;set; }
	GameObject Source { get; set; }
}
