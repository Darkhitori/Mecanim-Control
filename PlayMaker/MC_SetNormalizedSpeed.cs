//Darkhitori ver# 1.0
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("MecaninControl")]
	[Tooltip("Change the normalizedspeed value of the Animator component or AnimationData based on clipName/clip. If no parameters are used, SetSpeed will change the global speed from the Animator component.")]
	public class MC_SetNormalizedSpeed : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(MecanimControl))]
		public FsmOwnerDefault gameObject;
	    
		public enum SetNormalizedSpeed
		{
			clip_normalizedSpeed,
			clipName_normalizedSpeed
		}
	    
		public SetNormalizedSpeed methods;
	    
		[ActionSection("")]
		[ObjectType(typeof(AnimationClip))]
		public FsmObject clip;
	    
		[ActionSection("")]
		public FsmString clipName;
		
		[ActionSection("")]
		public FsmFloat normalizedSpeed;
		
		public FsmBool everyFrame;

		MecanimControl theScript;
  

		public override void Reset()
		{
			gameObject = null;
			methods =  SetNormalizedSpeed.clip_normalizedSpeed;
			clip = null;
			clipName = "";
			normalizedSpeed = null;
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
            
			var aclip = clip.Value as AnimationClip;
			if (aclip == null)
			{
				return;
			}
            
			switch (methods)
			{
			case  SetNormalizedSpeed.clip_normalizedSpeed:
				theScript.SetNormalizedSpeed(aclip, normalizedSpeed.Value);
				break;
			case  SetNormalizedSpeed.clipName_normalizedSpeed:
				theScript.SetNormalizedSpeed(clipName.Value, normalizedSpeed.Value);
				break;
			}
	        
		}

	}
}