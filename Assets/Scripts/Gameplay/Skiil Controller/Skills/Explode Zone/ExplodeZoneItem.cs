using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeZoneItem : MonoBehaviour
{
    [SerializeField]
    private PhotonView PV;

    [SerializeField]
    private SphereCollider hitboxCollider;

    [SerializeField]
    private float delayTime;

    [SerializeField]
    private float existTime;

    [SerializeField]
    private Renderer itemRenderer;

    [SerializeField]
    private LayerMask layerMask;

    private int ownerViewID;

    private List<IDamageable> currentTarget;

    public void Init(int _ownerViewID)
    {
        ownerViewID = _ownerViewID;
        PV.RPC(nameof(ShareOwnerViewID), RpcTarget.Others, _ownerViewID);
    }

    private void Start()
    {
        hitboxCollider.enabled = false;
        itemRenderer.enabled = false;
        currentTarget = new List<IDamageable>();
        StartCoroutine(ExplodeSequence());
    }

    IEnumerator ExplodeSequence(){
        yield return new WaitForSeconds(delayTime);
        hitboxCollider.enabled = true;
        itemRenderer.enabled = true;
        Collider[] targets = Physics.OverlapSphere(transform.position, hitboxCollider.radius * transform.lossyScale.x, layerMask);

        foreach(var item in targets)
        {
            if (item.CompareTag(GameConst.DamageableObject))
            {
                if (item.TryGetComponent(out IDamageable _target))
                {
                    if (!currentTarget.Contains(_target) && item.gameObject.GetPhotonView().ViewID != ownerViewID)
                    {
                        currentTarget.Add(_target);
                    }
                }
            }
        }

        foreach(var item in currentTarget)
        {
            CheckTarget(item);
        }

        yield return new WaitForSeconds(existTime);

        if (PV.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }

    private void CheckTarget(IDamageable other)
    {
        if (PV.IsMine)
        {
            other.ReceiveDamage(50);
        }
    }

    [PunRPC]
    void ShareOwnerViewID(int viewID)
    {
        ownerViewID = viewID;
    }
}
