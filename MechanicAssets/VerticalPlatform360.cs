using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class VerticalPlatform360 : MonoBehaviour, IMechanic
{

    [Header("Hareket Edecek Obje")]
    public GameObject moveObject;
    [Header("Hareket ne kadar hızlı gerçekleşecek(Daha düşük değer = Daha hızlı hareket!)")]
    public float speed;
    [Header("Araç triggera girdikten sonra kaç saniye bekleyip hareket başlayacak ?")]
    public float waitTime;
    [Header("Bools")]

    //Rotation of object
    private float rotateDegree;
    public SYS.Degree degree;
    [SerializeField] private PlatformHelper platformHelper;



    private void Start()
    {
        Debug.Log("GoUp");
        rotateDegree = moveObject.transform.localEulerAngles.y + rotateDegree;
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


    public void StartMove()
    {
        Debug.Log("StartMove");

    }
    private void GoUp()
    {
        Debug.Log("GoUp");
        var _sequence = DOTween.Sequence();
        _sequence.Append(moveObject.transform.DOLocalMoveY(2.5f, speed).SetEase(Ease.InSine));
        _sequence.Append(moveObject.transform.DOLocalRotate(new Vector3(0, rotateDegree, 0), speed).SetEase(Ease.InOutSine))
        .OnComplete(() =>
        {
            EventManager.Broadcast(GameEvent.OnPlatformOut);
            platformHelper.CloseThis();
        });

    }
    public void SetCarAsChild(Transform carTransform)
    {
        carTransform.transform.SetParent(moveObject.transform);
    }

    IEnumerator DoMove()
    {

        yield return new WaitForSeconds(waitTime);
        GoUp();
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
