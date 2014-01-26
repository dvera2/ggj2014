using UnityEngine;
using System.Collections;

public class CthjujuAnimController : MonoBehaviour {

	public enum ActionState {
		Idle,
		Attack,
		Die
	}

	public Animator animator;
	
	public enum EmoState {
		Twitch
	}
	
	public ActionState actionState = ActionState.Idle;
	private ActionState cachedActionState;
	
	public EmoState emoState = EmoState.Twitch;
	private EmoState cachedEmoState = EmoState.Twitch;
	
	// Use this for initialization
	void Start () {
		if (!animator)	animator = GetComponent<Animator> ();
		
		UpdateActionState ();
		UpdateEmoState ();
	}
	
	// Update is called once per frame
	void Update () {
		if (cachedActionState != actionState) {
			cachedActionState = actionState;
			
			UpdateActionState();
		}
		
		if(cachedEmoState != emoState) {
			cachedEmoState = emoState;
			
			UpdateEmoState();
		}
	}
	
	void UpdateActionState() {
		
		string trigger = string.Empty;
		switch (actionState) {
		case ActionState.Idle:
			trigger = "Idle";
			break;
		case ActionState.Attack:
			trigger = "Attack";
			break;
		case ActionState.Die:
			trigger = "Die";
			break;
		}
		
		if (!string.IsNullOrEmpty (trigger)) 
			animator.SetTrigger (trigger);
	}
	
	void UpdateEmoState() {
		string trigger = string.Empty;
		switch(emoState) {
		case EmoState.Twitch:
			trigger = "Twitch";
			break;
		}
		
		//if (!string.IsNullOrEmpty (trigger)) 
		//	animator.SetTrigger (trigger);
	}
}
