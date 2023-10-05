using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ErectableTornDynamic : MonoBehaviour
{
    [Header("Move object")]
    public GameObject moveObject;

    [Header("Speed of object")]
    public float speed;
    [Header("Waiting between moves")]
    public float waitTime;
    [Header("Bools")]


    [Header("Scripts")]
    [SerializeField] private LightColorChange lightColorChange;


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
        StopCoroutine(DoMove());
        if (lightColorChange != null) { lightColorChange.ChangeLightColor("green"); }
        this.enabled = false;
    }

    private void Start()
    {
        isDown = false;
        StartCoroutine(DoMove());
    }

    private void GoDown()
    {
        GameManager.instance.ChangeTagOfGameobject(moveObject, "Untagged");
        if (lightColorChange != null) { lightColorChange.ChangeLightColor("green"); }
        moveObject.transform.DOLocalRotate(new Vector3(0, 0, 45), speed)
        .OnComplete(() =>
        {
            StartCoroutine(DoMove());
            isDown = true;
        });
    }

    private void GoUp()
    {
        GameManager.instance.ChangeTagOfGameobject(moveObject, "Mechanic");
        if (lightColorChange != null) { lightColorChange.ChangeLightColor("red"); }
        moveObject.transform.DOLocalRotate(new Vector3(0, 0, 0), speed)
        .OnComplete(() =>
        {

            StartCoroutine(DoMove());
            isDown = false;
        });
    }

    IEnumerator DoMove()
    {
        yield return new WaitForSeconds(waitTime);
        if (isDown)
        {
            GoUp();
        }
        if (!isDown)
        {
            GoDown();
        }
    }
}
