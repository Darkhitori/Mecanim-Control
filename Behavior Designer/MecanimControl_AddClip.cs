using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.Tasks.Mecanim_Control
{
	[TaskCategory("MecanimControl/Actions")]
	[TaskDescription("Adds a clip to animations with the name newName. ")]
	public class MecanimControl_AddClip : Action
	{
	
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;
		
		public enum _AddClip
		{
			clip_newName,
			clip_newName_speed_wrapMode
		}
		
		public _AddClip addClipMethod;
	    
		public SharedAnimationClip clip;
	    
		public SharedString newName;
	    
		public SharedFloat speed;
	    
		public WrapMode wrapMode;
	
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
			
			switch(addClipMethod)
			{
			case _AddClip.clip_newName:
				theScript.AddClip(clip.Value, newName.Value);
				break;
			case _AddClip.clip_newName_speed_wrapMode:
				theScript.AddClip(clip.Value, newName.Value, speed.Value, wrapMode);
				break;
			}
			return TaskStatus.Success;
		}
	
		public override void OnReset()
		{
			targetGameObject = null;
			addClipMethod = _AddClip.clip_newName;
			clip = null;
			newName = "";
			speed = null;
			wrapMode = WrapMode.Default;
		}
	}
}
