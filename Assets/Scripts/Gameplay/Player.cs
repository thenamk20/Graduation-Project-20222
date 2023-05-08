using CnControls;
using Mirror;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NetworkBehaviour, IDamageable
{
    [SerializeField] private CharacterController characterController;

    [SerializeField] private SkillsManager skillManager;

    [SerializeField] private ProgressBar healthBar;

    [SerializeField] private GameObject characterModel;

    [SerializeField] private Transform missileMount;

    public GameObject VisualModel => characterModel;

    public SkillsManager SkillsManager => skillManager;

    public Transform MissileMount => missileMount;

    [SyncVar(hook = nameof(UpdateHealthBar))]
    public int healthAmount;

    //[SyncVar]
    public bool isDamageAble;

    [SyncVar]
    public bool isFreeMoving;

    [SyncVar]
    public bool isFreeRotating;

    [SyncVar]
    private CharacterStats stats;

    // Start is called before the first frame update

    void Start()
    {
        stats = new CharacterStats
        {
            moveSpeed = 2f,
            maxHealth = 100
        };

        healthAmount = 100;

        if (isLocalPlayer)
        {
            LocalBattleController.Instance.SetCamWatchMyPlayer(transform);
            isFreeMoving = true;
            isFreeRotating = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            Vector3 dir = new Vector3(CnInputManager.GetAxis("Horizontal"), 0, CnInputManager.GetAxis("Vertical"));
            if(dir != Vector3.zero && Vector3.SqrMagnitude(dir) > 0.05f)
                //MovePlayerTest(dir * stats.moveSpeed);
                CmdMovePlayer(dir * stats.moveSpeed);
        }
    }

    [Command]
    public void CmdMovePlayer(Vector3 dir)
    {
        //TODO: logic check
        RPCMovePlayer(dir);
    }

    [TargetRpc]
    public void RPCMovePlayer(Vector3 dir)
    {
        if(isFreeMoving) characterController.SimpleMove(dir);
        if(isFreeRotating) transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), 0.2f);
    }

    public void MovePlayerTest(Vector3 dir)
    {
        if (isFreeMoving) characterController.SimpleMove(dir);
        if (isFreeRotating) transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), 0.2f);
    }

    public void CheckToCastSkill(int skillIndex, SkillMessage message)
    {
        CmdExecuteASkill(skillIndex, (MissileMessage)message);
    }

    [Command]
    public void CmdExecuteASkill(int skillIndex, MissileMessage message)
    {
        skillManager.Skills[skillIndex].Execute(message);
        HCDebug.Log(message, HcColor.Violet);
        RPCUseSkill();
    }

    [TargetRpc]
    public void RPCUseSkill()
    {
        HCDebug.Log("use skill");
    }

    public void LookDirection(Vector3 direction)
    {
        var rot = Quaternion.LookRotation(direction).eulerAngles;
        transform.rotation = Quaternion.Euler(0, rot.y, 0);
    }

    [TargetRpc]
    public void RPCLookDirection(Vector3 direction)
    {
        LookDirection(direction);
    }

    public void ReceiveDamage(int amount)
    {
        HCDebug.Log("Recei damage with amount: " + amount);
        healthAmount -= amount;
    }

    [ClientCallback]
    void UpdateHealthBar(int oldAmount, int newAmount)
    {
        healthBar.SetDirectProgressValue(newAmount * 1.0f / stats.maxHealth);
        HCDebug.Log(oldAmount);
        HCDebug.Log(newAmount);
        //healthBar.SetDirectProgressValue(0.3f);
    }
}

public class CharacterStats
{
    public float moveSpeed;
    public int maxHealth;
}
