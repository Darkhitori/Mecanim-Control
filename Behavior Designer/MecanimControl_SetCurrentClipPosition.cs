using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.Tasks.Mecanim_Control
{
	[TaskCategory("MecanimControl/Actions")]
	[TaskDescription("Set the position in the timeline of the current playing clip (0-1). If pause is toggled on, the animation will be paused afterwards. ")]
	public class MecanimControl_SetCurrentClipPosition : Action
	{
	
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;
		
		public enum _SetCurrentClipPosition
		{
			normalizedTime,
			normalizedTime_pause
		}
		
		public _SetCurrentClipPosition setCurrentClipPosMethod;
	    
		public SharedFloat normalizedTime;
	    
		public SharedBool pause;
		
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

			switch(setCurrentClipPosMethod)
			{
			case  _SetCurrentClipPosition.normalizedTime:
				theScript.SetCurrentClipPosition(normalizedTime.Value);
				break;
			case  _SetCurrentClipPosition.normalizedTime_pause:
				theScript.SetCurrentClipPosition(normalizedTime.Value, pause.Value);
				break;
			}

			return TaskStatus.Success;
		}
	
		public override void OnReset()
		{
			targetGameObject = null;
			setCurrentClipPosMethod =  _SetCurrentClipPosition.normalizedTime;
			normalizedTime = null;
			pause = false;
		}
	}
}
