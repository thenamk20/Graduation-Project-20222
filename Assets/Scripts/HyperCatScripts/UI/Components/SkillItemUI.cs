using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillItemUI : HCMonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField]
    private SKILL_TRIGGER_TYPE triggerType;

    [SerializeField]
    private int skillIndex;

    [SerializeField]
    private GameObject executeCover;

    [SerializeField]
    private GameObject cooldownCover;

    [SerializeField]
    private Image cooldownFill;

    [SerializeField]
    private TextMeshProUGUI timeCoolDownText;

    [SerializeField]
    private GameObject upgradeSkillBtn;

    [SerializeField]
    private GameObject skillBase;

    private SkillConfig skillConfig;

    public void Init()
    {
        upgradeSkillBtn.SetActive(false);
        skillBase.SetActive(false);
    }

    private void Start()
    {
        MyPlayer.Instance.OnFinishSetupMyPlayer.AddListener(RegisterSkillPanel);
    }

    void RegisterSkillPanel()
    {
        PlayerCtrl.SkillsManager.OnReady.AddListener(HandleSkillReady);

        PlayerCtrl.SkillsManager.OnStartExecute.AddListener(HandleSkillStartExecute);
        PlayerCtrl.SkillsManager.OnDoneExecute.AddListener(HandleSkillDoneExecute);

        PlayerCtrl.SkillsManager.OnStartCoolDown.AddListener(HandleSkillStartCoolDown);
        PlayerCtrl.SkillsManager.OnDoneCoolDown.AddListener(HandleSkillDoneCoolDown);

        PlayerCtrl.SkillsManager.OnClaimAnUpgradePoint.AddListener(HandleClaimAnUpgradePoint);

        PlayerCtrl.SkillsManager.OnUpgradeSkill.AddListener(HandleClaimAnUpgradePoint);


        skillConfig = MyPlayer.Instance.Controller.SkillsManager.Skills[skillIndex].SkillConfig;

        cooldownCover.SetActive(false);
        executeCover.SetActive(false);
    }

    void UnRegisterSkillPanel()
    {
        PlayerCtrl.SkillsManager.OnReady.RemoveListener(HandleSkillReady);

        PlayerCtrl.SkillsManager.OnStartExecute.RemoveListener(HandleSkillStartExecute);
        PlayerCtrl.SkillsManager.OnDoneExecute.RemoveListener(HandleSkillDoneExecute);

        PlayerCtrl.SkillsManager.OnStartCoolDown.RemoveListener(HandleSkillStartCoolDown);
        PlayerCtrl.SkillsManager.OnDoneCoolDown.RemoveListener(HandleSkillDoneCoolDown);

        PlayerCtrl.SkillsManager.OnClaimAnUpgradePoint.RemoveListener(HandleClaimAnUpgradePoint);

        PlayerCtrl.SkillsManager.OnUpgradeSkill.RemoveListener(HandleClaimAnUpgradePoint);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PlayerCtrl.SkillsManager.TryUseSkill(skillIndex);
        skillBase.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PlayerCtrl.SkillsManager.Skills[skillIndex].PrepareSkillDirection();
        skillBase.SetActive(true);
    }

    
    void HandleSkillReady(int _skillIndex)
    {
        if (skillIndex != -skillIndex) return;
        cooldownCover.SetActive(false);
    }

    void HandleSkillStartExecute(int _skillIndex)
    {
        if (skillIndex != _skillIndex) return;
        executeCover.SetActive(true);
    }

    void HandleSkillDoneExecute(int _skillIndex)
    {
        if (skillIndex != _skillIndex) return;
        executeCover.SetActive(false);
    }

    void HandleSkillStartCoolDown(int _skillIndex)
    {
        if (skillIndex != _skillIndex) return;
        cooldownCover.SetActive(true);
        cooldownFill.fillAmount = 1;
        timeCoolDownText.text = skillConfig.cooldownTime.ToString();

        DOTween.To(() => cooldownFill.fillAmount, x => cooldownFill.fillAmount = x, 0, skillConfig.cooldownTime);
        DOTween.To(() => cooldownFill.fillAmount, 
                x => timeCoolDownText.text = (Mathf.Floor((x * skillConfig.cooldownTime) * 100) / 100f).ToString() , 
                0, 
                skillConfig.cooldownTime);
    }

    void HandleSkillDoneCoolDown(int _skillIndex)
    {
        if (skillIndex != _skillIndex) return;
        cooldownCover.SetActive(false);
    }

    void HandleClaimAnUpgradePoint()
    {
        int remainPoint = PlayerCtrl.stats.upgradePoint;
        upgradeSkillBtn.SetActive(remainPoint > 0);
    }   

    public void UpgradeSkill()
    {
        PlayerCtrl.SkillsManager.UpgradeSkill(skillIndex);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        MyPlayer.Instance?.OnFinishSetupMyPlayer.RemoveListener(RegisterSkillPanel);
        UnRegisterSkillPanel();
    }
}


public enum SKILL_TRIGGER_TYPE { 
    DRAG = 0,
    CLICK = 1
}
