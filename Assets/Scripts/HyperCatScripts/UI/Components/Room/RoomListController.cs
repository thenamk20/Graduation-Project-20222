using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomListController : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform roomListContainer;

    [SerializeField] private GameObject roomListItemPrefab;

    private List<RoomListItem> cachedRoomList;

    private void Awake()
    {
        
    }

    public void Init()
    {
       
    }

    public override void OnEnable()
    {
        base.OnEnable();
        cachedRoomList = new List<RoomListItem>();
        CreateRoomList(NetworkManager.Instance.AllRoomList);
        HCDebug.Log("init room list, get from network Manager, count room: " + NetworkManager.Instance.AllRoomList.Count, HcColor.Green);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        CreateRoomList(roomList);
    }

    void CreateRoomList(List<RoomInfo> roomList)
    {
        foreach (RoomInfo room in roomList)
        {
            if (room.RemovedFromList)
            {
                RoomListItem item = cachedRoomList.Find(x => x.RoomInfo.Name == room.Name);
                if (item != null)
                {
                    cachedRoomList.Remove(item);
                    Destroy(item.gameObject);
                }
            }
            else
            {
                RoomListItem newItem = Instantiate(roomListItemPrefab, roomListContainer).GetComponent<RoomListItem>();
                if (newItem != null)
                {
                    newItem.Init(room);
                    cachedRoomList.Add(newItem);
                }
            }
        }
    }

    public override void OnDisable()
    {
        base.OnDisable();
        foreach(Transform item in roomListContainer)
        {
            Destroy(item.gameObject);
        }
        cachedRoomList.Clear();
    }
}
