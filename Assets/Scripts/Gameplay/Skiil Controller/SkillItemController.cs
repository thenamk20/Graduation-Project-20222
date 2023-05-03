using CnControls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillItemController : MonoBehaviour
{
    [SerializeField]
    private SkillVisual skillVisual;

    [SerializeField]
    private int skillIndex;

    [SerializeField]
    private SKILL_AIM_TYPE aimType;

    bool isPrepare;

    void Start()
    {
        skillVisual.ToggleVisual(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPrepare) return;
        Vector3 dir = new Vector3(CnInputManager.GetAxis($"{GameConst.SkillJoystickHoz}{skillIndex}"), 0, CnInputManager.GetAxis($"{GameConst.SkillJoystickVer}{skillIndex}"));
        if(dir != Vector3.zero)
        {
            skillVisual.ToggleVisual(true);
            var rot = Quaternion.LookRotation(dir).eulerAngles;
            skillVisual.SkillIndicator.transform.rotation = Quaternion.Euler(0, rot.y, 0);
        }
        else
        {
            skillVisual.ToggleVisual(false);
            isPrepare = false;
        }
    }

    public void PrepareSkillDirection()
    {
        isPrepare = true;
    }

    public void Execute()
    {

    }
}

public enum SKILL_AIM_TYPE
{
    DIRECTION = 0,
    CIRCLE_DROP
}
