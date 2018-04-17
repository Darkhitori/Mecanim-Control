//Darkhitori ver# 1.0
using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("MecaninControl")]
	[Tooltip("Set the position in the timeline of the current playing clip (0-1). If pause is toggled on, the animation will be paused afterwards. ")]
	public class MC_SetCurrentClipPosition : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(MecanimControl))]
		public FsmOwnerDefault gameObject;
		
		public enum _SetCurrentClipPosition
		{
			normalizedTime,
			normalizedTime_pause
		}
		
		public _SetCurrentClipPosition methods;
	    
		public FsmFloat normalizedTime;
	    
		public FsmBool pause;
	    
		public FsmBool everyFrame;

		MecanimControl theScript;
  

		public override void Reset()
		{
			gameObject = null;
			methods =  _SetCurrentClipPosition.normalizedTime;
			normalizedTime = null;
			pause = false;
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
            
			switch(methods)
			{
			case  _SetCurrentClipPosition.normalizedTime:
				theScript.SetCurrentClipPosition(normalizedTime.Value);
				break;
			case  _SetCurrentClipPosition.normalizedTime_pause:
				theScript.SetCurrentClipPosition(normalizedTime.Value, pause.Value);
				break;
			}

		}

	}
}