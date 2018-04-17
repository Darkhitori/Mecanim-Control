//Darkhitori ver# 1.0
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("MecaninControl")]
	[Tooltip("Returns the AnimationData related to that animation name or clip. ")]
	public class MC_GetAnimationData : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(MecanimControl))]
		public FsmOwnerDefault gameObject;
		
		public enum _AnimationData
		{
			clipName,
			clip
		}
	    
		public _AnimationData methods;
		
		public FsmString clipName;
	    
		[ObjectType(typeof(AnimationClip))]
		public FsmObject clip;
	    
		[ActionSection("Return AnimationData")]
		[ObjectType(typeof(AnimationClip))]
		public FsmObject adClip;
	    
		[UIHint(UIHint.FsmString)]
		public FsmString adClipName;
	    
		[UIHint(UIHint.FsmFloat)]
		public FsmFloat speed;
	    
		[UIHint(UIHint.FsmFloat)]
		public FsmFloat transitionDuration;
	    
		[UIHint(UIHint.FsmEnum)]
		[ObjectType(typeof(WrapMode))]
		public FsmEnum wrapMode;
	    
		[UIHint(UIHint.FsmBool)]
		public FsmBool applyRootMotion;
		
		[UIHint(UIHint.FsmInt)]
		public FsmInt timesPlayed;
	    
		[UIHint(UIHint.FsmFloat)]
		public FsmFloat secondsPlayed;
	    
		[UIHint(UIHint.FsmFloat)]
		public FsmFloat length;
	    
		[UIHint(UIHint.FsmFloat)]
		public FsmFloat originalSpeed;
		
		[UIHint(UIHint.FsmFloat)]
		public FsmFloat normalizedSpeed;
		
		[UIHint(UIHint.FsmFloat)]
		public FsmFloat normalizedTime;
	    
		[UIHint(UIHint.FsmString)]
		public FsmString stateName;
		
		public FsmBool everyFrame;

		MecanimControl theScript;
	    
	    
		public override void Reset()
		{
			gameObject = null;
			methods = _AnimationData.clipName;
			clipName = "";
			clip = null;
			adClip = null;
			adClipName = "";
			speed = null;
			transitionDuration = null;
			wrapMode = null;
			applyRootMotion = false;
			timesPlayed = null;
			secondsPlayed = null;
			length = null;
			originalSpeed = null;
			normalizedSpeed = null;
			normalizedTime = null;
			stateName = null;
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
            
	        
	        
			switch (methods)
			{
			case _AnimationData.clipName:
				adClip.Value = theScript.GetAnimationData(clipName.Value).clip;
				adClipName.Value = theScript.GetAnimationData(clipName.Value).clipName;
				speed.Value = theScript.GetAnimationData(clipName.Value).speed;
				transitionDuration.Value = theScript.GetAnimationData(clipName.Value).transitionDuration;
				wrapMode.Value = theScript.GetAnimationData(clipName.Value).wrapMode;
				applyRootMotion.Value = theScript.GetAnimationData(clipName.Value).applyRootMotion;
				timesPlayed.Value = theScript.GetAnimationData(clipName.Value).timesPlayed;
				secondsPlayed.Value = theScript.GetAnimationData(clipName.Value).secondsPlayed;
				length.Value = theScript.GetAnimationData(clipName.Value).length;
				originalSpeed.Value = theScript.GetAnimationData(clipName.Value).originalSpeed;
				normalizedSpeed.Value = theScript.GetAnimationData(clipName.Value).normalizedSpeed;
				normalizedTime.Value = theScript.GetAnimationData(clipName.Value).normalizedTime;
				stateName.Value = theScript.GetAnimationData(clipName.Value).stateName;
				break;
			case _AnimationData.clip:
				var aClip = clip.Value as AnimationClip;
				if (aClip == null)
				{
					return;
				}
				adClip.Value = theScript.GetAnimationData(aClip).clip;
				adClipName.Value = theScript.GetAnimationData(aClip).clipName;
				speed.Value = theScript.GetAnimationData(aClip).speed;
				transitionDuration.Value = theScript.GetAnimationData(aClip).transitionDuration;
				wrapMode.Value = theScript.GetAnimationData(aClip).wrapMode;
				applyRootMotion.Value = theScript.GetAnimationData(aClip).applyRootMotion;
				timesPlayed.Value = theScript.GetAnimationData(aClip).timesPlayed;
				secondsPlayed.Value = theScript.GetAnimationData(aClip).secondsPlayed;
				length.Value = theScript.GetAnimationData(aClip).length;
				originalSpeed.Value = theScript.GetAnimationData(aClip).originalSpeed;
				normalizedSpeed.Value = theScript.GetAnimationData(aClip).normalizedSpeed;
				normalizedTime.Value = theScript.GetAnimationData(aClip).normalizedTime;
				stateName.Value = theScript.GetAnimationData(aClip).stateName;
				break;
			}
            
            
            
	       
		}

	}
}
