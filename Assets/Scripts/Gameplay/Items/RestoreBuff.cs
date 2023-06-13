using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreBuff : BuffItem
{
    [SerializeField]
    private int restoreAmount = 20;

    public override void ClaimBuff(PlayerController player)
    {
        player.RestoreHealth(restoreAmount);
    }
}
