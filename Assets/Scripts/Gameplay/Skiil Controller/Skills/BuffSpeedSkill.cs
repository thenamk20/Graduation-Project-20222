using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSpeedSkill : SkillItemController
{
    private float cachedSpeed;

    public override void Execute()
    {
        IsReady = false;
        PlayerCtrl.ChakraManager.ConsumeChakraForSkill();

        cachedSpeed = PlayerCtrl.stats.moveSpeed;
        PlayerCtrl.stats.moveSpeed = cachedSpeed * 2;

        StartCoroutine(ResetSpeed());
    }

    public override void PrepareSkillDirection()
    {
       
    }

    public override bool SkillAvailable()
    {
        return IsReady && PlayerCtrl.ChakraManager.CheckChakraRequireForSkill;
    }

    public override void Upgrade()
    {
        HCDebug.Log("Upgrade this skill", HcColor.Red);
    }

    IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(4f);
        PlayerCtrl.stats.moveSpeed = cachedSpeed;
    }
}
