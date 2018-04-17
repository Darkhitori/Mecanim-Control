using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.Tasks.Mecanim_Control
{
	[TaskCategory("MecanimControl/Actions")]
	[TaskDescription("Get the speed value set for animationClip/clipName. no parameters - Get the speed the animator is running based on the current running animation. ")]
	public class MecanimControl_GetSpeed : Action
	{
	
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;
		
		public enum _GetSpeed
		{
			clip,
			clipName,
			noParameters
		}
	    
		public _GetSpeed getSpeedMethods;
	    
	    
		public SharedAnimationClip clip;
	    
		public SharedString clipName;
		[RequiredField]
		public SharedFloat getSpeed;
	
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

			switch (getSpeedMethods)
			{
			case _GetSpeed.clip:
				getSpeed.Value = theScript.GetSpeed(clip.Value);
				break;
			case _GetSpeed.clipName:
				getSpeed.Value = theScript.GetSpeed(clipName.Value);
				break;
			case _GetSpeed.noParameters:
				getSpeed.Value = theScript.GetSpeed();
				break;
			}

			return TaskStatus.Success;
		}
	
		public override void OnReset()
		{
			targetGameObject = null;
			getSpeedMethods = _GetSpeed.clip;
			clip = null;
			clipName = "";
			getSpeed = null;
		}
	}
}
