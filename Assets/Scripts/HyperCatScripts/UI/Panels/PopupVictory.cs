using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PopupVictory : UIPanel
{
    [SerializeField]
    private TextMeshProUGUI rewardText;

    public static PopupVictory Instance { get; private set; }

    public override UiPanelType GetId()
    {
        return UiPanelType.PopupVictory;
    }

    public static void Show()
    {
        var newInstance = (PopupVictory) GUIManager.Instance.NewPanel(UiPanelType.PopupVictory);
        Instance = newInstance;
        newInstance.OnAppear();
    }

    public override void OnAppear()
    {
        if (isInited)
            return;

        base.OnAppear();

        Init();
    }

    private void Init()
    {
        int rewardPoint = MyPlayer.Instance.Manager.CalculateRewardPoint(isTop1: true) + 3;

        string prefix = (rewardPoint >= 0) ? "+" : "";
        rewardText.text = prefix + rewardPoint.ToString();

        PlayFabManager.Instance.UpdateRewardPoints(rewardPoint);

        
        AudioAssistant.Shot(TypeSound.Victory);
    }

    protected override void RegisterEvent()
    {
        base.RegisterEvent();
    }

    protected override void UnregisterEvent()
    {
        base.UnregisterEvent();
    }

    public override void OnDisappear()
    {
        base.OnDisappear();
        Instance = null;
    }

    public void GoBack()
    {
        AudioAssistant.Shot(TypeSound.Claim);
        if (PhotonNetwork.IsMasterClient)
        {
            List<Player> players = PhotonNetwork.PlayerList.ToList();

            List<Player> others = players.FindAll(x => !x.IsLocal);

            if (others.Count > 0)
            {
                Player randomPlayer = others.GetRandom();
                PhotonNetwork.SetMasterClient(randomPlayer);
            }
        }

        StartCoroutine(DelayLeaveRoom());
        PopupTransition.Show();
    }

    IEnumerator DelayLeaveRoom()
    {
        yield return new WaitForSecondsRealtime(1f);
        Close();
        PhotonNetwork.LoadLevel((int)SceneIndex.Hall);
        PhotonNetwork.LeaveRoom();
    }
}