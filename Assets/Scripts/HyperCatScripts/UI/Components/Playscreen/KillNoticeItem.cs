using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KillNoticeItem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI killerNameText;

    [SerializeField]
    private TextMeshProUGUI victimNameText;

    public void Init(KillItem killerItem, KillItem victimItem, Action cb)
    {
        killerNameText.text = killerItem.playerName;
        victimNameText.text = victimItem.playerName;

        DOVirtual.DelayedCall(2f, () =>
        {
            cb?.Invoke();
        }).SetTarget(this);
    }
}
