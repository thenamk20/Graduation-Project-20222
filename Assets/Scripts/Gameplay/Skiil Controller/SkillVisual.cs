using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillVisual : MonoBehaviour
{
    [SerializeField]
    private GameObject skillRange;

    [SerializeField]
    private GameObject skillIndicator;

    public GameObject SkillIndicator => skillIndicator;

    public void ToggleVisual(bool isEnable)
    {
        gameObject.SetActive(isEnable);
    }
}
