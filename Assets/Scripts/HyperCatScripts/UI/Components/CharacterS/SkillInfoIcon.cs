using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillInfoIcon : MonoBehaviour
{
    [SerializeField] private SkillConfig skillConfig;

    [SerializeField] private TextMeshProUGUI descriptionText;

    [SerializeField] private GameObject activeBorder;

    [SerializeField] private int skillIndex;

    private Action<int> onClick;

    public void SetUp(Action<int> _onClick)
    {
        onClick = _onClick;
    }

    void SetDescription()
    {
        descriptionText.text = skillConfig.description;
    }

    public void ShowSkillInfo()
    {
        SetDescription();
        onClick?.Invoke(skillIndex);
    }

    public void ShowActive(bool isActive)
    {
        activeBorder.SetActive(isActive);
    }
}
