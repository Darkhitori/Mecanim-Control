using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.Tasks.Mecanim_Control
{
	[TaskCategory("MecanimControl/Actions")]
	[TaskDescription("Removes the AnimationData from animations related to clipName/clip. ")]
	public class MecanimControl_RemoveClip : Action
	{
	
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;
		
		public enum RemoveClip
		{
			name,
			clip
		}
	    
		public RemoveClip removeClipMethods;
	    
		public SharedString name;
	    
		public SharedAnimationClip clip;
		
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

			switch (removeClipMethods)
			{
			case RemoveClip.name:
				theScript.RemoveClip(name.Value);
				break;
			case RemoveClip.clip:
				theScript.RemoveClip(clip.Value);
				break;
			}

			return TaskStatus.Success;
		}
	
		public override void OnReset()
		{
			targetGameObject = null;
			removeClipMethods = RemoveClip.name;
			name = "";
			clip = null;
		}
	}
}
