using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RimCollisionDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("NonMechanic"))
        {

            if (other.gameObject.GetComponentInParent<Rigidbody>() != null)
            {
                if (!GameManager.instance.isGameOver && !GameManager.instance.isLevelPass)
                {
                    StartCoroutine(CarCollider.instance.LockOtherObject(other.gameObject, 0.5f));
                    CarCollider.instance.CollideWithNonMechanic(other.gameObject, other.gameObject.transform.position);
                    Rigidbody rig = other.gameObject.GetComponentInParent<Rigidbody>();
                    Vector3 direction = (other.transform.position - transform.position).normalized;
                    Vector3 contactPoint = other.ClosestPoint(other.transform.position);
                    rig.AddRelativeForce(direction * 40, ForceMode.Impulse);
                }
            }
        }
    }
}
