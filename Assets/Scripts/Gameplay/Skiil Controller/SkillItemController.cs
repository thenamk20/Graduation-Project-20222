using CnControls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillItemController : MonoBehaviour
{
    [SerializeField]
    protected SkillVisual skillVisual;

    [SerializeField]
    protected SkillConfig skillConfig;

    [SerializeField]
    protected int skillIndex;

    [SerializeField]
    protected SKILL_AIM_TYPE aimType;

    protected bool isPrepare;

    protected PlayerController PlayerCtrl => MyPlayer.Instance.Controller;

    public SkillConfig SkillConfig => skillConfig;

    public bool IsReady;

    void Start()
    {
        IsReady = true;
        skillVisual.ToggleVisual(false);
    }

    public abstract bool SkillAvailable();

    public abstract void PrepareSkillDirection();

    public abstract void Execute();

    public abstract void Upgrade();
}



public enum SKILL_AIM_TYPE
{
    DIRECTION = 0,
    CIRCLE_DROP
}
