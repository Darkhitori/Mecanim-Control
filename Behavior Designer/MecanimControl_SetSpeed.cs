using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.Tasks.Mecanim_Control
{
	[TaskCategory("MecanimControl/Actions")]
	[TaskDescription("Change the speed value of the Animator component or AnimationData based on clipName/clip. If no parameters are used, SetSpeed will change the global speed from the Animator component. ")]
	public class MecanimControl_SetSpeed : Action
	{
	
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;
		
		public enum _SetSpeed
		{
			clip_speed,
			clipName_speed,
			speed
		}
	    
		public _SetSpeed setSpeedMethods;
	    
		public SharedAnimationClip clip;
	    
		public SharedString clipName;
		
		public SharedFloat speed;
		
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

			switch (setSpeedMethods)
			{
			case  _SetSpeed.clip_speed:
				theScript.SetSpeed(clip.Value, speed.Value);
				break;
			case  _SetSpeed.clipName_speed:
				theScript.SetSpeed(clipName.Value, speed.Value);
				break;
			case  _SetSpeed.speed:
				theScript.SetSpeed(speed.Value);
				break;
			}

			return TaskStatus.Success;
		}
	
		public override void OnReset()
		{
			targetGameObject = null;
			setSpeedMethods = _SetSpeed.clip_speed;
			clip = null;
			clipName = "";
			speed = null;
		}
	}
}
