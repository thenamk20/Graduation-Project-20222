using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class PopupDefeat : UIPanel
{
    [SerializeField]
    private TextMeshProUGUI topText;

    [SerializeField]
    private TextMeshProUGUI rewardText;

    [SerializeField] private GameObject panel1;

    [SerializeField] private GameObject panel2;

    public static PopupDefeat Instance { get; private set; }

    public override UiPanelType GetId()
    {
        return UiPanelType.PopupDefeat;
    }

    public static void Show()
    {
        var newInstance = (PopupDefeat) GUIManager.Instance.NewPanel(UiPanelType.PopupDefeat);
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
        topText.text = "You got top" + (BattleController.Instance.Players.Count);
        panel1.SetActive(true);
        panel2.SetActive(false);

        int rewardPoint = MyPlayer.Instance.Manager.CalculateRewardPoint();
        PlayFabManager.Instance.UpdateRewardPoints(rewardPoint);

        rewardText.text = rewardPoint.ToString();

        AudioAssistant.Shot(TypeSound.Defeat);
        AudioAssistant.Instance.Pause();
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

        PopupTransition.Show();
        StartCoroutine(DelayLeaveRoom());
    }

    IEnumerator DelayLeaveRoom()
    {
        yield return new WaitForSecondsRealtime(1f);
        Close();
        PhotonNetwork.LoadLevel((int)SceneIndex.Hall);
        PhotonNetwork.LeaveRoom();
    }

    public void Leave()
    {
        panel1.SetActive(false);
        panel2.SetActive(true);
    }
}