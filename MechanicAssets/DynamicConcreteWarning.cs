using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DynamicConcreteWarning : MonoBehaviour
{
    [Header("Move object")]
    public GameObject moveObject;
    [Header("Speed of movement(!değer düştükçe obje hızlanır!)")]
    public float speed;
    [Header("How many seconds for a new move?")]
    public float waitTime;
    [Header("Bools for checking current situation")]
    private bool canGoUP;
    private bool canGoDown;
    private bool isDown;



    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnGameOver, CantMove);
    }
    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnGameOver, CantMove);
    }
    public void CantMove()
    {
        this.enabled = false;
        StopAllCoroutines();
    }

    void Start()
    {

        isDown = false;
        StartCoroutine(DoMove());
    }

    private void GoDown()
    {
        moveObject.transform.DOLocalMoveY(-1.479f, speed).SetEase(Ease.OutSine)
        .OnComplete(() => { isDown = true; StartCoroutine(DoMove()); });
    }
    private void GoUp()
    {
        moveObject.transform.DOLocalMoveY(-0.5f, speed).SetEase(Ease.OutSine)
        .OnComplete(() => { isDown = false; StartCoroutine(DoMove()); });
    }
    IEnumerator DoMove()
    {
        yield return new WaitForSeconds(waitTime);
        if (isDown)
        {
            GoUp();
        }
        else if (!isDown)
        {
            GoDown();
        }


    }

}
