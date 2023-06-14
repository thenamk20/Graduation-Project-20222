using CnControls;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeZoneSkill : SkillItemController
{
    [SerializeField]
    private float attackTime = 0.5f;

    [SerializeField]
    private bool lockMoving = false;

    [SerializeField]
    private bool lockRotating = true;

    [SerializeField]
    private PhotonView playerPV;

    [SerializeField]
    private float skillRange;

    [SerializeField]
    private GameObject explodeItemPrefab;

    private IDamageable owner;

    private Vector3 skillAimPos;

    private Vector3 skillAimDir;

    void Start()
    {
        owner = GetComponentInParent<IDamageable>();
        IsReady = true;
        skillVisual?.ToggleVisual(false);
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

        ExplodeZoneItem zoneItem = NetworkManager.Instance.InstantiateObject(explodeItemPrefab, skillAimPos, Quaternion.identity).GetComponent<ExplodeZoneItem>();
        zoneItem.Init(playerPV.ViewID);

        StartCoroutine(RestoreSequence());
    }

    IEnumerator RestoreSequence()
    {
        yield return new WaitForSecondsRealtime(attackTime);

        if (lockRotating)
            PlayerCtrl.rotateable = true;
        if (lockMoving)
            PlayerCtrl.moveable = true;
    }


    private void Update()
    {
        if (!isPrepare) return;
        Vector3 dir = new Vector3(CnInputManager.GetAxis($"{GameConst.SkillJoystickHoz}{skillIndex}"), 0, CnInputManager.GetAxis($"{GameConst.SkillJoystickVer}{skillIndex}"));
        if (dir != Vector3.zero)
        {
            skillAimDir = dir;
            skillAimPos = transform.position + dir * skillRange;
            skillVisual.SkillIndicator.transform.position = skillAimPos;
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

    public override bool SkillAvailable()
    {
        return IsReady && PlayerCtrl.ChakraManager.CheckChakraRequireForSkill;
    }

    public override void Upgrade()
    {
        
    }
}
