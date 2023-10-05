using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] GameObject objectToLook;
    [SerializeField] float lookAtSpeed;


    void Update()
    {
        Vector3 relativePos = objectToLook.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, lookAtSpeed * Time.deltaTime);
        Debug.DrawRay(transform.position, relativePos, Color.cyan);

    }
}
