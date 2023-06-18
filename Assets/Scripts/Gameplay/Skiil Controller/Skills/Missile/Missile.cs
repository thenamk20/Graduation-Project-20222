using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField]
    private PhotonView PV;

    [SerializeField]
    private float destroyAfter = 1;

    [SerializeField]
    private Rigidbody rigidBody;

    [SerializeField]
    public float force = 500;

    void Start()
    {
        rigidBody.AddForce(transform.forward * force);
        if (PV.IsMine)
        {
            StartCoroutine(DelayDestroy());
        }
    }

    void OnTriggerEnter(Collider co)
    {
        if (co.CompareTag(GameConst.DamageableObject))
        {
            IDamageable damageableObject;
            if(PV.IsMine && co.TryGetComponent<IDamageable>(out damageableObject))
            {
                HCDebug.Log("Missile deal damage");
                damageableObject.ReceiveDamage(30);
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }

    IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(destroyAfter);
        PhotonNetwork.Destroy(gameObject);
    }
}
