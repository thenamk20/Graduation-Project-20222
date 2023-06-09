using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSpeedSkill : SkillItemController
{
    public override void Execute()
    {
        IsReady = false;
        PlayerCtrl.ChakraManager.ConsumeChakraForSkill();
        


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
}
