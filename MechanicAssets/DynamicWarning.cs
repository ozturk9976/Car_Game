using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DynamicWarning : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private LightColorChange lightColorChange;
    [Header("Move object")]
    public GameObject moveObject;
    [Header("Speed of object")]
    public float speed;
    [Header("waiting between moves")]
    public float waitTime;
    [Header("Bools")]
    private bool canMove;
    private bool isDown;

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

        isDown = true;
        StartCoroutine(DoMove());
    }

    private void GoDown()
    {
        if (lightColorChange != null) { lightColorChange.ChangeLightColor("red"); }
        moveObject.transform
        .DOLocalRotate(new Vector3(0, 0, 0), speed)
          .OnComplete(() =>
          {
              isDown = true;
              StartCoroutine(DoMove());

          });
    }
    private void GoUp()
    {
        if (lightColorChange != null) { lightColorChange.ChangeLightColor("green"); }
        moveObject.transform.DOLocalRotate(new Vector3(80, 0, 0), speed).SetEase(Ease.OutQuart)
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
