using CnControls;
using HighlightPlus;
using JetBrains.Annotations;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable, IHideable
{
    [SerializeField] private CharacterController characterController;

    [SerializeField] private SkillsManager skillManager;

    [SerializeField] private ProgressBar healthBar;

    [SerializeField] private ProgressBar chakraBar;

    [SerializeField] private GameObject characterModel;

    [SerializeField] private Transform missileMount;

    [SerializeField] private ChakraManager chakraManager;

    [SerializeField] private PlayerAnimationController animController;

    [SerializeField] private CapsuleCollider hitBoxCollider;

    [SerializeField] private BuffEffect buffEffect;

    [SerializeField] private TextMeshProUGUI playerNameText;

    [SerializeField] private CharacterStatConfig initStats;

    [SerializeField] private HighlightEffect effect;

    public CharacterStats stats;

    PhotonView PV;

    public PlayerManager playerManager;

    public SkillsManager SkillsManager => skillManager;

    public ChakraManager ChakraManager => chakraManager;

    public PlayerAnimationController AnimationController => animController;

    public PhotonView PhotonView => PV;

    public bool moveable = true;

    public bool rotateable = true;

    private bool isAlive;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (PV.IsMine)
        {
            BattleController.Instance.SetCamWatchMyPlayer(gameObject.transform);
            healthBar.SetMyPlayerHealthColor();
            PV.RPC(nameof(RPC_SetName), RpcTarget.All, PhotonNetwork.NickName);
        }
        else
        {
            healthBar.SetEnemyHealthColor();
        }

        isAlive = true;
        chakraBar.gameObject.SetActive(PV.IsMine);

        stats = new CharacterStats(initStats);
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
        {
            MovePlayer(dir * stats.moveSpeed);
        }
        else
        {
            animController.PlayIdle();
        }
    }

    void MovePlayer(Vector3 dir)
    {
        if (moveable)
        {
            characterController.SimpleMove(dir);
            animController.PlayMoving();
        }
        if(rotateable) transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), 0.2f);
    }

    public void ReceiveDamage(int amount)
    {
        PV.RPC(nameof(RPC_ReceiveDamage), RpcTarget.All, amount);
    }

    [PunRPC]
    void RPC_SetName(string name)
    {
        playerNameText.text = name;
    }


    [PunRPC]
    void RPC_ReceiveDamage(int amount, PhotonMessageInfo info)
    {
        if (!isAlive) return;

        effect.HitFX();
        HCDebug.Log("Me receive damage", HcColor.Red);
        stats.currentHealth -= amount;

        float healthPercent = stats.currentHealth * 1.0f / stats.maxHealth;
        healthBar.SetDirectProgressValue(healthPercent);

        if (stats.currentHealth <= 0)
        {
            isAlive = false;
            if (PV.IsMine)
                OnDied(info);
            else
            {
                HCDebug.Log("this other player die");
            }
        }
    }

    void OnDied(PhotonMessageInfo info)
    {
        isAlive = false;
        characterController.enabled = false;
        hitBoxCollider.enabled = false;
        healthBar.gameObject.SetActive(false);
        chakraBar.gameObject.SetActive(false);

        playerManager.Die(PhotonNetwork.LocalPlayer.NickName, info.Sender.NickName);
        animController.PlayDie();
        PlayerManager.Find(info.Sender).GetKill();
    }

    public bool IsFullHealth => (stats.currentHealth >= stats.maxHealth);

    #region buff
    //buff managing
    public void IncreaseDam(float gain)
    {
        buffEffect.ShowBuffEffectUIItem(ItemBuff.Damage, (int)(gain * 100));
        PV.RPC(nameof(RPC_IncreaseDam), RpcTarget.All, gain);
    }

    [PunRPC]
    void RPC_IncreaseDam(float gain)
    {
        stats.damMultiply += gain;
    }

    public void IncreaseHealth(float gainPercent)
    {
        buffEffect.ShowBuffEffectUIItem(ItemBuff.Health, (int)(gainPercent * 100));
        PV.RPC(nameof(RPC_IncreaseHealth), RpcTarget.All, gainPercent);
    }

    [PunRPC]
    void RPC_IncreaseHealth(float gainPercent)
    {
        int gain = (int)(gainPercent * stats.maxHealth);
        stats.maxHealth += gain;
        stats.currentHealth += gain;

        float healthPercent = stats.currentHealth * 1.0f / stats.maxHealth;
        healthBar.SetDirectProgressValue(healthPercent);
    }

    public void RestoreHealth(int amount)
    {
        buffEffect.ShowBuffEffectUIItem(ItemBuff.Restore, amount);
        PV.RPC(nameof(RPC_RestoreHealth), RpcTarget.All, amount);
    }

    [PunRPC]
    void RPC_RestoreHealth(int amount)
    {
        stats.currentHealth += amount;
        if(stats.currentHealth > stats.maxHealth)
        {
            stats.currentHealth = stats.maxHealth;
        }

        float healthPercent = stats.currentHealth * 1.0f / stats.maxHealth;
        healthBar.SetDirectProgressValue(healthPercent);
    }

    public void ClaimAnUpgradePoint()
    {
        PV.RPC(nameof(RPC_ClaimAnUpgradePoint), RpcTarget.All);
        skillManager.OnClaimAnUpgradePoint.Dispatch();
    }

    [PunRPC]
    void RPC_ClaimAnUpgradePoint()
    {
        stats.upgradePoint += 1;
    }

    public void IncreaseMoveSpeed(float gainPercent)
    {
        buffEffect.ShowBuffEffectUIItem(ItemBuff.MoveSpeed, (int)(gainPercent * 100));
        PV.RPC(nameof(RPC_IncreaseMoveSpeed), RpcTarget.All, gainPercent);
    }

    [PunRPC]
    void RPC_IncreaseMoveSpeed(float gainPercent)
    {
        stats.moveSpeed += stats.moveSpeed * gainPercent;
    }

    public void IncreaseChakraRestore(float gainPercent)
    {
        buffEffect.ShowBuffEffectUIItem(ItemBuff.AttackSpeed, (int)(gainPercent * 100));
        PV.RPC(nameof(RPC_IncreaseChakraRestore), RpcTarget.All, gainPercent);
    }

    [PunRPC]
    void RPC_IncreaseChakraRestore(float gainPercent)
    {
        stats.restoreChakraSpeed += stats.restoreChakraSpeed * gainPercent;
    }

    public void ConsumeAnUpgradePoint()
    {
        stats.upgradePoint -= 1;
        PV.RPC(nameof(RPC_ConsumeAnUpgradePoint), RpcTarget.Others);
    }

    [PunRPC]
    void RPC_ConsumeAnUpgradePoint()
    {
        stats.upgradePoint -= 1;
    }



    #endregion

    public PhotonView GetPV()
    {
        return PV;
    }

    public int currentBushId;
    public bool isOnBush;

    public void Hide(int bushId)
    {
        currentBushId = bushId;
        isOnBush = true;
        PV.RPC(nameof(RPC_HideOnBush), RpcTarget.All, bushId);
    }

    public void Appear(int bushId)
    {
        currentBushId = -1;
        isOnBush = false;
    }

    [PunRPC]
    void RPC_HideOnBush(int bushId)
    {
        if (PV.IsMine)
        {

        }
        else
        {
            if (isOnBush)
            {

            }
            else
            {

            }
        }
    }
}

[Serializable]
public class CharacterStats
{
    public int currentHealth;
    public float currentChakra;

    public float moveSpeed;
    public int maxHealth;

    public float restoreChakraSpeed = 10f;
    public float maxChakra;
    public int charkaSlots = 3;
    public float damMultiply = 1f;

    public int upgradePoint = 0;

    public float CharkaRequirePerSkill => maxChakra / charkaSlots;

    public CharacterStats(CharacterStatConfig statsConfig)
    {
        //moveSpeed = 3f;
        //maxHealth = 100;
        //maxChakra = 100;
        //restoreChakraSpeed = 10;

        //currentHealth = 100;
        //currentChakra = 100;

        moveSpeed = statsConfig.moveSpeed;
        maxHealth = statsConfig.health;
        maxChakra = statsConfig.chakra;
        restoreChakraSpeed = statsConfig.restoreChakraSpeed;

        currentHealth = statsConfig.health;
        currentChakra = statsConfig.chakra;
    }
}

