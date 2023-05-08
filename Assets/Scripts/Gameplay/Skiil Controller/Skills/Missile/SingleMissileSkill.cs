using CnControls;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class SingleMissileSkill : SkillItemController
{
    [SerializeField]
    private GameObject missilePrefab;

    [SerializeField]
    private Transform missileMount;

    [SerializeField]
    private float delayAttack = 0;

    [SerializeField]
    private float attackTime = 0.5f;

    [SerializeField]
    private bool lockMoving = false;

    [SerializeField]
    private bool lockRotating = true;

    private Vector3 skillAimDir;

    void Update()
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

    public override void PrepareSkillDirection()
    {
        isPrepare = true;
        skillVisual.ToggleVisual(true);
    }

    public override void Execute(SkillMessage message)
    {
        MissileMessage missileMessage = (MissileMessage)message;

        missileMessage.myPlayer.RPCLookDirection(missileMessage.animDirection);
        if(missileMessage.isLockMoving) missileMessage.myPlayer.isFreeMoving = false;
        if (missileMessage.isLockRotating) missileMessage.myPlayer.isFreeRotating = false;

        StartCoroutine(AttackAction(missileMessage));
    }

    IEnumerator AttackAction(MissileMessage missileMessage)
    {
        yield return new WaitForSeconds(missileMessage.delayTime);

        var rot = Quaternion.LookRotation(missileMessage.animDirection).eulerAngles;
        Vector3 spawnPoint = myPlayer.MissileMount.position;
        HCDebug.Log("offset: " + missileMessage.spawnPointOffset);

        GameObject missile = Instantiate(missilePrefab, spawnPoint, Quaternion.Euler(0, rot.y, 0));
        NetworkServer.Spawn(missile);

        yield return new WaitForSeconds(missileMessage.attackTime);
        if (missileMessage.isLockMoving) missileMessage.myPlayer.isFreeMoving = true;
        if (missileMessage.isLockRotating) missileMessage.myPlayer.isFreeRotating = true;
    }

    public override SkillMessage GetSkillMessage()
    {
        return new MissileMessage
        {
            animDirection = skillAimDir,
            isLockMoving = lockMoving,
            isLockRotating = lockRotating,
            attackTime = attackTime,
            delayTime = delayAttack,
            spawnPointOffset = missileMount.localPosition,
            myPlayer = myPlayer
        };
    }
}

public class MissileMessage : SkillMessage
{
    public SKILL_AIM_TYPE skillAimType;
    public bool isLockMoving;
    public bool isLockRotating;
    public Vector3 animDirection;
    public float attackTime;
    public float delayTime;
    public Vector3 spawnPointOffset;
    public Player myPlayer;
}
