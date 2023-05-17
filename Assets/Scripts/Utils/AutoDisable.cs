using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDisable : MonoBehaviour
{
    [SerializeField]
    private float existTime = 2f;

    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 0.3f).OnComplete(() =>
        {
            transform.DOScale(Vector3.zero, 0.3f).SetDelay(existTime).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        });
    }

    private void OnDisable()
    {
        DOTween.Kill(this);
    }
}
