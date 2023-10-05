using DG.Tweening;
using System.Collections;
using UnityEngine;

public enum Where
{
    Down,
    Up
}

public class LiftingPack : MonoBehaviour, IMechanic
{
    public Where moveDirection;

    [Header("Move object")]
    public GameObject moveObject;
    [Header("Move Positions")]
    [SerializeField] float yPosUpside;
    [SerializeField] float yPosDownside;
    [Header("Speed of object")]
    public float speed;

    [Header("waiting between moves")]
    public float waitTime;
    [SerializeField] private PlatformHelper platformHelper;
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
        StopAllCoroutines();
    }

    public void StartMove()
    {
        StartCoroutine(DoMove());
    }

    private void GoUp()
    {
        moveObject.transform.DOLocalMoveY(yPosUpside, speed).SetEase(Ease.OutSine)
          .OnComplete(() =>
          {
              EventManager.Broadcast(GameEvent.OnPlatformOut);
              platformHelper.CloseThis();
              this.enabled = false;
          });
    }

    private void GoDown()
    {
        moveObject.transform.DOLocalMoveY(yPosDownside, speed).SetEase(Ease.OutSine)
         .OnComplete(() =>
         {
             EventManager.Broadcast(GameEvent.OnPlatformOut);
             platformHelper.CloseThis();
             this.enabled = false;
         });

    }


    IEnumerator DoMove()
    {
        yield return new WaitForSeconds(waitTime);

        switch (moveDirection)
        {
            case Where.Up:
                GoUp();
                break;
            case Where.Down:
                GoDown();
                break;
        }
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
