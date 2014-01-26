using UnityEngine;
using System.Collections;

public class SchnopAnimController : MonoBehaviour {

	public enum EmoState {
		Neutral,
		Mad,
		Scared
	};
	
	public enum ActionState {
		Idle,
		Walk,
		Hit,
		Die
	};

	private Animator animator;
	private Animation animation;

	public EmoState emoState = EmoState.Neutral;
	private EmoState cachedEmostate;

	public ActionState actionState = ActionState.Idle;
	private ActionState cachedActionState;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		animation = GetComponent<Animation> ();
		UpdateEmoState ();
		UpdateActionState ();
	}
	
	// Update is called once per frame
	void Update () {
		if (cachedEmostate != emoState) {
			cachedEmostate = emoState;

			UpdateEmoState();
		}

		if(cachedActionState != actionState) {
			cachedActionState = actionState;

			UpdateActionState();
		}
	}

	void UpdateEmoState() {
		string triggerName = string.Empty;
		string stateName = string.Empty;
		string clipName = string.Empty;

		switch (emoState) {
		case EmoState.Neutral:
			triggerName = "ToNeutral";
			stateName = "Neutral";
			clipName = "Schnop_Emo_Neutral";
			break;
		case EmoState.Mad:
			triggerName = "ToMad";
			stateName = "Mad";			
			clipName = "Schnop_Emo_Mad";
			break;
		case EmoState.Scared:
			triggerName = "ToScared";
			stateName = "Scared";
			clipName = "Schnop_Emo_Scared";
			break;
		default:
			break;
		}


		if(!string.IsNullOrEmpty(triggerName))
		   animator.SetTrigger (triggerName);
	}

	void UpdateActionState() {
		string triggerName = string.Empty;

		switch (actionState) {
		case ActionState.Die:
			triggerName = "Die";
			break;

		case ActionState.Hit:
			triggerName = "Hit";
			break;

		case ActionState.Idle:
			triggerName = "Idle";
			break;

		case ActionState.Walk:
			triggerName = "Walk";
			break;
		default:
			break;
		}


		animator.SetTrigger (triggerName);
	}
}
