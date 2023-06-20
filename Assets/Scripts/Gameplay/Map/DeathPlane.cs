using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    [SerializeField]
    private PhotonView PV;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameConst.DamageableObject))
        {
            if (other.TryGetComponent(out IDamageable damageableObject))
            {
                if (damageableObject.GetPV().IsMine)
                {
                    damageableObject?.ReceiveDamage(9999);
                }
            }
        }
    }
}
