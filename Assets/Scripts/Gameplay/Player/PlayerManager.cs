using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    public bool isAlive;

    private PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PV.IsMine)
        {
            isAlive = true;
            CreateController();
        }
        BattleController.Instance.AddPlayer(this);
        BattleController.Instance.OnEndBattle.AddListener(HandleBattleEnd);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateController()
    {
        PlayerController playerController = PhotonNetwork.Instantiate(Path.Combine(GameConst.PhotonPrefabs, GameConst.PlayerController), Vector3.zero, Quaternion.identity).GetComponent<PlayerController>();
        playerController.Init(this);
        MyPlayer.Instance.InitMyPlayer(this, playerController);
    }

    private void OnDestroy()
    {
        BattleController.Instance.RemovePlayer(this);
        BattleController.Instance.OnEndBattle.RemoveListener(HandleBattleEnd);
    }

    public void Die()
    {
        ScreenDefeat.Show();
        PV.RPC(nameof(RPC_Die), RpcTarget.All);
    }

    [PunRPC]
    void RPC_Die()
    {
        isAlive = false;
        BattleController.Instance.CheckEndBattle();
    }

    void HandleBattleEnd()
    {
        if (isAlive)
        {
            ScreenVictory.Show();
        }
    }
}
