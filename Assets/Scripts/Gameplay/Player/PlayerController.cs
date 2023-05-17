using CnControls;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;

    [SerializeField] private SkillsManager skillManager;

    [SerializeField] private ProgressBar healthBar;

    [SerializeField] private ProgressBar chakraBar;

    [SerializeField] private GameObject characterModel;

    [SerializeField] private Transform missileMount;

    public CharacterStats stats;

    PhotonView PV;

    public PlayerManager playerManager;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (PV.IsMine)
        {
            BattleController.Instance.SetCamWatchMyPlayer(gameObject.transform);
        }

        stats = new CharacterStats();
    }

    public void Init(PlayerManager _playerManager)
    {
        playerManager = _playerManager;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine) return;

        Vector3 dir = new Vector3(CnInputManager.GetAxis("Horizontal"), 0, CnInputManager.GetAxis("Vertical"));
        if (dir != Vector3.zero && Vector3.SqrMagnitude(dir) > 0.05f)
            MovePlaye(dir * stats.moveSpeed);
    }

    void MovePlaye(Vector3 dir)
    {
        characterController.SimpleMove(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), 0.2f);
    }
}

[Serializable]
public class CharacterStats
{
    public float moveSpeed;
    public int maxHealth;

    public float restoreChakraSpeed = 2f;
    public float maxChakra;
    public int charkaSlots = 3;

    public float CharkaRequirePerSkill => maxChakra / charkaSlots;

    public CharacterStats()
    {
        moveSpeed = 2f;
        maxHealth = 100;
        maxChakra = 100;
    }
}

