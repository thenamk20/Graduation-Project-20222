using Mirror;
using Mirror.SimpleWeb;
using Sigtrap.Relays;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsManager : MonoBehaviour
{
    //public Relay<int> OnPrepareUseSkill = new Relay<int>();

    //public Relay<int> OnUseSkill = new Relay<int>();

    // Start is called before the first frame update
    [SerializeField]
    private List<SkillItemController> skills;

    public List<SkillItemController> Skills => skills;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ToggleVisual(int skillIndex, bool isEnable)
    {

    }

    public void ExecuteSkill(int skillIndex, Vector3 direction)
    {

    }

}
