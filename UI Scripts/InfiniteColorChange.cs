using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    Light,
    Image
}

public class InfiniteColorChange : MonoBehaviour
{
    [SerializeField] private ObjectType objectType;
    [SerializeField] private Color32[] allColors;
    [SerializeField] private GameObject targetObject;

    [SerializeField] private float _waitTimeBetweenColors;
    [SerializeField] private float _speed;
    private Color32 _targetColor;
    private Color32 _currentColor;

    private void Start()
    {
        InvokeRepeating(nameof(SetNewColor), 0, _waitTimeBetweenColors);
        switch (objectType)
        {
            case ObjectType.Light:
                _currentColor = targetObject.GetComponent<Light>().color;
                break;
        }


    }
    private void Update()
    {
        _currentColor = Color.Lerp(_currentColor, _targetColor, _speed * Time.deltaTime);
    }

    private void SetNewColor()
    {
        int colorIndex = Random.Range(0, allColors.Length);
        Color32 _selectedColor = allColors[colorIndex];
        _targetColor = _selectedColor;
    }
}
