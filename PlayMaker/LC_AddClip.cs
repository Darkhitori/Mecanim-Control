//Darkhitori ver# 1.0
// __ECO__ __PLAYMAKER__ __ACTION__
using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("LegacyControl")]
	[Tooltip("Adds a clip to animations with the name newName. ")]
	public class LC_AddClip : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(LegacyControl))]
		public FsmOwnerDefault gameObject;
		
		public enum _AddClip
		{
			clip_newName,
			clip_newName_speed_wrapMode,
			clip_newName_speed_wrapMode_length
		}
		
		public _AddClip methods;
	    
		[ObjectType(typeof(LegacyControl))]
		public FsmObject clip;
	    
		public FsmString newName;
	    
		public FsmFloat speed;
	    
		public WrapMode wrapMode;
		
		public FsmFloat length;
	    
		public FsmBool everyFrame;

		LegacyControl theScript;
  

		public override void Reset()
		{
			gameObject = null;
			methods = _AddClip.clip_newName;
			clip = null;
			newName = "";
			speed = null;
			wrapMode = WrapMode.Default;
			length = null;
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
            
			var aClip = clip.Value as AnimationClip;
			if (aClip == null)
			{
				return;
			}
	        
			switch(methods)
			{
			case _AddClip.clip_newName:
				theScript.AddClip(aClip, newName.Value);
				break;
			case _AddClip.clip_newName_speed_wrapMode:
				theScript.AddClip(aClip, newName.Value, speed.Value, wrapMode);
				break;
			case _AddClip.clip_newName_speed_wrapMode_length:
				theScript.AddClip(aClip, newName.Value, speed.Value, wrapMode, length.Value);
				break;
			}

		}

	}
}
