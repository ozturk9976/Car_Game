using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ConcreteDynamicPlatform : MonoBehaviour
{
    [Header("Move object")]
    public GameObject moveObject;
    [Header("Speed of object")]
    public float speed;
    [Header("waiting between moves")]
    public float waitTime;
    [Header("Bools")]
    private bool canMove;
    private bool isDown;
    private Sequence _sequence;

    [SerializeField] private Collider _collider;


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
        isDown = false;
        StartCoroutine(DoMove());
    }

    private void GoDown()
    {
        _sequence = DOTween.Sequence();
        _sequence
        .Append(moveObject.transform
        .DOLocalRotate(new Vector3(0, 0, 28f), speed)
        .SetEase(Ease.InSine))
        .OnComplete(() =>
        {
            _collider.enabled = false;
            GameManager.instance.ChangeTagOfGameobject(moveObject, "Untagged");
            isDown = true;
            StartCoroutine(DoMove());
        });
    }
    private void GoUp()
    {
        _collider.enabled = true;
        GameManager.instance.ChangeTagOfGameobject(moveObject, "Mechanic");
        _sequence = DOTween.Sequence();
        _sequence
        .Append(moveObject.transform
        .DOLocalRotate(new Vector3(0, 0, 0), speed)
        .SetEase(Ease.InSine))
       .OnComplete(() =>
       {

           isDown = false;
           StartCoroutine(DoMove());
       });
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
