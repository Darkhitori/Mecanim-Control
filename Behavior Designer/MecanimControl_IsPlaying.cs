using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.Tasks.Mecanim_Control
{
	[TaskCategory("MecanimControl")]
	[TaskDescription("Returns true if clipName, clip or animationData is playing. ")]
	public class MecanimControl_IsPlaying : Conditional
	{
	
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;
		
		public enum IsPlaying
		{
			clipName,
			clipName_weight,
			clip,
			clip_weight,
			animData,
			animData_weight
		}
	    
		public IsPlaying isPlayingMethods;
	    
		public SharedString clipName;
	    
		public SharedFloat weight;
	    
		public SharedAnimationClip clip;
	    
		public SharedMecanimAnimationData aniData;
	    
		[RequiredField]
		public SharedBool isPlaying;
	
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

			switch (isPlayingMethods)
			{
			case IsPlaying.clipName:
				isPlaying.Value = theScript.IsPlaying(clipName.Value);
				break;
			case IsPlaying.clipName_weight:
				isPlaying.Value = theScript.IsPlaying(clipName.Value, weight.Value);
				break;
			case IsPlaying.clip:
				isPlaying.Value = theScript.IsPlaying(clip.Value);
				break;
			case IsPlaying.clip_weight:
				isPlaying.Value = theScript.IsPlaying(clip.Value, weight.Value);
				break;
			case IsPlaying.animData:
				isPlaying.Value = theScript.IsPlaying(aniData.Value);
				break;
			case IsPlaying.animData_weight:
				isPlaying.Value = theScript.IsPlaying(aniData.Value, weight.Value);
				break;
			}

			return isPlaying.Value? TaskStatus.Success : TaskStatus.Failure;
		}
	
		public override void OnReset()
		{
			targetGameObject = null;
			clipName = "";
			weight = null;
			clip = null;
			aniData = null;
			isPlaying = false;
		}
	}
}
