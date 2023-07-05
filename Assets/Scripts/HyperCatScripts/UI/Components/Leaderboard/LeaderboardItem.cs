using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rankIndexText;

    [SerializeField] private TextMeshProUGUI playerNameText;

    [SerializeField] private TextMeshProUGUI scoreText;

    public void Init(string rank, string player, string score)
    {
        rankIndexText.text = rank;
        playerNameText.text = player;
        scoreText.text = score;
    }
}
