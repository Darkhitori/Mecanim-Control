//Darkhitori ver# 1.0
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("MecaninControl")]
	[Tooltip("Sets the Wrap Mode of an AnimationData based on clipName/clip. If no parameters are used, SetWrapMode will change defaultWrapMode. ")]
	public class MC_SetWrapMode : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(MecanimControl))]
		public FsmOwnerDefault gameObject;
		
		public enum _SetWrapMode
		{
			wrapMode,
			clip_wrapMode,
			clipName_wrapMode
		}
	    
		public _SetWrapMode methods;
		[ActionSection("")]
		[ObjectType(typeof(WrapMode))]
		public FsmEnum wrapMode;
		[ActionSection("")]
		[ObjectType(typeof(AnimationClip))]
		public FsmObject clip;
		[ActionSection("")]
		public FsmString clipName;
		
		public FsmBool everyFrame;

		MecanimControl theScript;
  

		public override void Reset()
		{
			gameObject = null;
			methods =  _SetWrapMode.wrapMode;
			wrapMode = null;
			clip = null;
			clipName = "";
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
			var aClip = clip.Value as AnimationClip;
			if (aClip == null)
			{
				return;
			}
	       
            
			switch (methods)
			{
			case   _SetWrapMode.wrapMode:
				theScript.SetWrapMode((WrapMode)wrapMode.Value);
				break;
			case   _SetWrapMode.clip_wrapMode:
				theScript.SetWrapMode(aClip, (WrapMode)wrapMode.Value);
				break;
			case   _SetWrapMode.clipName_wrapMode:
				theScript.SetWrapMode(clipName.Value, (WrapMode)wrapMode.Value);
				break;
			}
		        
		   
		}

	}
}