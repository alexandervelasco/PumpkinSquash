using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using PathologicalGames;

[RequireComponent(typeof(TransformConstraint))]
public class CharacterAttributeIntBar : EventReceiverBehavior, IGameObjectSource {

	//serialized data
	public GameObject source = null;
	public ModifiableID attributeID = ModifiableID.None;
	public int minimumValue = 0;
	public Gradient colorGradient = null;
	public string currentValueGOName = string.Empty;
	
	private int maximumValue = 100;
	private TransformConstraint transformConstraint = null;
	private Image barImage = null;

	public GameObject Source {
		get {
			if (source == null)
				source = gameObject;
			return source;
		}
		set {
			source = value;
		}
	}

	private Image BarImage {
		get {
			if (barImage == null)
			{
				Image[] images = GetComponentsInChildren<Image>();
				barImage = images.FirstOrDefault(image => image.gameObject.name.Equals(currentValueGOName));
			}
			return barImage;
		}
	}

	// Use this for initialization
	void Start () {
		transformConstraint = gameObject.GetComponent<TransformConstraint>();
		if (transformConstraint != null)
			transformConstraint.target = Source.transform;
		IModifiable<int>[] sourceAttributes = Source.GetComponents<CharacterAttributeInt>();
		IModifiable<int> sourceAttribute = sourceAttributes.FirstOrDefault (attribute => attribute.ID == attributeID);
		if (sourceAttribute != null)
		{
			maximumValue = sourceAttribute.FinalValue;
			BarImage.color = colorGradient.Evaluate(1);
			BarImage.transform.localScale = new Vector3(1,
			                                            BarImage.transform.localScale.y,
			                                            BarImage.transform.localScale.z);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region implemented abstract members of EventReceiverBehavior

	public override void ReceiveEvent (string eventName, object args, object sender)
	{
		IModifiable<int> modifiable = args as IModifiable<int>;
		MonoBehaviour senderBehavior = sender as MonoBehaviour;
		if (modifiable != null && senderBehavior != null && 
		    modifiable.ID == attributeID && senderBehavior.gameObject == Source)
		{
			float attributePercentage = Mathf.InverseLerp((float)minimumValue, (float)maximumValue,
			                                              Mathf.Clamp(modifiable.FinalValue, minimumValue, maximumValue));
			BarImage.transform.localScale = new Vector3(attributePercentage,
			                                            BarImage.transform.localScale.y,
			                                            BarImage.transform.localScale.z);
			BarImage.color = colorGradient.Evaluate(attributePercentage);
		}
	}

	#endregion
}
