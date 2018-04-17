//Darkhitori ver# 1.0
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("LegacyControl")]
	[Tooltip("Returns true if clipName or animationData is playing. ")]
	public class LC_IsPlaying : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(LegacyControl))]
		public FsmOwnerDefault gameObject;
	    
		[ActionSection("")]
		public FsmString clipName;
	    
		[ActionSection("Return")]
		[UIHint(UIHint.FsmBool)]
		public FsmBool isPlaying;
		[ActionSection("Send Events")]
		public FsmEvent trueEvent;
		public FsmEvent falseEvent;
		
		public FsmBool everyFrame;

		LegacyControl theScript;
  

		public override void Reset()
		{
			gameObject = null;
			clipName = "";
			isPlaying = false;
			trueEvent = null;
			falseEvent = null;
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
            
			isPlaying.Value = theScript.IsPlaying(clipName.Value);
			
			if (isPlaying.Value)
			{
				if(trueEvent != null)
				{
					Fsm.Event(trueEvent);
				}
			}
			else if(!isPlaying.Value)
			{
				if(falseEvent != null)
				{
					Fsm.Event(falseEvent);
				}
			}
	        
		}

	}
}