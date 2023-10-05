using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LaserTrapNoRotate_2 : MonoBehaviour
{
    [Header("Move object")]
    public GameObject moveObject;
    [Header("Speed of movement(!değer düştükçe obje hızlanır!)")]
    public float speed;

    [Header("Move Y (Y aksisteki hareketi)")]
    [SerializeField] float yValue;
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
        Move();
    }

    private void Move()
    {
        moveObject.transform.DOLocalMoveY(yValue, speed).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }


}
