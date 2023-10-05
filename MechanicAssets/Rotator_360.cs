using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Rotator_360 : MonoBehaviour
{
    [SerializeField] private SYS.RotateType looptype;
    [SerializeField] private GameObject rotateObject;

    [SerializeField] private float speedOfRotation;
    Sequence _sequence;

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
        DOTween.KillAll();
    }


    void Start()
    {
        DoMove();
    }

    void DoMove()
    {
        switch (looptype)
        {
            case SYS.RotateType.Yoyo:
                rotateObject.transform.DORotate(new Vector3(0, 360, 0), speedOfRotation, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
                break;
            case SYS.RotateType.Restart:
                rotateObject.transform.DORotate(new Vector3(0, 360, 0), speedOfRotation, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
                break;
        }

    }
}
