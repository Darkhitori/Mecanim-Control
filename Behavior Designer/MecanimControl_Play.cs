using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.Tasks.Mecanim_Control
{
	[TaskCategory("MecanimControl/Actions")]
	[TaskDescription("Plays animation. ")]
	public class MecanimControl_Play : Action
	{
	
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;
		
		public enum _Play
		{
			clipName_blendingTime_normalizedTime_mirror,
			clip_blendingTime_normalizedTime_mirror,
			clipName_mirror,
			clipName,
			clip_mirror,
			clip,
			aniData_mirror,
			aniData,
			aniData_blendingTime_normalizedTime_mirror,
			play,
		}
	    
		public _Play playMethods;
		
		public SharedString clipName;
		public SharedAnimationClip clip;
		public SharedFloat blendingTime;
		public SharedFloat normalizedTime;
		public SharedBool mirror;
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

			switch (playMethods)
			{
			case   _Play.clipName_blendingTime_normalizedTime_mirror:
				theScript.Play(clipName.Value, blendingTime.Value, normalizedTime.Value, mirror.Value);
				break;
			case   _Play.clip_blendingTime_normalizedTime_mirror:
				theScript.Play(clip.Value, blendingTime.Value, normalizedTime.Value, mirror.Value);
				break;
			case  _Play.clipName_mirror:
				theScript.Play(clipName.Value, mirror.Value);
				break;
			case _Play.clipName:
				theScript.Play(clipName.Value);
				break;
			case _Play.clip_mirror:
				theScript.Play(clip.Value, mirror.Value);
				break;
			case _Play.clip:
				theScript.Play(clip.Value);
				break;
			case _Play.aniData_mirror:
				theScript.Play(aniData.Value, mirror.Value);
				break;
			case _Play.aniData:
				theScript.Play(aniData.Value);
				break;
			case _Play.aniData_blendingTime_normalizedTime_mirror:
				theScript.Play(aniData.Value, blendingTime.Value, normalizedTime.Value, mirror.Value);
				break;
			case _Play.play:
				theScript.Play();
				break;
			}

			return TaskStatus.Success;
		}
	
		public override void OnReset()
		{
			targetGameObject = null;
			playMethods = _Play.clipName_blendingTime_normalizedTime_mirror;
			clip = null;
			clipName = "";
			blendingTime = null;
			normalizedTime = null;
			mirror = false;
			aniData = null;
		}
	}
}
