using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkObjectAutoDestroy : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private PhotonView PV;

    [SerializeField]
    private float existTime;

    private void Start()
    {
        if (PV.IsMine)
        {
            StartCoroutine(DelayDestroy());
        }
    }

    IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(existTime);
        PhotonNetwork.Destroy(gameObject);
    }
}
