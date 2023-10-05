using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CrashImageBehaivour : MonoBehaviour
{
    /// <summary>
    /// This class is used for in game crash sprite animations
    /// </summary>
    /// 

    private Sequence _sequence;
    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnGameOver, OnGameOver);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnGameOver, OnGameOver);
    }

    private void Start()
    {
        transform.DOScale(new Vector3(0.1f, 0.1f, 0.5f), 1f);
        _sequence = DOTween.Sequence();
        _sequence.Append(transform.DOLocalMoveY(transform.position.y + 2, 1.5f)).SetEase(Ease.OutQuart).SetUpdate(true);
        _sequence.Append(transform.DOScale(Vector3.zero, 0.5f)).SetUpdate(true).OnComplete(() =>
        {
            this.enabled = false;
            Destroy(this.gameObject);
        });
    }


    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }

    void OnGameOver()
    {
        Destroy(this.gameObject);
    }



}
