using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IDamageable
{
    [SerializeField] private List<GameObject> buffItemsList;

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
        int rd = Random.Range(0, 100);
        int itemCount = 0;

        if(rd < 40)
        {
            itemCount = 1;
        }
        else if(rd < 70)
        {
            itemCount = 2;
        }
        else if(rd < 90)
        {
            itemCount = 3;
        }
        else
        {
            itemCount = 4;
        }

        PhotonNetwork.Destroy(gameObject);

        for(int i=0; i< itemCount; i++)
        {
            BuffItem item = NetworkManager.Instance.InstantiateRoomObject(buffItemsList.GetRandom(), transform.position, Quaternion.identity).GetComponent<BuffItem>();
            item.Init();
        }
    }

    public PhotonView GetPV()
    {
        return chestPV;
    }
}
