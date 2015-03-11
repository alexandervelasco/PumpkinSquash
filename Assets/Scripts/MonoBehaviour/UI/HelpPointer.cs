using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
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
	public string arrowAnimationName = string.Empty;
	public string unitPoolName = string.Empty;
	public Sprite pointerSprite = null;
	public Sprite arrowSprite = null;

	private bool render = false;
	private Camera pointerCamera = null;
	private List<GameObject> targets = null;
	Animator animator = null;
	private SpawnPool unitPool = null;
	private RectTransform rectTransform = null;
	private RectTransform canvasRectTransform = null;
	private Image image = null;

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
		image = GetComponentInChildren<Image>();
		if (animator == null)
			animator = GetComponentInChildren<Animator>();
		unitPool = PoolManager.Pools[unitPoolName];
		animator.SetBool(renderAnimationName, render);
		Canvas canvas = GetComponentInParent<Canvas>();
		if (canvas != null)
		{
			canvasRectTransform = canvas.GetComponent<RectTransform>();
			pointerCamera = canvas.worldCamera;
		}
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
				rectTransform.anchoredPosition = ClampToRectTransform(screenPosition, canvasRectTransform);
				if (canvasRectTransform.rect.Contains(screenPosition))
				{
					animator.SetBool(arrowAnimationName, false);
					animator.SetBool(tapRepeatAnimationName, true);
					image.sprite = pointerSprite;
					rectTransform.localRotation = Quaternion.identity;
				}
				else
				{
					animator.SetBool(tapRepeatAnimationName, false);
					animator.SetBool(arrowAnimationName, true);
					image.sprite = arrowSprite;
					float rotationDegrees = Vector2.Angle(Vector2.right, screenPosition.normalized);
					Vector3 rotationDirection = Vector3.Cross(Vector2.right, screenPosition.normalized);
					if (rotationDirection.z < 0)
						rotationDegrees = 360.0f - rotationDegrees;
					rectTransform.localRotation = Quaternion.AngleAxis(rotationDegrees, Vector3.forward);
				}
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
		Vector3 viewportPoint = camera.WorldToViewportPoint(transform.position);
		if (viewportPoint.z < 0.0f)
			viewportPoint *= -1;
		float rectWidth = rectTransform.rect.width;
		float rectHeight = rectTransform.rect.height;
		Vector2 absolutePosition = new Vector2 (rectWidth * viewportPoint.x, rectHeight * viewportPoint.y);
		relativePosition = absolutePosition + rectTransform.rect.position; 
		return relativePosition;    
	}

	private Vector2 ClampToRectTransform(Vector2 point, RectTransform rectTransform)
	{
		float xClamp = Mathf.Clamp(point.x, rectTransform.rect.x, rectTransform.rect.x + rectTransform.rect.width);
		float yClamp = Mathf.Clamp(point.y, rectTransform.rect.y, rectTransform.rect.y + rectTransform.rect.height);
		return new Vector2(xClamp, yClamp);
	}
}
