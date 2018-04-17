using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.Tasks.Mecanim_Control
{
	[TaskCategory("MecanimControl/Actions")]
	[TaskDescription("Sets the Wrap Mode of an AnimationData based on clipName/clip. If no parameters are used, SetWrapMode will change defaultWrapMode. ")]
	public class MecanimControl_SetWrapMode : Action
	{
	
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;
		
		public enum _SetWrapMode
		{
			wrapMode,
			aniData_wrapMode,
			clip_wrapMode,
			clipName_wrapMode
		}
	    
		public _SetWrapMode setWrapModeMethods;
		
		public WrapMode wrapMode;
		
		public SharedAnimationClip clip;
		
		public SharedString clipName;
		
		public SharedMecanimAnimationData aniData;
		
		MecanimControl theScript;
		GameObject prevGameObject;
	
		public override void OnStart()
		{
			var currentGameObject = GetDefaultGameObject(targetGameObject.Value);
			if (currentGameObject != prevGameObject)
			{
				theScript = currentGameObject.GetComponent<MecanimControl>();
				prevGameObject = currentGameObject;
			}
		}
	
		public override TaskStatus OnUpdate()
		{
			if (theScript == null)
			{
				return TaskStatus.Failure;
			}

			switch (setWrapModeMethods)
			{
			case   _SetWrapMode.wrapMode:
				theScript.SetWrapMode(wrapMode);
				break;
			case   _SetWrapMode.aniData_wrapMode:
				theScript.SetWrapMode(aniData.Value, wrapMode);
				break;
			case   _SetWrapMode.clip_wrapMode:
				theScript.SetWrapMode(clip.Value, wrapMode);
				break;
			case   _SetWrapMode.clipName_wrapMode:
				theScript.SetWrapMode(clipName.Value, wrapMode);
				break;
			}

			return TaskStatus.Success;
		}
	
		public override void OnReset()
		{
			targetGameObject = null;
			setWrapModeMethods =  _SetWrapMode.wrapMode;
			clip = null;
			clipName = "";
			aniData = null;
		}
	}
}
