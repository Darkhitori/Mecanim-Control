﻿using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.Tasks.Mecanim_Control
{
	[TaskCategory("MecanimControl/Actions")]
	[TaskDescription("Stops any animation from playing and starts playing the default animation. ")]
	public class MecanimControl_Stop : Action
	{
	
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;
		
		
		
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

			theScript.Stop();

			return TaskStatus.Success;
		}
	
		public override void OnReset()
		{
			targetGameObject = null;
			
		}
	}
}
