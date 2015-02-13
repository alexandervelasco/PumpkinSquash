﻿using UnityEngine;
using System;
using System.Collections.Generic;

public interface ISingleOperandModifier<T> {
	void SetModifier (int priorityGroup, Guid id, Func<T, T> modifier);
	void RemoveModifier (Guid id);
	T Resolve (T operand1);
}
