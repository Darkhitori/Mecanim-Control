using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.Tasks.Mecanim_Control
{
	[TaskCategory("MecanimControl/Actions")]
	[TaskDescription("Sets the defaultclip through code (instead of the UI). ")]
	public class MecanimControl_SetDefaultClip : Action
	{
	
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;
		
		public SharedAnimationClip clip;
	    
		public SharedString name;
	    
		public SharedFloat speed;
	    
		public WrapMode wrapMode;
	    
		public SharedBool mirror;
	
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

			theScript.SetDefaultClip(clip.Value,
				name.Value,
				speed.Value,
				wrapMode,
				mirror.Value);

			return TaskStatus.Success;
		}
	
		public override void OnReset()
		{
			targetGameObject = null;
			clip = null;
			name = "";
			speed = null;
			wrapMode = WrapMode.Default;
			mirror = false;
		}
	}
}
