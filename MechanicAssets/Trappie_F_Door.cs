using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Trappie_F_Door : MonoBehaviour
{
    [Header("Move object")]
    public GameObject moveObject;
    [Header("Speed of object")]
    public float speed;
    [Header("waiting between moves")]
    public float waitTime;
    [Header("Bools")]
    private bool isUp;
    [Header("Scripts")]
    [SerializeField] private LightColorChange lightColorChange;
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
        isUp = true;
        StartCoroutine(DoMove());
    }


    private void GoUp()
    {
        if (lightColorChange != null) { lightColorChange.ChangeLightColor("red"); }
        moveObject.transform.DOLocalRotate(new Vector3(0, 0, 0), speed).SetEase(Ease.InOutQuart)
        .OnComplete(() => { isUp = true; StartCoroutine(DoMove()); });
    }

    private void GoDown()
    {
        if (lightColorChange != null) { lightColorChange.ChangeLightColor("green"); }
        moveObject.transform.DOLocalRotate(new Vector3(0, 0, 90), speed).SetEase(Ease.InOutQuart)
        .OnComplete(() => { isUp = false; StartCoroutine(DoMove()); });
    }


    IEnumerator DoMove()
    {
        yield return new WaitForSeconds(waitTime);
        if (isUp)
        {
            GoDown();
        }
        if (!isUp)
        {
            GoUp();
        }
    }
}
