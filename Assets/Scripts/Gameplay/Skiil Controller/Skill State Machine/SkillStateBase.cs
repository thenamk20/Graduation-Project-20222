using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillStateBase 
{
    protected SkillItemController skill;

    public SkillStateBase(SkillItemController _skill)
    {
        this.skill = _skill;
    }

    public abstract void OnEnter();

    public abstract SkillStateBase OnUpdate();

    public abstract void OnExit();

    protected bool isSkillReady => skill.IsReady;
}
