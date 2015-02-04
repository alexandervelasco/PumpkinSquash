using UnityEngine;
using System.Collections.Generic;

public enum CharacterActionStatus {Inactive,Started,Active,Cancelled,Ended};

public interface ICharacterAction {
	string ID { get;set; }
	ulong Type { get;set; }
	CharacterActionStatus Status { get;set; }
	GameObject Source { get; set; }
}
