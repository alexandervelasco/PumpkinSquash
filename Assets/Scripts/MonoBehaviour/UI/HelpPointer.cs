using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using PathologicalGames;

[RequireComponent(typeof(TransformConstraint))]
public class HelpPointer : EventReceiverBehavior, IGameObjectSource, ITargeted<GameObject> {

	//serialized data
	public GameObject source = null;
	public UIActionID toggleActionID = UIActionID.HelpOverlay;
	public CharacterActionID resetActionID = CharacterActionID.None;
	public string tapRepeatAnimationName = string.Empty;
	public string unitPoolName = string.Empty;
	public Camera worldCamera = null;

	private bool render = false;
	private List<GameObject> targets = null;
	List<Renderer> renderers = null;
	Animator animator = null;
	private SpawnPool unitPool = null;
	private TransformConstraint transformConstraint = null;

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
		if (source == null)
			source = gameObject;
		targets = new List<GameObject>();
		renderers = new List<Renderer>();
		renderers.AddRange(GetComponents<Renderer>());
		renderers.AddRange(GetComponentsInChildren<Renderer>());
		foreach (Renderer renderer in renderers)
			renderer.enabled = render;
		animator = GetComponent<Animator>();
		if (animator == null)
			animator = GetComponentInChildren<Animator>();
		unitPool = PoolManager.Pools[unitPoolName];
		transformConstraint = GetComponent<TransformConstraint>();
	}
	
	// Update is called once per frame
	void Update () {
		if (render)
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
				if (worldCamera != null)
				{
					Vector3 viewportPoint = worldCamera.WorldToViewportPoint(targetTransform.position);
					Vector2 viewportXY = new Vector2(viewportPoint.x, viewportPoint.y);
				}
			}
		}
	}

	#region implemented abstract members of EventReceiverBehavior

	public override void ReceiveEvent (string eventName, object args, object sender)
	{
		IUIAction uiAction = args as IUIAction;
		ICharacterAction characterAction = args as ICharacterAction;
		if (uiAction != null && uiAction.ID == toggleActionID && (uiAction.Status & UIActionStatus.Clicked) == UIActionStatus.Clicked)
		{
			render = !render;
			foreach (Renderer renderer in renderers)
				renderer.enabled = render;
		}
		else if (characterAction != null && Targets.Contains(characterAction.Source))
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
}
