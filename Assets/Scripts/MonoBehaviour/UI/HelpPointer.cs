using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using PathologicalGames;

[RequireComponent(typeof(RectTransform))]
public class HelpPointer : EventReceiverBehavior, IGameObjectSource, ITargeted<GameObject> {

	//serialized data
	public GameObject source = null;
	public UIActionID toggleActionID = UIActionID.HelpOverlay;
	public CharacterActionID resetActionID = CharacterActionID.None;
	public string tapRepeatAnimationName = string.Empty;
	public string renderAnimationName = string.Empty;
	public string unitPoolName = string.Empty;
	public Camera pointerCamera = null;

	private bool render = false;
	private List<GameObject> targets = null;
	Animator animator = null;
	private SpawnPool unitPool = null;
	private RectTransform rectTransform = null;
	private RectTransform canvasRectTransform = null;

	#region IGameObjectSource implementation

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

	#endregion

	#region ITargeted implementation

	public List<GameObject> Targets {
		get {
			if (targets == null)
				targets = new List<GameObject>();
			return targets;
		}
	}

	#endregion

	// Use this for initialization
	void Start () {
		rectTransform = GetComponent<RectTransform>();
		if (source == null)
			source = gameObject;
		targets = new List<GameObject>();
		animator = GetComponent<Animator>();
		if (animator == null)
			animator = GetComponentInChildren<Animator>();
		unitPool = PoolManager.Pools[unitPoolName];
		animator.SetBool(renderAnimationName, render);
		Canvas canvas = GetComponentInParent<Canvas>();
		if (canvas != null)
			canvasRectTransform = canvas.GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		if (render && canvasRectTransform != null)
		{
			if (Targets.Count == 0)
			{
				Transform closestTransform = GetClosestTransform(Source.transform, unitPool);
				if (closestTransform != null)
					Targets.Add(closestTransform.gameObject);
			}
			if (Targets.Count > 0)
			{
				Transform targetTransform = Targets[0].transform;
				Vector2 screenPosition = GetRectTransformPosition(targetTransform, canvasRectTransform, pointerCamera);
				rectTransform.anchoredPosition = screenPosition;
			}
		}
		else
			Targets.Clear();
	}

	#region implemented abstract members of EventReceiverBehavior

	public override void ReceiveEvent (string eventName, object args, object sender)
	{
		IUIAction uiAction = args as IUIAction;
		ICharacterAction characterAction = args as ICharacterAction;
		if (uiAction != null && uiAction.ID == toggleActionID && (uiAction.Status & UIActionStatus.Clicked) == UIActionStatus.Clicked)
		{
			render = !render;
			animator.SetBool(renderAnimationName, render);
		}
		else if (characterAction != null && characterAction.ID == resetActionID && 
		         Targets.Contains(characterAction.Source) && (characterAction.Status & CharacterActionStatus.Active) == CharacterActionStatus.Active)
			Targets.Clear();
	}

	#endregion

	private Transform GetClosestTransform (Transform source, IEnumerable<Transform> targets)
	{
		float smallestSquareDistance = float.PositiveInfinity;
		Transform result = null;
		foreach (Transform target in targets)
		{
			float squareDistance = (target.position - source.position).sqrMagnitude;
			if (squareDistance < smallestSquareDistance)
			{
				result = target;
				smallestSquareDistance = squareDistance;
			}
		}

		return result;
	}

	private Vector2 GetRectTransformPosition(Transform transform, RectTransform rectTransform, Camera camera)
	{
		Vector2 relativePosition = Vector2.zero;
		Vector3 screenPoint = camera.WorldToScreenPoint(transform.position);
		float rectWidth = rectTransform.rect.width;
		float rectHeight = rectTransform.rect.height;
		float screenWidth = (rectTransform.anchoredPosition.x / rectTransform.pivot.x);
		float screenHeight = (rectTransform.anchoredPosition.y / rectTransform.pivot.y);
		float xPercentage = screenPoint.x / screenWidth;
		float yPercentage = screenPoint.y / screenHeight;
		Vector2 absolutePosition = new Vector2 (rectWidth * xPercentage, yPercentage * rectHeight);
		relativePosition = absolutePosition + rectTransform.rect.position; 
		return relativePosition;    
	}
}
