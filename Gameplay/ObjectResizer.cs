using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectResizer : MonoBehaviour
{
    [SerializeField] private Vector3 objectSize;
    [SerializeField] private Transform objectToResize;
    void Start()
    {

        if (objectSize != null)
        {
            objectToResize.localScale = objectSize;
        }
        else
        {
            objectToResize.localScale = new Vector3(1, 1, 1);
        }
    }
}
