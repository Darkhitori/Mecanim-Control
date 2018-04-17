//Darkhitori ver# 1.0
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("LegacyControl")]
	[Tooltip("Removes the AnimationData from animations related to clipName/clip. ")]
	public class LC_RemoveClip : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(LegacyControl))]
		public FsmOwnerDefault gameObject;
		
		public enum RemoveClip
		{
			name,
			clip
		}
	    
		public RemoveClip methods;
	    
		public FsmString name;
	    
		[ObjectType(typeof(AnimationClip))]
		public FsmObject clip;
	    
		public FsmBool everyFrame;

		LegacyControl theScript;
  

		public override void Reset()
		{
			gameObject = null;
			methods = RemoveClip.name;
			name = "";
			clip = null;
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

			switch (methods)
			{
			case RemoveClip.name:
				theScript.RemoveClip(name.Value);
				break;
			case RemoveClip.clip:
				theScript.RemoveClip(aClip);
				break;
			}
		        
		   
		}

	}
}
