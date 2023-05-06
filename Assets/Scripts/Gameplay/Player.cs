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

    public GameObject VisualModel => characterModel;

    public SkillsManager SkillsManager => skillManager;

    //[SyncVar(hook = nameof(UpdateHealthBar))]
    public int healthAmount;

    //[SyncVar]
    public bool isDamageAble;

    //[SyncVar]
    public bool isFreeMoving;

    //[SyncVar]
    public bool isFreeRotating;

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
        UpdateHealthBar(healthAmount, healthAmount);

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
        if(isFreeRotating) characterModel.transform.rotation = Quaternion.Lerp(characterModel.transform.rotation, Quaternion.LookRotation(dir), 0.2f);
    }

    public void MovePlayerTest(Vector3 dir)
    {
        if (isFreeMoving) characterController.SimpleMove(dir);
        if (isFreeRotating) characterModel.transform.rotation = Quaternion.Lerp(characterModel.transform.rotation, Quaternion.LookRotation(dir), 0.2f);
    }

    [Command]
    public void CmdExecuteASkill(int skillIndex, SkillMessage message)
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
        characterModel.transform.rotation = Quaternion.Euler(0, rot.y, 0);
    }

    public void ReceiveDamage(int amount)
    {
        healthAmount -= amount;
    }

    void UpdateHealthBar(int oldAmount, int newAmount)
    {
        healthBar.SetDirectProgressValue(newAmount * 1.0f / stats.maxHealth);
    }
}

public class CharacterStats
{
    public float moveSpeed;
    public int maxHealth;
}
