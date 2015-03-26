using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[AddComponentMenu("UI/Effects/Linear Gradient")]
public class LinearGradient : BaseVertexEffect {

	//serialized data
	[SerializeField]
	private Gradient gradient = new Gradient();
	[SerializeField]
	private bool horizontal = false;

	public Gradient Gradient {
		get {
			return gradient;
		}
		set {
			gradient = value;
		}
	}
	
	public override void ModifyVertices(List<UIVertex> vertexList) {
		if (!IsActive()) {
			return;
		}

		float min = float.MaxValue;
		float max = float.MinValue;
		foreach (UIVertex uiVertex in vertexList)
		{
			if (horizontal)
			{
				min = Mathf.Min(min, uiVertex.position.x);
				max = Mathf.Max(max, uiVertex.position.x);
			}
			else
			{
				min = Mathf.Min(min, uiVertex.position.y);
				max = Mathf.Max(max, uiVertex.position.y);
			}
		}
		for (int i = 0; i < vertexList.Count; i++)
		{
			UIVertex uiVertex = vertexList[i];
			float lerp = horizontal ? uiVertex.position.x : uiVertex.position.y;
			uiVertex.color = gradient.Evaluate(Mathf.InverseLerp(min, max, lerp));
			vertexList[i] = uiVertex;

		}
	}
}