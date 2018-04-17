//Darkhitori ver# 1.0
using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("MecaninControl")]
	[Tooltip("Get the number of times the current clip has played. Only works if the animation's WrapMode is set to either WrapMode.Loop or WrapMode.PingPong")]
	public class MC_GetCurrentClipPlayCount : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(MecanimControl))]
		public FsmOwnerDefault gameObject;
	    
		[ActionSection("Return")]
		[UIHint(UIHint.FsmInt)]
		public FsmInt currentClipPlayCount;
		
		public FsmBool everyFrame;

		MecanimControl theScript;
  

		public override void Reset()
		{
			gameObject = null;
			currentClipPlayCount = null;
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
            
			currentClipPlayCount.Value = theScript.GetCurrentClipPlayCount();
	        
		}

	}
}