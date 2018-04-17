using UnityEngine;
using BehaviorDesigner.Runtime;

[System.Serializable]
public class MC_AnimationClip
{

}

[System.Serializable]
public class SharedAnimationClip : SharedVariable<AnimationClip>
{
	public override string ToString() { return mValue == null ? "null" : mValue.ToString(); }
	public static implicit operator SharedAnimationClip(AnimationClip value) { return new SharedAnimationClip { mValue = value }; }
}