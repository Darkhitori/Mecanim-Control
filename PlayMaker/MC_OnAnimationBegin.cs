//Darkhitori ver# 1.0
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("MecaninControl")]
	[Tooltip("Fires when an animation begins. ")]
	public class MC_OnAnimationBegin : FsmStateAction
	{
		[ActionSection("Animation Clip")]
		[ObjectType(typeof(AnimationClip))]
		public FsmObject clip;
		
		[ActionSection("Event")]
		public FsmEvent sendEvent;
		
		public override void Reset()
		{
			clip = null;
			sendEvent = null;
		}
		
		public override void OnEnter()
		{
			MecanimControl.OnAnimationBegin += OnAnimationBegin;
			
		}

		public override void OnExit()
		{
			MecanimControl.OnAnimationBegin -= OnAnimationBegin;
			
		}
		
		void OnAnimationBegin(MecanimAnimationData animData)
		{
			var mClip = clip.Value as AnimationClip;
			
			if (mClip == null)
			{
				return;
			}
			
			if (mClip == animData.clip)
			{
				if (sendEvent != null)
				{
					Fsm.Event(sendEvent);
				}
				
			}
			
		}

	}
}