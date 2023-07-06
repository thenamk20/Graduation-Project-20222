using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSpeedSkill : SkillItemController
{
    private float cachedSpeed;

    [SerializeField]
    private GameObject buffFxObject;

    [SerializeField]
    private TypeSound skillSound;

    private GameObject buffVFx;

    public override void Execute()
    {
        IsReady = false;
        PlayerCtrl.ChakraManager.ConsumeChakraForSkill();

        cachedSpeed = PlayerCtrl.stats.moveSpeed;
        PlayerCtrl.stats.moveSpeed = cachedSpeed * 2;

        StartCoroutine(ResetSpeed());
        buffVFx = NetworkManager.Instance.InstantiateObject(buffFxObject, transform.position, Quaternion.identity);

        AudioAssistant.Shot(skillSound);
    }

    public override void PrepareSkillDirection()
    {
       
    }

    private void Update()
    {
        if(buffVFx != null)
        {
            buffVFx.transform.position = PlayerCtrl.transform.position;
        }
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
        yield return new WaitForSeconds(3f);
        PlayerCtrl.stats.moveSpeed = cachedSpeed;
        buffVFx = null;
    }
}
