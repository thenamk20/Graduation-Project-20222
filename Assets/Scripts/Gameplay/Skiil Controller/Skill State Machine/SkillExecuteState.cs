using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillExecuteState : SkillStateBase
{
    private float countExecuteTime;

    public SkillExecuteState(SkillItemController _skill) : base(_skill)
    {
    }

    public override void OnEnter()
    {
        MyPlayer.Instance.Controller.SkillsManager.OnStartExecute.Dispatch(skill.SkillConfig.skillIndex);
        countExecuteTime = 0;
        HCDebug.Log("Enter skill execute state", HcColor.Violet);
    }

    public override void OnExit()
    {
        MyPlayer.Instance.Controller.SkillsManager.OnDoneExecute.Dispatch(skill.SkillConfig.skillIndex);
    }

    public override SkillStateBase OnUpdate()
    {
        countExecuteTime += Time.deltaTime;

        if(countExecuteTime <= skill.SkillConfig.executeTime)
        {
            return this;
        }
        else
        {
            return new SkillCooldownState(skill);
        }
    }
}
