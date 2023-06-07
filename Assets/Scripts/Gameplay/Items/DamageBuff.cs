using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBuff : BuffItem
{
    [SerializeField] private float buffAmount = 0.1f;

    public override void ClaimBuff(PlayerController player)
    {
        player.IncreaseDam(buffAmount);
    }
}
