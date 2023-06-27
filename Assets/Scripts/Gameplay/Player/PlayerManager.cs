using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Photon.Realtime;
using System.Linq;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerManager : MonoBehaviour
{
    public bool isAlive;

    private PhotonView PV;

    public Vector3 spawnPoint;

    public int killsCount;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        BattleController.Instance.AddPlayer(this);
        BattleController.Instance.OnEndBattle.AddListener(HandleBattleEnd);
        EventGlobalManager.Instance.OnStartBattle.AddListener(CreateController);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateController()
    {
        if (!PV.IsMine) return;
        GameObject character = ConfigManager.Instance.characters[GameManager.Instance.data.user.currentCharacter].characterPrefab;
        PlayerController playerController = PhotonNetwork.Instantiate(Path.Combine(GameConst.PhotonPrefabs, character.name), spawnPoint, Quaternion.identity).GetComponent<PlayerController>();
        playerController.Init(this);
        MyPlayer.Instance.InitMyPlayer(this, playerController);
    }

    private void OnDestroy()
    {
        BattleController.Instance.RemovePlayer(this);
        BattleController.Instance.OnEndBattle.RemoveListener(HandleBattleEnd);
        EventGlobalManager.Instance.OnStartBattle.RemoveListener(CreateController);
    }

    public void Die(string victimName, string killerName)
    {
        EventGlobalManager.Instance.OnDoneFighting.Dispatch();
        PopupDefeat.Show();
        PV.RPC(nameof(RPC_Die), RpcTarget.All, victimName, killerName);
    }

    [PunRPC]
    void RPC_Die(string victimName, string killerName)
    {
        isAlive = false;
        NoticeKill(victimName, killerName);
        BattleController.Instance.RemovePlayer(this);
        BattleController.Instance.CheckEndBattle();
    }

    public void NoticeKill(string victimName, string killerName)
    {
        Hashtable hash = new Hashtable
        {
            { "killer", killerName },
            { "victim", victimName }
        };

        HCDebug.Log(hash);

        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    }

    void HandleBattleEnd()
    {
        if (PV.IsMine && isAlive)
        {
            EventGlobalManager.Instance.OnDoneFighting.Dispatch();
            PopupVictory.Show();
        }
    }

    public static PlayerManager Find(Player player)
    {
        return FindObjectsOfType<PlayerManager>().SingleOrDefault(x => x.PV.Owner == player);
    }

    public void GetKill()
    {
        PV.RPC(nameof(RPC_GainKill), RpcTarget.All);
    }

    [PunRPC]
    void RPC_GainKill()
    {
        killsCount++;
    }

    public void GetAnSpawnPoint(Vector3 pos)
    {
        PV.RPC(nameof(RPC_GetSpawnPoint), RpcTarget.All, pos);
    }

    [PunRPC]
    void RPC_GetSpawnPoint(Vector3 pos)
    {
        spawnPoint = pos;
    }
}
