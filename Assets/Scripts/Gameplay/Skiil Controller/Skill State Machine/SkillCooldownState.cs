using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class SkillCooldownState : SkillStateBase
{
    private float countCooldown;

    public SkillCooldownState(SkillItemController _skill) : base(_skill)
    {
    }

    public override void OnEnter()
    {
        countCooldown = skill.SkillConfig.cooldownTime;
        MyPlayer.Instance.Controller.SkillsManager.OnStartCoolDown.Dispatch(skill.SkillConfig.skillIndex);
        HCDebug.Log("Enter skill cooldown state", HcColor.Violet);
    }

    public override void OnExit()
    {
        MyPlayer.Instance.Controller.SkillsManager.OnDoneCoolDown.Dispatch(skill.SkillConfig.skillIndex);
    }

    public override SkillStateBase OnUpdate()
    {
        countCooldown -= Time.deltaTime;
        if(countCooldown <= 0)
        {
            return new SkillReadyState(skill);
        }
        else
        {
            return this;
        }
    }
}
