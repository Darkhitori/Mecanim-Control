using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.Tasks.Mecanim_Control
{
	[TaskCategory("MecanimControl/Actions")]
	[TaskDescription("Returns the AnimationData related to that animation name or clip. ")]
	public class MecanimControl_GetAnimationData : Action
	{
	
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;
		
		public enum _AnimationData
		{
			clipName,
			clip
		}
	    
		public _AnimationData animationDataMethods;
		
		public SharedString clipName;
	    
		public SharedAnimationClip clip;
		[Tooltip("Return")]
		[RequiredField]
		public SharedMecanimAnimationData animData;
	
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

			switch (animationDataMethods)
			{
			case _AnimationData.clipName:
				animData.Value = theScript.GetAnimationData(clipName.Value);
				break;
			case _AnimationData.clip:
				animData.Value = theScript.GetAnimationData(clip.Value);
				break;
			}

			return TaskStatus.Success;
		}
	
		public override void OnReset()
		{
			targetGameObject = null;
			animationDataMethods = _AnimationData.clipName;
			clipName = "";
			clip = null;
			animData = null;
		}
	}
}
