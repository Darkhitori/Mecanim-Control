//Darkhitori ver# 1.0
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("MecaninControl")]
	[Tooltip("Fires when an animation loops. This is only triggered if the animation WrapMode is set to either WrapMode.Loop or WrapMode.PingPong ")]
	public class MC_OnAnimationLoop : FsmStateAction
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
		
		public override void Awake()
		{
			
		}
		
		public override void OnEnter()
		{
			MecanimControl.OnAnimationLoop += OnAnimationLoop;
			
		}

		public override void OnExit()
		{
			MecanimControl.OnAnimationLoop -= OnAnimationLoop;
		}
		
		void OnAnimationLoop(MecanimAnimationData animData)
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