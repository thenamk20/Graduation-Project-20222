using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : SingletonDestroy<BattleController>
{
    public CinemachineVirtualCamera followVcam;

    private CinemachineFramingTransposer followVcamFt;

    private void Start()
    {
        followVcamFt = followVcam.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    public void SetCamWatchMyPlayer(Transform myPlayer)
    {
        followVcam.Follow = myPlayer;
    }
}
