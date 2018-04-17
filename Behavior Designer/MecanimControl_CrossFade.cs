using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.Tasks.Mecanim_Control
{
	[TaskCategory("MecanimControl/Actions")]
	[TaskDescription("Fades the animation with name clipName in over a period of blendingTime seconds as it fades other animations out. You can also set normalizedTime to set where, in its timeline, you want the animation to start (0-1) as well as toggle mirror. ")]
	public class MecanimControl_CrossFade : Action
	{
	
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;
		
		public enum _CrossFade
		{
			clipName_blendingTime,
			clipName_blendingTime_normalizedTime_mirror,
			animationData_blendingTime_normalizedTime_mirror
		}
		
		public _CrossFade crossFadeMethods;
	    
		public SharedString clipName;
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

			switch (crossFadeMethods)
			{
			case  _CrossFade.clipName_blendingTime:
				theScript.CrossFade(clipName.Value, blendingTime.Value);
				break;
			case  _CrossFade.clipName_blendingTime_normalizedTime_mirror:
				theScript.CrossFade(clipName.Value,blendingTime.Value, normalizedTime.Value, mirror.Value);
				break;
			case _CrossFade.animationData_blendingTime_normalizedTime_mirror:
				theScript.CrossFade(aniData.Value, blendingTime.Value, normalizedTime.Value, mirror.Value);
				break;
			}

			return TaskStatus.Success;
		}
	
		public override void OnReset()
		{
			targetGameObject = null;
			crossFadeMethods =  _CrossFade.clipName_blendingTime;
			clipName = "";
			blendingTime = null;
			mirror = false;
			aniData = null;
		}
	}
}
