using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryChecker : MonoBehaviour
{

    [SerializeField] private PlayerController player;

    [SerializeField] private float intervalBurning;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IntervalCheckBoundary());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator IntervalCheckBoundary()
    {
        WaitForSeconds interval = new WaitForSeconds(intervalBurning);
        while (true)
        {
            yield return interval;
            CheckDamage();
        }
    }

    void CheckDamage()
    {
        if(Vector3.Distance(player.transform.position, BattleController.Instance.Spawner.DamageArea.transform.position) 
            > BattleController.Instance.Spawner.DamageArea.Range)
        {
            player.ReceiveDamage(40);
        }
        else
        {
            HCDebug.Log("not in damage area", HcColor.Green);
        }
    }
}
