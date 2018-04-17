//Darkhitori ver# 1.0
using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("LegacyControl")]
	[Tooltip("Get the normalizedSpeed value set for animationClip/clipName/animData")]
	public class LC_GetNormalizedSpeed : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(LegacyControl))]
		public FsmOwnerDefault gameObject;
	    
		public enum NormalizedSpeed
		{
			clip,
			clipName
		}
	    
		public NormalizedSpeed methods;
	    
	    
		[ObjectType(typeof(AnimationClip))]
		public FsmObject clip;
	    
		public FsmString clipName;
		
		[ActionSection("Return")]
		[UIHint(UIHint.FsmFloat)]
		public FsmFloat normalizedSpeed;
		
		public FsmBool everyFrame;

		LegacyControl theScript;
  

		public override void Reset()
		{
			gameObject = null;
			methods = NormalizedSpeed.clip;
			clip = null;
			clipName = "";
			normalizedSpeed = null;
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
			case NormalizedSpeed.clip:
				normalizedSpeed.Value = theScript.GetNormalizedSpeed(aclip);
				break;
			case NormalizedSpeed.clipName:
				normalizedSpeed.Value = theScript.GetNormalizedSpeed(clipName.Value);
				break;
			}
	        
		}

	}
}