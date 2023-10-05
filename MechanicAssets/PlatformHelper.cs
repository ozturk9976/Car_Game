using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlatformHelper : MonoBehaviour
{
    [SerializeField] private Collider wheelCollisionChecker;
    [SerializeField] float time;
    private int wheelCount;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Wheel"))
        {
            wheelCount++;

            if (wheelCount == 4)
            {


                IMechanic _ıMechanic = transform.GetComponentInParent<IMechanic>();
                EventManager.Broadcast(GameEvent.OnPlatformIn);
                if (_ıMechanic != null)
                {
                    _ıMechanic.IDoMove();
                    _ıMechanic.ISetCarAsChild(other.transform.root);
                }

            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Wheel"))
        {
            wheelCount--;
        }
    }

    public void CloseThis()
    {
        Destroy(this.gameObject);
        wheelCollisionChecker.enabled = false;
        this.enabled = false;
    }
}
