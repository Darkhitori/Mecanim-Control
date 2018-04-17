//Darkhitori ver# 1.0
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("MecaninControl")]
	[Tooltip("When toggled on, every animation will be played with the mirror tag toggled on. ")]
	public class MC_SetMirror : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(MecanimControl))]
		public FsmOwnerDefault gameObject;
		
		public enum _SetMirror
		{
			toggle,
			toggle_blendingTime,
			toggle_blendingTime_forcemirror
		}
	    
		public _SetMirror methods;
	    
		[ActionSection("")]
		public FsmBool toggle;
		[ActionSection("")]
		public FsmFloat blendingTime;
		[ActionSection("")]
		public FsmBool forceMirror;
	    
		public FsmBool everyFrame;

		MecanimControl theScript;
  

		public override void Reset()
		{
			gameObject = null;
			methods = _SetMirror.toggle;
			toggle = false;
			blendingTime = null;
			forceMirror = false;
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
			case   _SetMirror.toggle:
				theScript.SetMirror(toggle.Value);
				break;
			case   _SetMirror.toggle_blendingTime:
				theScript.SetMirror(toggle.Value,blendingTime.Value);
				break;
			case  _SetMirror.toggle_blendingTime_forcemirror:
				theScript.SetMirror(toggle.Value, blendingTime.Value, forceMirror.Value);
				break;
			}
		        
		   
		}

	}
}