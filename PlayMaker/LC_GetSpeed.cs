﻿//Darkhitori ver# 1.0
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("LegacyControl")]
	[Tooltip("Get the speed value set for animationClip/clipName. no parameters - Get the speed the animator is running based on the current running animation.")]
	public class LC_GetSpeed : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(LegacyControl))]
		public FsmOwnerDefault gameObject;
	    
		public enum _GetSpeed
		{
			clip,
			clipName,
			noParameters
		}
	    
		public _GetSpeed methods;
	    
	    
		[ObjectType(typeof(AnimationClip))]
		public FsmObject clip;
	    
		public FsmString clipName;
		
		[ActionSection("Return")]
		[UIHint(UIHint.FsmFloat)]
		public FsmFloat getSpeed;
		
		public FsmBool everyFrame;

		LegacyControl theScript;
  

		public override void Reset()
		{
			gameObject = null;
			methods = _GetSpeed.clip;
			clip = null;
			clipName = "";
			getSpeed = null;
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
            
			var aclip = clip.Value as AnimationClip;
			if (aclip == null)
			{
				return;
			}
            
			switch (methods)
			{
			case _GetSpeed.clip:
				getSpeed.Value = theScript.GetSpeed(aclip);
				break;
			case _GetSpeed.clipName:
				getSpeed.Value = theScript.GetSpeed(clipName.Value);
				break;
			case _GetSpeed.noParameters:
				getSpeed.Value = theScript.GetSpeed();
				break;
			}
	        
		}

	}
}
