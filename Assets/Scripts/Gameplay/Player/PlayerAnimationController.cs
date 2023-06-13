using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviourPun
{
    [SerializeField]
    private Animator playerAnimator;

    public const byte TRIGGER_ANIMATOR_EVENT = 0;

    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += HandleReceiveTriggerAnimatorEvent;
        HCDebug.Log("photon view:" + base.photonView.ViewID, HcColor.Red);
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= HandleReceiveTriggerAnimatorEvent;
    }

    public void PlayIdle()
    {
        // playerAnimator.SetFloat(GameConst.MovingSpeedHash, 0);
        playerAnimator.SetBool(GameConst.MovingBoolHash, false);
    }

    public void PlayMoving()
    {
        //playerAnimator.SetFloat(GameConst.MovingSpeedHash, 1);
        playerAnimator.SetBool(GameConst.MovingBoolHash, true);
    }

    public void PlayNormalAttack()
    {
        playerAnimator.SetTrigger(GameConst.NormalAttackHash);

        object[] data = new object[] { base.photonView.ViewID, GameConst.NormalAttackHash };

        PhotonNetwork.RaiseEvent(TRIGGER_ANIMATOR_EVENT, data, RaiseEventOptions.Default, SendOptions.SendReliable);

    }

    public void PlaySkill1()
    {
        playerAnimator.SetTrigger(GameConst.Skill1Hash);

        object[] data = new object[] {base.photonView.ViewID ,  GameConst.Skill1Hash };

        PhotonNetwork.RaiseEvent(TRIGGER_ANIMATOR_EVENT, data, RaiseEventOptions.Default, SendOptions.SendReliable);
    }

    public void PlayUltimate()
    {
        playerAnimator.SetTrigger(GameConst.UltimateHash);

        object[] data = new object[] { base.photonView.ViewID, GameConst.UltimateHash };

        PhotonNetwork.RaiseEvent(TRIGGER_ANIMATOR_EVENT, data, RaiseEventOptions.Default, SendOptions.SendReliable);
    }

    public void PlayDie()
    {
        playerAnimator.SetTrigger(GameConst.DieHash);

        object[] data = new object[] { base.photonView.ViewID, GameConst.DieHash };

        PhotonNetwork.RaiseEvent(TRIGGER_ANIMATOR_EVENT, data, RaiseEventOptions.Default, SendOptions.SendReliable);
    }

    void HandleReceiveTriggerAnimatorEvent(EventData obj)
    {
        if(obj.Code == TRIGGER_ANIMATOR_EVENT)
        {
            object[] data = (object[])obj.CustomData;
            int triggerHash = (int)data[1];
            int viewID = (int)data[0];

            if(base.photonView.ViewID == viewID)
                playerAnimator.SetTrigger(triggerHash);
        }
    }

    public void SetTriggerAttackAnimation(int attackIndex)
    {
        switch (attackIndex) {
            case 0:
                PlayNormalAttack();
                break;

            case 1:
                PlaySkill1();
                break;

            case 2:
                PlayUltimate();
                break;
        }
    }
}
