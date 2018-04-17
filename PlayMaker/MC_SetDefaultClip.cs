//Darkhitori ver# 1.0
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("MecaninControl")]
	[Tooltip("Sets the defaultclip through code (instead of the UI). ")]
	public class MC_SetDefaultClip : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(MecanimControl))]
		public FsmOwnerDefault gameObject;
		
		[ObjectType(typeof(AnimationClip))]
		public FsmObject clip;
	    
		public FsmString name;
	    
		public FsmFloat speed;
	    
		[ObjectType(typeof(WrapMode))]
		public FsmEnum wrapMode;
	    
		public FsmBool mirror;
	    
		public FsmBool everyFrame;

		MecanimControl theScript;
  

		public override void Reset()
		{
			gameObject = null;
			clip = null;
			name = "";
			speed = null;
			wrapMode = null;
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

			theScript.SetDefaultClip(aClip, name.Value, speed.Value, (WrapMode)wrapMode.Value, mirror.Value);
	    	
	        
		        
		   
		}

	}
}