using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

[RequireComponent(typeof(Button))]
public class UIButtonAction : EventCallerBehavior, IUIAction {

	//serialized data
	public GameObject source = null;
	public UIActionID id;

	private UIActionStatus status = UIActionStatus.Enabled;
	private Button button = null;

	#region IUIAction implementation
	public UIActionID ID {
		get {
			return this.id;
				}
		set {
			this.id = value;
				}
	}
	public UIActionStatus Status {
		get {
			return this.status;
		}
		set {
			if (this.status != value)
			{
				this.status = value;
				CallEvent(0, this);
			}
		}
	}
	#endregion
	#region IGameObjectSource implementation
	public GameObject Source {
		get {
			if (source == null)
				source = gameObject;
			return this.source;
		}
		set {
			this.source = value;
		}
	}
	#endregion

	// Use this for initialization
	void Start () {
		button = gameObject.GetComponent<Button>();
		if (button != null)
		{
			button.onClick.AddListener(() => { OnClick (); } );
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClick()
	{
		Status = UIActionStatus.Clicked;
		Status = UIActionStatus.Enabled;
	}
}
