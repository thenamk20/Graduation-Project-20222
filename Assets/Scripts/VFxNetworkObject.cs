using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFxNetworkObject : MonoBehaviour
{
    [SerializeField]
    private PhotonView PV;

    [SerializeField]
    private float existTime;

    private void Start()
    {
        StartCoroutine(DelayDestroy());
    }

    IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(existTime);

        if (PV.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }


}
