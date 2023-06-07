using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillNoticeBoard : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private KillNoticeItem killNoticeItemPrefab;

    [SerializeField]
    private float space = 60f;

    [SerializeField]
    private Transform killItemsContainer;

    private int currentItem = 0;

    private List<Transform> killItems;

    private void Start()
    {
        currentItem = 0;
        killItems = new List<Transform>();
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if(targetPlayer == PhotonNetwork.LocalPlayer)
        {
            KillItem killer = new KillItem();
            KillItem victim = new KillItem();

            if (changedProps.ContainsKey("killer"))
            {
                if (changedProps.TryGetValue("killer", out object killerName))
                {
                    HCDebug.Log("Killer: " + (string)killerName, HcColor.Red);
                    killer.Setup(KILL_ROLE.KILLER, (string)killerName == PhotonNetwork.LocalPlayer.NickName, (string)killerName);
                }

            }

            if (changedProps.ContainsKey("victim"))
            {
                if (changedProps.TryGetValue("victim", out object victimName))
                {
                    HCDebug.Log("Victim: " + (string)victimName, HcColor.Red);
                    victim.Setup(KILL_ROLE.VICTIM, (string)victimName == PhotonNetwork.LocalPlayer.NickName, (string)victimName);
                }
            }

            //EventGlobalManager.Instance.OnKillNotice.Dispatch(killer, victim);

            KillNoticeItem killItem = Instantiate(killNoticeItemPrefab, killItemsContainer);
            killItem.transform.localPosition = new Vector3(0, 0, 0);
            MoveDownCurrentItems();

            killItems.Add(killItem.transform);
            killItem.Init(killer, victim, () =>
            {
                RemoveItem(killItem.transform);
            });

        }
    }

    void MoveDownCurrentItems()
    {
        foreach(var item in killItems)
        {
            item.transform.localPosition = item.transform.localPosition - new Vector3(0, space, 0);
        }
    }

    public void RemoveItem(Transform item)
    {
        killItems.Remove(item);
        Destroy(item.gameObject);
    }
}

public class KillItem
{
    public KILL_ROLE role;
    public bool isMe = false;
    public string playerName;

    public void Setup(KILL_ROLE _role, bool _isMe, string _playerName)
    {
        role = _role;
        isMe = _isMe;
        playerName = _playerName;
    }
}

public enum KILL_ROLE
{
    KILLER = 0,
    VICTIM = 1
}
