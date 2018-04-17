//Darkhitori ver# 1.0
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("MecaninControl")]
	[Tooltip("Fades the animation with name clipName in over a period of blendingTime seconds as it fades other animations out. You can also set normalizedTime to set where, in its timeline, you want the animation to start (0-1) as well as toggle mirror.")]
	public class MC_CrossFade : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(MecanimControl))]
		public FsmOwnerDefault gameObject;
		
		public enum _CrossFade
		{
			clipName_blendingTime,
			clipName_blendingTime_normalizedTime_mirror
		}
	    
		public _CrossFade methods;
	    
		public FsmString clipName;
		[ActionSection("")]
		public FsmFloat blendingTime;
		[ActionSection("")]
		public FsmFloat normalizedTime;
		[ActionSection("")]
		public FsmBool mirror;
		
		public FsmBool everyFrame;

		MecanimControl theScript;
  

		public override void Reset()
		{
			gameObject = null;
			methods =  _CrossFade.clipName_blendingTime;
			clipName = "";
			blendingTime = null;
			mirror = false;
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
			case  _CrossFade.clipName_blendingTime:
				theScript.CrossFade(clipName.Value, blendingTime.Value);
				break;
			case  _CrossFade.clipName_blendingTime_normalizedTime_mirror:
				theScript.CrossFade(clipName.Value,blendingTime.Value, normalizedTime.Value, mirror.Value);
				break;
			}
		        
		   
		}

	}
}
