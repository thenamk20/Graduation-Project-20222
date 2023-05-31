using CnControls;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

    public override void Execute()
    {
        if(lockRotating)
            MyPlayer.Instance.Controller.rotateable = false;
        if(lockMoving)
            MyPlayer.Instance.Controller.moveable = false;

        var rot = Quaternion.LookRotation(skillAimDir).eulerAngles;
        MyPlayer.Instance.Controller.transform.rotation = Quaternion.Euler(0, rot.y, 0);

        HCDebug.Log("Skill aim dir: " + skillAimDir);

        StartCoroutine(FireSequence());
    }

    IEnumerator FireSequence()
    {
        yield return new WaitForSecondsRealtime(delayAttack);
        Missile missile = PhotonNetwork.Instantiate(Path.Combine(GameConst.PhotonPrefabs, GameConst.MissileName), missileMount.position, missileMount.rotation).GetComponent<Missile>();
        yield return new WaitForSecondsRealtime(attackTime);

        if (lockRotating)
            MyPlayer.Instance.Controller.rotateable = true;
        if (lockMoving)
            MyPlayer.Instance.Controller.moveable = true;
    }
}

