using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleLoop : MonoBehaviour
{
    [SerializeField] private float timePerUnit;
    [SerializeField] private float min;
    [SerializeField] private float max;
    [SerializeField] private Ease easyType;

    [SerializeField] private float delay;

    [SerializeField] private bool unscaleTime;

    private void OnEnable()
    {
        transform.localScale = Vector3.one * min;
        transform.DOScale(Vector3.one * max, timePerUnit).SetUpdate(UpdateType.Normal, unscaleTime).OnComplete(() =>
        {

        }).SetLoops(-1, LoopType.Yoyo).SetDelay(delay);
    }

    void OnDisable()
    {
        DOTween.Kill(gameObject.transform);
        transform.localScale = Vector3.one;
    }
}