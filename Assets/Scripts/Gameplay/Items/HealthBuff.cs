using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBuff : BuffItem
{
    [SerializeField]
    private float gainPercent;

    public override void ClaimBuff(PlayerController player)
    {
        player.IncreaseHealth(gainPercent);
    }
}
