using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using PathologicalGames;

[RequireComponent(typeof(TransformConstraint))]
public class IModifiableFloatProgressBar : EventReceiverBehavior, IGameObjectSource {

	//serialized data
	public GameObject source = null;
	public ModifiableID attributeID = ModifiableID.None;
	public float minimumValue = 0;
	public Gradient colorGradient = null;
	public string currentValueGOName = string.Empty;
	
	private float maximumValue = 0;
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
		maximumValue = minimumValue;
		UpdateBarImage(maximumValue);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region implemented abstract members of EventReceiverBehavior

	public override void ReceiveEvent (string eventName, object args, object sender)
	{
		IModifiable<float> modifiable = args as IModifiable<float>;
		ICharacterAction senderAction = sender as ICharacterAction;
		if (modifiable != null && senderAction != null && 
		    modifiable.ID == attributeID && senderAction.Source == Source)
		{
			float currentValue = modifiable.FinalValue.Value;
			if (currentValue >= maximumValue)
				maximumValue = currentValue;
			UpdateBarImage(currentValue);
		}
	}

	#endregion

	void UpdateBarImage (float currentValue)
	{
		float attributePercentage = Mathf.InverseLerp ((float)minimumValue, (float)maximumValue, Mathf.Clamp (currentValue, minimumValue, maximumValue));
		BarImage.transform.localScale = new Vector3 (attributePercentage, BarImage.transform.localScale.y, BarImage.transform.localScale.z);
		BarImage.color = colorGradient.Evaluate (attributePercentage);
	}
}
