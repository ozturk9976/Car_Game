using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DynamicMetalWarning : MonoBehaviour
{
    [Header("Move object")]
    public GameObject moveObject;
    [Header("Speed of movement(!değer düştükçe obje hızlanır!)")]
    public float speed;
    [Header("How many seconds for a new move?")]
    public float waitTime;
    [Header("Bools for checking current situation")]
    private bool canMove;
    private bool isUp;

    [Header("Checking if object is moving right now and is car on trigger")]
    private bool isCarOnTrigger;


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

    void Start()
    {
        isUp = true;
        StartCoroutine(DoMove());
    }

    private void GoDown()
    {
        moveObject.transform.DOLocalMoveY(-0.866f, speed).SetEase(Ease.OutSine)
         .OnComplete(() =>
         {
             isUp = false;
             StartCoroutine(DoMove());
         });
    }

    private void GoUp()
    {
        moveObject.transform.DOLocalMoveY(-0.3f, speed).SetEase(Ease.OutSine)
        .OnComplete(() =>
        {
            isUp = true;
            StartCoroutine(DoMove());
        });
    }

    IEnumerator DoMove()
    {
        yield return new WaitForSeconds(waitTime);
        if (!isUp)
        {
            GoUp();
        }
        if (isUp)
        {
            GoDown();
        }
    }
}
