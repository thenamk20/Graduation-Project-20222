using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PingDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pingText;

    private void Update()
    {
        int rtt = PhotonNetwork.GetPing();
        pingText.text = "RTT: " + rtt + "ms";
    }
}
