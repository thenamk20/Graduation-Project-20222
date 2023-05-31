using CnControls;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable, IPunObservable
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

    public SkillsManager SkillsManager => skillManager;

    public bool moveable = true;

    public bool rotateable = true;

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
        healthBar.SetDirectProgressValue(1);

        moveable = true;
        rotateable = true;
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
        if(moveable) characterController.SimpleMove(dir);
        if(rotateable) transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), 0.2f);
    }

    public void ReceiveDamage(int amount)
    {
        PV.RPC(nameof(RPC_ReceiveDamage), RpcTarget.All, amount);
    }

    [PunRPC]
    void RPC_ReceiveDamage(int amount)
    {
        if (PV.IsMine)
        {
            HCDebug.Log("Me receive damage", HcColor.Red);
            stats.currentHealth -= amount;

            UpdateHealthToOthers();

            float healthPercent = stats.currentHealth * 1.0f / stats.maxHealth;
            healthBar.SetDirectProgressValue(healthPercent);

            if(stats.currentHealth <= 0)
            {
                OnDied();
            }
        }
    }

    void OnDied()
    {
        playerManager.Die();
    }

    void UpdateHealthToOthers()
    {
        PV.RPC(nameof(UpdateHealth), RpcTarget.AllViaServer, stats.currentHealth);
    }

    [PunRPC]
    void UpdateHealth(int health)
    {
        if (PV.IsMine) return;
        HCDebug.Log("update other: " + health.ToString());
        stats.currentHealth = health;
        healthBar.SetDirectProgressValue(health  * 1.0f/ stats.maxHealth);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //HCDebug.Log("Serialzie View", HcColor.Green);
        //if (stream.IsWriting)
        //{
        //    // Sending data to other clients
        //    stream.SendNext(stats.currentHealth);
        //}
        //else if (stream.IsReading)
        //{
        //    // Receiving data from the server
        //    stats.currentHealth = (int)stream.ReceiveNext();
        //    healthBar.SetDirectProgressValue(stats.currentHealth / stats.maxHealth);
        //}
    }
}

[Serializable]
public class CharacterStats
{
    public int currentHealth;
    public int currentChakra;

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

        currentHealth = 100;
        currentChakra = 100;
    }
}

