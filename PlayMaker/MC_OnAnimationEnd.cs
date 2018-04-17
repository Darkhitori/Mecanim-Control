//Darkhitori ver# 1.0
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("MecaninControl")]
	[Tooltip("Fires when an animation ends. ")]
	public class MC_OnAnimationEnd : FsmStateAction
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
			MecanimControl.OnAnimationEnd += OnAnimationEnd;
		}

		public override void OnExit()
		{
			MecanimControl.OnAnimationEnd -= OnAnimationEnd;
		}
		
		void OnAnimationEnd(MecanimAnimationData animData)
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