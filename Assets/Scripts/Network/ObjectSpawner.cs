using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private List<Transform> chestInitPoints;

    [SerializeField]
    private GameObject chestPrefab;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            foreach(var item in chestInitPoints)
            {
                NetworkManager.Instance.InstantiateRoomObject(chestPrefab, item.position, Quaternion.identity);
            }
        }
    }
}
