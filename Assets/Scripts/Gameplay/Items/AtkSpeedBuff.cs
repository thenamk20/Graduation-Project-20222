using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkSpeedBuff : BuffItem
{
    [SerializeField] private float increaseChakraAmount;

    public override void ClaimBuff(PlayerController player)
    {
        player.IncreaseChakraRestore(increaseChakraAmount);
    }
}
