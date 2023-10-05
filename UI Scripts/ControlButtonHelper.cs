using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ControlButtonHelper : MonoBehaviour
{

    public bool canMove;
    public bool canResize;

    private Vector3 pos;

    [SerializeField] Color32 activeColor;
    [SerializeField] Color32 deactiveColor;
    public Vector2 defaultPosition;
    public Vector2 defaultScale;

    public RectTransform rect;


    public void DisableObjectAfterInit()
    {
        this.gameObject.SetActive(false);
    }

    public void ChangePositionAndScale(float TransformX, float TransformY, float ScaleX, float ScaleY)
    {
        rect.anchoredPosition = new Vector2(TransformX, TransformY);
        rect.localScale = new Vector2(ScaleX, ScaleY);

    }

    public float Get_TransformX
    {
        get { return defaultPosition.x; }
    }
    public float Get_TransformY
    {
        get { return defaultPosition.y; }
    }
    public float Get_ScaleX
    {
        get { return defaultScale.x; }
    }
    public float Get_ScaleY
    {
        get { return defaultScale.y; }
    }
    public void ResizeButton_PointerDown()
    {
        canResize = true;
    }
    public void ResizeButton_PointerUp()
    {
        canResize = false;
    }
    public void MoveButton_PointerDown()
    {
        canMove = true;
    }
    public void MoveButton_PointerUp()
    {
        canMove = false;
    }

}