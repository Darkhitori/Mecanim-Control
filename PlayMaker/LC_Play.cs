//Darkhitori ver# 1.0
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("LegacyControl")]
	[Tooltip(" Plays animation. ")]
	public class LC_Play : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(LegacyControl))]
		public FsmOwnerDefault gameObject;
		
		[ActionSection("")]
		public FsmString animationName;
		[ActionSection("")]
		public FsmFloat blendingTime;
		[ActionSection("")]
		public FsmFloat normalizedTime;
		
	    
		public FsmBool everyFrame;

		LegacyControl theScript;
  

		public override void Reset()
		{
			gameObject = null;
			animationName = "";
			blendingTime = null;
			normalizedTime = null;
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
			
			theScript.Play(animationName.Value, blendingTime.Value, normalizedTime.Value);
		}

	}
}
