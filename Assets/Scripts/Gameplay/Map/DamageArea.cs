using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class DamageArea : MonoBehaviourPun
{
    [SerializeField]
    private float initRange = 100f;

    [SerializeField]
    private float minRange = 10f;

    [SerializeField]
    private PhotonView PV;

    [SerializeField]
    private float reduceSpeed;

    [SerializeField]
    private Transform maskTransform; // scale = 1 <=> range = 5

    private float range;

    public float Range => range;

    private bool reducing = true;

    private void Start()
    {
        range = initRange;
        reducing = true;
        BattleController.Instance.OnEndBattle.AddListener(StopReducing);
    }

    private void Update()
    {
        if(range > minRange && reducing)
        {
            range -= reduceSpeed * Time.deltaTime;
            maskTransform.localScale = range / 5f * Vector3.one;
        }
    }


    void StopReducing()
    {
        reducing = false;
    }
}
