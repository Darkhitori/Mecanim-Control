//Darkhitori ver# 1.0
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("LegacyControl")]
	[Tooltip("Change the speed value of the Animator component or AnimationData based on clipName/clip. If no parameters are used, SetSpeed will change the global speed from the Animator component.")]
	public class LC_SetSpeed : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(LegacyControl))]
		public FsmOwnerDefault gameObject;
	    
		public enum _SetSpeed
		{
			clip_speed,
			clipName_speed,
			speed
		}
	    
		public _SetSpeed methods;
	    
		[ActionSection("")]
		[ObjectType(typeof(AnimationClip))]
		public FsmObject clip;
	    
		[ActionSection("")]
		public FsmString clipName;
		
		[ActionSection("")]
		public FsmFloat speed;
		
		public FsmBool everyFrame;

		LegacyControl theScript;
  

		public override void Reset()
		{
			gameObject = null;
			methods = _SetSpeed.clip_speed;
			clip = null;
			clipName = "";
			speed = null;
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
			case  _SetSpeed.clip_speed:
				theScript.SetSpeed(aclip, speed.Value);
				break;
			case  _SetSpeed.clipName_speed:
				theScript.SetSpeed(clipName.Value, speed.Value);
				break;
			case  _SetSpeed.speed:
				theScript.SetSpeed(speed.Value);
				break;
			}
	        
		}

	}
}