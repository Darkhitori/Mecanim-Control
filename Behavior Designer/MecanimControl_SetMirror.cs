using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.Tasks.Mecanim_Control
{
	[TaskCategory("MecanimControl/Actions")]
	[TaskDescription("When toggled on, every animation will be played with the mirror tag toggled on. ")]
	public class MecanimControl_SetMirror : Action
	{
	
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;
		
		public enum _SetMirror
		{
			toggle,
			toggle_blendingTime,
			toggle_blendingTime_forcemirror
		}
	    
		public _SetMirror setMirrorMethods;
	    
		public SharedBool toggle;
		
		public SharedFloat blendingTime;
		
		public SharedBool forceMirror;
		
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

			switch (setMirrorMethods)
			{
			case   _SetMirror.toggle:
				theScript.SetMirror(toggle.Value);
				break;
			case   _SetMirror.toggle_blendingTime:
				theScript.SetMirror(toggle.Value,blendingTime.Value);
				break;
			case  _SetMirror.toggle_blendingTime_forcemirror:
				theScript.SetMirror(toggle.Value, blendingTime.Value, forceMirror.Value);
				break;
			}

			return TaskStatus.Success;
		}
	
		public override void OnReset()
		{
			targetGameObject = null;
			setMirrorMethods = _SetMirror.toggle;
			toggle = false;
			blendingTime = null;
			forceMirror = false;
		}
	}
}
