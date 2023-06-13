using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IDamageable
{
    [SerializeField]
    private GameObject buffDamageItemPrefab;

    [SerializeField]
    private GameObject buffUpgradeItemPrefab;

    [SerializeField]
    private GameObject buffHealthItemPrefab;

    [SerializeField]
    private GameObject restoreHealthItemPrefab;

    [SerializeField]
    private GameObject buffMoveSpeedBuffItemPrefab;

    [SerializeField] 
    private ProgressBar healthBar;

    [SerializeField]
    private int chestMaxHealth = 50;

    PhotonView chestPV;

    private int currentHealth;

    private void Awake()
    {
        chestPV = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = chestMaxHealth;
        healthBar.SetDirectProgressValue(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReceiveDamage(int amount)
    {
        //update for others
        chestPV.RPC(nameof(RPC_UpdateHealth), RpcTarget.All, amount);

        //update health for this client
        //chestPV.TransferOwnership(PhotonNetwork.LocalPlayer);
    }

    [PunRPC]
    void RPC_UpdateHealth(int amountDamage)
    {
        currentHealth -= amountDamage;
        healthBar.SetDirectProgressValue(currentHealth * 1.0f / chestMaxHealth);

        if (currentHealth <= 0)
        {
            if (chestPV.IsMine)
                Explode();
        }
    }

    void Explode()
    {
        HCDebug.Log("Chest explode", HcColor.Red);
        PhotonNetwork.Destroy(gameObject);
        BuffItem item = NetworkManager.Instance.InstantiateRoomObject(restoreHealthItemPrefab, transform.position, Quaternion.identity).GetComponent<BuffItem>();
        item.Init();
    }
}
