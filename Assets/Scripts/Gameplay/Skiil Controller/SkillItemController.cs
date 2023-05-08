using CnControls;
using Mirror;
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
    protected Player myPlayer;

    protected bool isPrepare;

    void Start()
    {
        skillVisual.ToggleVisual(false);
    }

    public abstract void PrepareSkillDirection();

    public abstract void Execute(SkillMessage skillMessage);

    public abstract SkillMessage GetSkillMessage();
}

public class SkillMessage { 
    
}




public enum SKILL_AIM_TYPE
{
    DIRECTION = 0,
    CIRCLE_DROP
}
