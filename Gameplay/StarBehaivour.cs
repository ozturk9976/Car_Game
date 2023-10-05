using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StarBehaivour : MonoBehaviour
{
    public void Destroy()
    {
        var _sequence = DOTween.Sequence();
        _sequence.Append(transform.DOScale(new Vector3(1.4f, 1.4f, 1.4f), 0.2f).SetEase(Ease.InOutQuart)).SetUpdate(true);
        _sequence.Append(transform.DOScale(Vector3.zero, 1f)).SetEase(Ease.InOutQuint).SetUpdate(true).OnComplete(() => { Destroy(gameObject); });
    }
}
