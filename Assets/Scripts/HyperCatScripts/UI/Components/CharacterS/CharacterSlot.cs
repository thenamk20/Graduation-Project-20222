using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSlot : MonoBehaviour
{
    [SerializeField] private GameObject characterInfoPanel;

    [SerializeField] private GameObject skillInfoPanel;

    [SerializeField] private List<SkillInfoIcon> skillsBtn;

    public void Init()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (SkillInfoIcon skill in skillsBtn)
        {
            skill.SetUp(HandleClickAnSkillIcon);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenCharacterInfo()
    {
        characterInfoPanel.SetActive(true);
        skillInfoPanel.SetActive(false);
    }

    public void OpenSkillsInfo()
    {
        characterInfoPanel.SetActive(false);
        skillInfoPanel.SetActive(true);
    }

    void HandleClickAnSkillIcon(int skillIndex)
    {
        OpenSkillsInfo();
        for(int i = 0; i < skillsBtn.Count; i++)
        {
            skillsBtn[i].ShowActive(i == skillIndex);
        }
    }
}
