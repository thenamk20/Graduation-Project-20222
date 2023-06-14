using CnControls;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashing : SkillItemController
{
    [SerializeField]
    private float skillTime = 0.5f;

    [SerializeField]
    private bool lockMoving = false;

    [SerializeField]
    private bool lockRotating = true;

    private Vector3 skillAimDir;

    private void Update()
    {
        if (!isPrepare) return;
        Vector3 dir = new Vector3(CnInputManager.GetAxis($"{GameConst.SkillJoystickHoz}{skillIndex}"), 0, CnInputManager.GetAxis($"{GameConst.SkillJoystickVer}{skillIndex}"));
        if (dir != Vector3.zero)
        {
            skillAimDir = dir;
            var rot = Quaternion.LookRotation(dir).eulerAngles;
            skillVisual.SkillIndicator.transform.rotation = Quaternion.Euler(0, rot.y, 0);
        }
        else
        {
            skillVisual.ToggleVisual(false);
            isPrepare = false;
        }
    }

    public override void Execute()
    {
        IsReady = false;
        PlayerCtrl.ChakraManager.ConsumeChakraForSkill();

        if (lockRotating)
            PlayerCtrl.rotateable = false;
        if (lockMoving)
            PlayerCtrl.moveable = false;

        var rot = Quaternion.LookRotation(skillAimDir).eulerAngles;
        PlayerCtrl.transform.rotation = Quaternion.Euler(0, rot.y, 0);

        HCDebug.Log("Skill aim dir: " + skillAimDir);

        StartCoroutine(DashSequence());
    }

    IEnumerator DashSequence()
    {
        PlayerCtrl.transform.DOMove(PlayerCtrl.transform.position + Vector3.Normalize(skillAimDir) * 4.5f, skillTime);
        yield return new WaitForSecondsRealtime(skillTime);

        if (lockRotating)
            PlayerCtrl.rotateable = true;
        if (lockMoving)
            PlayerCtrl.moveable = true;
    }

    public override void PrepareSkillDirection()
    {
        isPrepare = true;
        skillVisual.ToggleVisual(true);
    }

    public override bool SkillAvailable()
    {
        return IsReady && PlayerCtrl.ChakraManager.CheckChakraRequireForSkill;
    }

    public override void Upgrade()
    {
        HCDebug.Log("Upgrade this skill", HcColor.Red);
    }
}
