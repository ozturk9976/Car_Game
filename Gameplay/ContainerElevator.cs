using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ContainerElevator : MonoBehaviour, IMechanic
{
    [Header("Move object")]
    public GameObject moveObject;
    [Header("Speed of object")]
    public float speed;

    [Header("Waiting between moves")]
    public float waitTime;
    [Header("Bools")]

    [SerializeField] GameObject[] animate;

    [SerializeField] private PlatformHelper platformHelper;


    public void StartMove()
    {
        StartCoroutine(DoMove());
    }
    private void GoUp()
    {
        var _sequence = DOTween.Sequence();
        _sequence.Append(moveObject.transform.DOLocalMoveY(2.5f, speed));
        _sequence.Append(moveObject.transform.DOLocalMoveZ(2.85f, speed))
        .OnComplete(() =>
         {
             EventManager.Broadcast(GameEvent.OnPlatformOut);
             platformHelper.CloseThis();
         });

    }

    IEnumerator DoMove()
    {
        yield return new WaitForSeconds(waitTime);
        GoUp();
    }

    public void IDoMove()
    {
        StartMove();
    }
    public void ISetCarAsChild(Transform carTransform)
    {
        carTransform.transform.SetParent(moveObject.transform);
    }
}
