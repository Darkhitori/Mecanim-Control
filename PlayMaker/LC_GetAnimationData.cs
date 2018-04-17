//Darkhitori ver# 1.0
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("LegacyControl")]
	[Tooltip("Returns the AnimationData related to that animation name or clip. ")]
	public class LC_GetAnimationData : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(LegacyControl))]
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
	    
		[UIHint(UIHint.FsmEnum)]
		[ObjectType(typeof(WrapMode))]
		public FsmEnum wrapMode;
	    
		[UIHint(UIHint.FsmFloat)]
		public FsmFloat length;
	    
		[UIHint(UIHint.FsmFloat)]
		public FsmFloat originalSpeed;
		
		[UIHint(UIHint.FsmFloat)]
		public FsmFloat normalizedSpeed;
		
		public AnimationState animName;
		
		public FsmBool everyFrame;

		LegacyControl theScript;
	    
	    
		public override void Reset()
		{
			gameObject = null;
			methods = _AnimationData.clipName;
			clipName = "";
			clip = null;
			adClip = null;
			adClipName = "";
			speed = null;
			wrapMode = null;
			length = null;
			originalSpeed = null;
			normalizedSpeed = null;
			animName = null;
			everyFrame = true;


		}
       
		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);

			theScript = go.GetComponent<LegacyControl>();


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
				wrapMode.Value = theScript.GetAnimationData(clipName.Value).wrapMode;
				length.Value = theScript.GetAnimationData(clipName.Value).length;
				originalSpeed.Value = theScript.GetAnimationData(clipName.Value).originalSpeed;
				normalizedSpeed.Value = theScript.GetAnimationData(clipName.Value).normalizedSpeed;
				animName = theScript.GetAnimationData(clipName.Value).animState;
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
				wrapMode.Value = theScript.GetAnimationData(aClip).wrapMode;
				length.Value = theScript.GetAnimationData(aClip).length;
				originalSpeed.Value = theScript.GetAnimationData(aClip).originalSpeed;
				normalizedSpeed.Value = theScript.GetAnimationData(aClip).normalizedSpeed;
				animName = theScript.GetAnimationData(aClip).animState;
				break;
			}
            
            
            
	       
		}

	}
}
