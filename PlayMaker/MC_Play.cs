//Darkhitori ver# 1.0
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("MecaninControl")]
	[Tooltip(" Plays animation. ")]
	public class MC_Play : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(MecanimControl))]
		public FsmOwnerDefault gameObject;
		
		public enum _Play
		{
			clipName_blendingTime_normalizedTime_mirror,
			clip_blendingTime_normalizedTime_mirror,
			clipName_mirror,
			clipName,
			clip_mirror,
			clip,
			play
		}
	    
		public _Play methods;
		[ActionSection("")]
		public FsmString clipName;
		[ActionSection("")]
		[ObjectType(typeof(AnimationClip))]
		public FsmObject clip;
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
			methods = _Play.clipName_blendingTime_normalizedTime_mirror;
			clip = null;
			clipName = "";
			blendingTime = null;
			normalizedTime = null;
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
            
			var aClip = clip.Value as AnimationClip;
			if (aClip == null)
			{
				return;
			}
            
			switch (methods)
			{
			case   _Play.clipName_blendingTime_normalizedTime_mirror:
				theScript.Play(clipName.Value, blendingTime.Value, normalizedTime.Value, mirror.Value);
				break;
			case   _Play.clip_blendingTime_normalizedTime_mirror:
				theScript.Play(aClip, blendingTime.Value, normalizedTime.Value, mirror.Value);
				break;
			case  _Play.clipName_mirror:
				theScript.Play(clipName.Value, mirror.Value);
				break;
			case _Play.clipName:
				theScript.Play(clipName.Value);
				break;
			case _Play.clip_mirror:
				theScript.Play(aClip, mirror.Value);
				break;
			case _Play.clip:
				theScript.Play(aClip);
				break;
			case _Play.play:
				theScript.Play();
				break;
			}
		        
		   
		}

	}
}