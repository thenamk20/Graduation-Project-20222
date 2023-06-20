using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObstacle : MonoBehaviour, IDamageable
{
    [SerializeField]
    private PhotonView PV;

    public PhotonView GetPV()
    {
        return PV;
    }

    public void ReceiveDamage(int amount)
    {
        return;
    }
}
