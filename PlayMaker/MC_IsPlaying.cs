//Darkhitori ver# 1.0
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("MecaninControl")]
	[Tooltip("Returns true if clipName, clip or animationData is playing. ")]
	public class MC_IsPlaying : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(MecanimControl))]
		public FsmOwnerDefault gameObject;
	    
		public enum IsPlaying
		{
			clipName,
			clipName_weight,
			clip,
			clip_weight
		}
	    
		public IsPlaying methods;
	    
		[ActionSection("")]
		public FsmString clipName;
	    
		[ActionSection("")]
		public FsmFloat weight;
	    
		[ActionSection("")]
		[ObjectType(typeof(AnimationClip))]
		public FsmObject clip;
	    
		[ActionSection("Return")]
		[UIHint(UIHint.FsmBool)]
		public FsmBool isPlaying;
		[ActionSection("Send Events")]
		public FsmEvent trueEvent;
		public FsmEvent falseEvent;
		
		public FsmBool everyFrame;

		MecanimControl theScript;
  

		public override void Reset()
		{
			gameObject = null;
			methods = IsPlaying.clip;
			clipName = "";
			weight = null;
			clip = null;
			isPlaying = false;
			trueEvent = null;
			falseEvent = null;
			everyFrame = true;
		}
       
		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);

			theScript = go.GetComponent<MecanimControl>();


			if (!everyFrame.Value)
			{
				DoTheMagic();
				Finish();
			}

		}

		public override void OnUpdate()
		{
			if (everyFrame.Value)
			{
				DoTheMagic();
			}
		}

		void DoTheMagic()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				return;
			}
            
			var aClip = clip.Value as AnimationClip;
			if (aClip == null)
			{
				return;
			}
            
			switch (methods)
			{
			case IsPlaying.clipName:
				isPlaying.Value = theScript.IsPlaying(clipName.Value);
				break;
			case IsPlaying.clipName_weight:
				isPlaying.Value = theScript.IsPlaying(clipName.Value, weight.Value);
				break;
			case IsPlaying.clip:
				isPlaying.Value = theScript.IsPlaying(aClip);
				break;
			case IsPlaying.clip_weight:
				isPlaying.Value = theScript.IsPlaying(aClip, weight.Value);
				break;
			}
			
			if (isPlaying.Value)
			{
				if(trueEvent != null)
				{
					Fsm.Event(trueEvent);
				}
			}
			else if(!isPlaying.Value)
			{
				if(falseEvent != null)
				{
					Fsm.Event(falseEvent);
				}
			}
	        
		}

	}
}