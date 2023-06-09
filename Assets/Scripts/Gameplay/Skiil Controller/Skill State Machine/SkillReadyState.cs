using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillReadyState : SkillStateBase
{
    public SkillReadyState(SkillItemController _skill) : base(_skill)
    {

    }

    public override void OnEnter()
    {
        skill.IsReady = true;
        MyPlayer.Instance.Controller.SkillsManager.OnReady.Dispatch(skill.SkillConfig.skillIndex);
        HCDebug.Log("Enter skill ready state", HcColor.Violet);

    }

    public override void OnExit()
    {
       
    }

    public override SkillStateBase OnUpdate()
    {
        if (isSkillReady) return this;
        else return new SkillExecuteState(skill);
    }
}
