using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacterButton : MonoBehaviour
{
    [SerializeField] private GameObject activeBorder;

    [SerializeField] private int buttonIndex;

    private Action<int> onClick;

    public void Setup(Action<int> _onClick)
    {
        onClick = _onClick;
    }

    public void SetActive(bool isActive)
    {
        activeBorder.SetActive(isActive);
    }

    public void SelectCharacter()
    {
        onClick?.Invoke(buttonIndex);
    }
}
