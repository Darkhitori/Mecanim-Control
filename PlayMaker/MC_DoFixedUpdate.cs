//Darkhitori ver# 1.0
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("MecaninControl")]
	[Tooltip(" ")]
	public class MC_DoFixedUpdate : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(MecanimControl))]
		public FsmOwnerDefault gameObject;
		
		public FsmBool everyFrame;

		MecanimControl theScript;
  

		public override void Reset()
		{
			gameObject = null;
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
            
			theScript.DoFixedUpdate();
			
		}

	}
}
