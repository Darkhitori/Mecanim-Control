//Darkhitori ver# 1.0
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("LegacyControl")]
	[Tooltip("Get the name of the current running clip.")]
	public class LC_GetCurrentClipName : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(LegacyControl))]
		public FsmOwnerDefault gameObject;
	    
		[ActionSection("Return")]
		[UIHint(UIHint.FsmString)]
		public FsmString currentClipName;
		
		public FsmBool everyFrame;

		LegacyControl theScript;
  

		public override void Reset()
		{
			gameObject = null;
			currentClipName = "";
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
            
			currentClipName.Value = theScript.GetCurrentClipName();
	        
		}

	}
}
