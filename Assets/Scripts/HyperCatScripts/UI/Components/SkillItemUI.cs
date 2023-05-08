using Mirror;
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

    private Player myPlayer;

    public void Init()
    {
       
    }

    private void Start()
    {
        myPlayer = NetworkClient.localPlayer.gameObject.GetComponent<Player>();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SkillMessage skillMessage = myPlayer.SkillsManager.Skills[skillIndex].GetSkillMessage();
        HCDebug.Log(skillMessage, HcColor.Orange);
        myPlayer.CheckToCastSkill(skillIndex, skillMessage);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        HCDebug.Log("Pointer down");
        myPlayer.SkillsManager.Skills[skillIndex].PrepareSkillDirection();
    }
}


public enum SKILL_TRIGGER_TYPE { 
    DRAG = 0,
    CLICK = 1
}
