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

    private void Start()
    {
        range = initRange;
    }

    private void Update()
    {
        if(range > minRange)
        {
            range -= reduceSpeed * Time.deltaTime;
            maskTransform.localScale = range / 5f * Vector3.one;
        }
    }



}
