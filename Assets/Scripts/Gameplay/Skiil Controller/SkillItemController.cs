using CnControls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillItemController : MonoBehaviour
{
    [SerializeField]
    protected SkillVisual skillVisual;

    [SerializeField]
    protected int skillIndex;

    [SerializeField]
    protected SKILL_AIM_TYPE aimType;

    [SerializeField]
    protected PlayerControllerOld myPlayer;

    protected bool isPrepare;

    void Start()
    {
        skillVisual.ToggleVisual(false);
    }

    public abstract bool SkillAvailable();

    public abstract void PrepareSkillDirection();

    public abstract void Execute();

}



public enum SKILL_AIM_TYPE
{
    DIRECTION = 0,
    CIRCLE_DROP
}
