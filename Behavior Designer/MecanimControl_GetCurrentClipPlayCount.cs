using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.Tasks.Mecanim_Control
{
	[TaskCategory("MecanimControl/Actions")]
	[TaskDescription("Get the number of times the current clip has played. Only works if the animation's WrapMode is set to either WrapMode.Loop or WrapMode.PingPong ")]
	public class MecanimControl_GetCurrentClipPlayCount : Action
	{
	
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;
		
		[RequiredField]
		public SharedInt currentClipPlayCount;
	
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

			currentClipPlayCount.Value = theScript.GetCurrentClipPlayCount();

			return TaskStatus.Success;
		}
	
		public override void OnReset()
		{
			targetGameObject = null;
			currentClipPlayCount = null;
		}
	}
}
