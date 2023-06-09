using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBuff : BuffItem
{
    public override void ClaimBuff(PlayerController player)
    {
        player.ClaimAnUpgradePoint();
    }
}
