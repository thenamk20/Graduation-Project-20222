using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReceiveBuffItem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI buffText;

    public void Init(string text, Color _color)
    {
        buffText.text = text;
        buffText.color = _color;
    }

    private void Start()
    {
        transform.localPosition = transform.localPosition + new Vector3(0, Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
        transform.DOLocalMove(transform.localPosition + new Vector3(0, 1f, 0), 0.9f).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
