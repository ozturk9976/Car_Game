using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ConcreteDynamic : MonoBehaviour
{
    [Header("Move object")]
    public GameObject moveObject;
    [Header("Speed of object")]
    public float speed;
    [Header("waiting between moves")]
    public float waitTime;
    [Header("Bools")]
    private bool isRight;

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnGameOver, CantMove);
        EventManager.AddHandler(GameEvent.OnLevelPass, CantMove);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnGameOver, CantMove);
        EventManager.RemoveHandler(GameEvent.OnLevelPass, CantMove);
    }

    public void CantMove()
    {
        StopAllCoroutines();
    }

    private void Start()
    {
        isRight = false;
        StartCoroutine(DoMove());
    }


    private void GoRight()
    {
        moveObject.transform.DOLocalMoveZ(1.9f, speed).SetEase(Ease.OutSine)
        .OnComplete(() => { isRight = true; StartCoroutine(DoMove()); });
    }

    private void GoLeft()
    {
        moveObject.transform.DOLocalMoveZ(-1.9f, speed).SetEase(Ease.OutSine)
        .OnComplete(() => { isRight = false; StartCoroutine(DoMove()); });
    }


    IEnumerator DoMove()
    {
        yield return new WaitForSeconds(waitTime);
        if (isRight)
        {
            GoLeft();
        }
        if (!isRight)
        {
            GoRight();
        }
    }
}
