using CnControls;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashAttack : SkillItemController
{
    [SerializeField]
    private Collider hitBoxCollider;

    [SerializeField]
    private float delayAttack = 0;

    [SerializeField]
    private float attackTime = 0.5f;

    [SerializeField]
    private bool lockMoving = false;

    [SerializeField]
    private bool lockRotating = true;

    [SerializeField]
    private PhotonView playerPV;

    private IDamageable owner;

    private Vector3 skillAimDir;

    void Start()
    {
        hitBoxCollider.enabled = false;
        owner = GetComponentInParent<IDamageable>();
        IsReady = true;
        skillVisual?.ToggleVisual(false);
    }

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

        StartCoroutine(SlashSequence());
    }

    IEnumerator SlashSequence()
    {
        yield return new WaitForSecondsRealtime(delayAttack);
        hitBoxCollider.enabled = true;

        yield return new WaitForSecondsRealtime(attackTime);
        hitBoxCollider.enabled = false;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameConst.DamageableObject))
        {
            if (playerPV.IsMine && other.TryGetComponent(out IDamageable damageableObject))
            {
                if (damageableObject == owner) return;
                HCDebug.Log("Missile deal damage");
                damageableObject?.ReceiveDamage(30);
            }
        }
    }
}
