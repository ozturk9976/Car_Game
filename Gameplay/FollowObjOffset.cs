using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObjOffset : MonoBehaviour
{
    [SerializeField] float offsetX;
    [SerializeField] float offsetY;
    [SerializeField] float offsetZ;
    [SerializeField] float followSpeed;
    [SerializeField] GameObject objectToFollow;


    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(objectToFollow.transform.position.x
            + offsetX, objectToFollow.transform.position.y
            + offsetY, objectToFollow.transform.position.z
            + offsetZ), followSpeed);
    }
}
