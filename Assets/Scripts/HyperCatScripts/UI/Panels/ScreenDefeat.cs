using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenDefeat : UIPanel
{
    public static ScreenDefeat Instance { get; private set; }

    public override UiPanelType GetId()
    {
        return UiPanelType.ScreenDefeat;
    }

    public static void Show()
    {
        var newInstance = (ScreenDefeat) GUIManager.Instance.NewPanel(UiPanelType.ScreenDefeat);
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

            Player randomPlayer = players.FindAll(x => !x.IsLocal).GetRandom();

            PhotonNetwork.SetMasterClient(randomPlayer);
        }

        PhotonNetwork.LoadLevel((int)SceneIndex.Hall);
    }
}