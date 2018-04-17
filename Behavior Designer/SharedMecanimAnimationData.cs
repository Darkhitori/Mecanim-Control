using UnityEngine;
using BehaviorDesigner.Runtime;

[System.Serializable]
public class MC_MecanimAnimationData
{

}

[System.Serializable]
public class SharedMecanimAnimationData : SharedVariable<MecanimAnimationData>
{
	public override string ToString() { return mValue == null ? "null" : mValue.ToString(); }
	public static implicit operator SharedMecanimAnimationData(MecanimAnimationData value) { return new SharedMecanimAnimationData { mValue = value }; }
}