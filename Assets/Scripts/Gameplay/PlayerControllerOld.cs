using CnControls;
using Photon.Pun;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerControllerOld : MonoBehaviour
{
    //serializefield properties
    [SerializeField] private CharacterController characterController;

    [SerializeField] private SkillsManager skillManager;

    [SerializeField] private ProgressBar healthBar;

    [SerializeField] private ProgressBar chakraBar;

    [SerializeField] private GameObject characterModel;

    [SerializeField] private Transform missileMount;


    //public properties
    public GameObject VisualModel => characterModel;

    public SkillsManager SkillsManager => skillManager;

    public Transform MissileMount => missileMount;

    public bool AvailableToUseSkill => (chakra >= stats.CharkaRequirePerSkill);

    public bool isDamageAble;

    public bool isFreeMoving;

    public bool isFreeRotating;
    public int healthAmount;
    public float chakra;

    public CharacterStats stats;

    //private properties
    PhotonView PV;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

            Vector3 dir = new Vector3(CnInputManager.GetAxis("Horizontal"), 0, CnInputManager.GetAxis("Vertical"));
            if(dir != Vector3.zero && Vector3.SqrMagnitude(dir) > 0.05f)
                //MovePlayerTest(dir * stats.moveSpeed);
                
        RestoreChakra();
    }

    void RestoreChakra()
    {
        if (chakra < stats.maxChakra)
        {
            chakra += stats.restoreChakraSpeed * Time.deltaTime;
        }
    }

    public void CmdMovePlayer(Vector3 dir)
    {
        //TODO: logic check
        RPCMovePlayer(dir);
    }

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

    public void LookDirection(Vector3 direction)
    {
        var rot = Quaternion.LookRotation(direction).eulerAngles;
        transform.rotation = Quaternion.Euler(0, rot.y, 0);
    }


    public void RPCLookDirection(Vector3 direction)
    {
        LookDirection(direction);
    }

    public void ReceiveDamage(int amount)
    {
        HCDebug.Log("Recei damage with amount: " + amount);
        healthAmount -= amount;
    }

    void UpdateHealthBar(int oldAmount, int newAmount)
    {
        healthBar.SetDirectProgressValue(newAmount * 1.0f / stats.maxHealth);
    }

    void UpdateChakraBar(float oldAmount, float newAmount)
    {
        chakraBar.SetDirectProgressValue(newAmount * 1.0f / stats.maxChakra);
    }

    public void ComsumingCharka()
    {
        chakra -= stats.CharkaRequirePerSkill;
    }
}

