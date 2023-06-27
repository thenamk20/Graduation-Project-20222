using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffItemVFX : HCMonoBehaviour
{
    [SerializeField]
    private PhotonView PV;

    [SerializeField] private float timeExist = 1f;

    private Transform target;

    void Start()
    {
        if (PV.IsMine)
        {
            StartCoroutine(DelayDestroy());
        }
    }

    public void Init(Transform _target, int _viewID)
    {
        if (PV.IsMine)
        {
            target = _target;
            PV.RPC(nameof(RPC_UpdateTarget), RpcTarget.Others, _viewID);
        }
            
    }

    private void Update()
    {
        if(target != null)
            transform.position = target.position;
    }

    IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(timeExist);
        PhotonNetwork.Destroy(gameObject);
    }

    [PunRPC]
    void RPC_UpdateTarget(int _viewID)
    {
        target = PhotonView.Find(_viewID).gameObject.transform;
    }
}
