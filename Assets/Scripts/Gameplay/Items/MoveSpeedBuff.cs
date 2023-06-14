using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeedBuff : BuffItem
{
    [SerializeField] private float gain;

    public override void ClaimBuff(PlayerController player)
    {
        player.IncreaseMoveSpeed(gain);
    }
}
