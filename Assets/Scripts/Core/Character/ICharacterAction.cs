using UnityEngine;
using System;
using System.Collections.Generic;

public enum CharacterActionID
{
	None,
	CharacterMove,
	CharacterJump,
	CharacterDeath,
	CharacterTimedSuicideBomb,
	CharacterAttributeIntOverTime,
	CharacterAttack,
	CharacterAttributeIntOnKill
}

[Flags]
public enum CharacterActionStatus
{
	None = 0,
	Inactive = 1,
	Started = 2,
	Active = 4,
	Cancelled = 8,
	Ended = 16,
	Windup = 32,
	Recovery = 64
};

public interface ICharacterAction : IGameObjectSource {
	CharacterActionID ID { get;set; }
	CharacterActionStatus Status { get;set; }
	U GetProperty<T, U>(T propertyId) where T : IConvertible;
	void SetProperty<T, U>(T propertyId, U propertyValue) where T : IConvertible;
}
