using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.Tasks.Mecanim_Control
{
	[TaskCategory("MecanimControl/Actions")]
	[TaskDescription("Copy animationData from to another ")]
	public class MecanimControl_CopyAnimationData : Action
	{
	
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;
		
		public SharedMecanimAnimationData from;
		public SharedMecanimAnimationData to;
		
		
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
			MecanimAnimationData mTo = to.Value;

			theScript.CopyAnimationData(from.Value, ref mTo);

			return TaskStatus.Success;
		}
	
		public override void OnReset()
		{
			targetGameObject = null;
			from = null;
			to = null;
		}
	}
}
