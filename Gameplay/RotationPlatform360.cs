using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotationPlatform360 : MonoBehaviour, IMechanic
{

    [Header("Hareket Edecek Obje")]
    public GameObject moveObject;
    [Header("Hareket ne kadar hızlı gerçekleşecek(Daha düşük değer = Daha hızlı hareket!)")]
    public float speed;
    [Header("Araç triggera girdikten sonra kaç saniye bekleyip hareket başlayacak ?")]
    public float waitTime;
    [Header("Bools")]


    private bool isMovingRightNow;
    private float rotateDegree;
    private float startRotation;
    public SYS.Degree degree;

    [SerializeField] private PlatformHelper platformHelper;

    private void Start()
    {

        startRotation = this.transform.localEulerAngles.y;

        switch (degree)
        {
            case SYS.Degree.Degree_right_45:
                rotateDegree = 45;
                break;
            case SYS.Degree.Degree_right_90:
                rotateDegree = 90;
                break;
            case SYS.Degree.Degree_right_135:
                rotateDegree = 135;
                break;
            case SYS.Degree.Degree_right_180:
                rotateDegree = 180;
                break;
            case SYS.Degree.Degree_left_135:
                rotateDegree = 225;
                break;
            case SYS.Degree.Degree_left_90:
                rotateDegree = 270;
                break;
            case SYS.Degree.Degree_left_45:
                rotateDegree = 315;
                break;
        }
    }
    private void Turn()
    {
        var _sequence = DOTween.Sequence();
        _sequence.Append(moveObject.transform.DOLocalRotate(new Vector3(0, rotateDegree, 0), speed).SetEase(Ease.OutSine))
        .OnComplete(() =>
         {
             EventManager.Broadcast(GameEvent.OnPlatformOut);
             platformHelper.CloseThis();
         });
    }

    IEnumerator DoMove()
    {
        yield return new WaitForSeconds(waitTime);
        Turn();
    }

    public void IDoMove()
    {
        StartCoroutine(DoMove());
    }
    public void ISetCarAsChild(Transform carTransform)
    {
        carTransform.transform.SetParent(moveObject.transform);
    }
}
