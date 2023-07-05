using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomListItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI roomName;

    private RoomInfo _roomInfo;

    public RoomInfo RoomInfo => _roomInfo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Init(RoomInfo roomInfo)
    {
        _roomInfo = roomInfo;
        roomName.text = roomInfo.Name;
    }

    public void Onclick()
    {
        Debug.Log("Click join room");
        NetworkManager.Instance.JoinRoom(_roomInfo);
        PopupRoom.Instance.SetLoadingMessage("Joining room...");
    }
}
