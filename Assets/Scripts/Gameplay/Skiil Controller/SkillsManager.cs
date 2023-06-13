
using Photon.Pun;
using Sigtrap.Relays;
using System.Collections.Generic;
using UnityEngine;

public class SkillsManager : MonoBehaviour
{
    //public Relay<int> OnPrepareUseSkill = new Relay<int>();

    //public Relay<int> OnUseSkill = new Relay<int>();

    // Start is called before the first frame update
    [SerializeField]
    private PlayerController controller;

    [SerializeField]
    private List<SkillItemController> skills;

    public List<SkillItemController> Skills => skills;

    public Relay<int> OnReady = new Relay<int>();

    public Relay<int> OnStartExecute = new Relay<int>();

    public Relay<int> OnDoneExecute = new Relay<int>();

    public Relay<int> OnStartCoolDown = new Relay<int>();

    public Relay<int> OnDoneCoolDown = new Relay<int>();

    public Relay OnClaimAnUpgradePoint = new Relay();

    public Relay OnUpgradeSkill = new Relay();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryUseSkill(int skillIndex)
    {
        if (skills[skillIndex].SkillAvailable())
        {
            skills[skillIndex].Execute();
            controller.AnimationController.SetTriggerAttackAnimation(skillIndex);
        }
    }

    public void UpgradeSkill(int skillIndex)
    {
        if(controller.stats.upgradePoint > 0)
        {
            skills[skillIndex].Upgrade();
            controller.ConsumeAnUpgradePoint();
            OnUpgradeSkill.Dispatch();
        }
    }
}
