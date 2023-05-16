using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillItemUI : HCMonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField]
    private SKILL_TRIGGER_TYPE triggerType;

    [SerializeField]
    private int skillIndex;

    public void Init()
    {
       
    }

    private void Start()
    {
       
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        HCDebug.Log("Pointer down");
    }
}


public enum SKILL_TRIGGER_TYPE { 
    DRAG = 0,
    CLICK = 1
}
