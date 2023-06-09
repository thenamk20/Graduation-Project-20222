using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void ReceiveDamage(int amount);

    public PhotonView GetPV();
}
