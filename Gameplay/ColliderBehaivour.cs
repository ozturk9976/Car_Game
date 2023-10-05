using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderBehaivour : MonoBehaviour
{
    [Header("Collider")]
    [SerializeField] private Collider _collider;

    public void TurnOn_Collider()
    {
        if (_collider.enabled == false)
        {
            _collider.enabled = true;
        }
    }
    public void TurnOff_Collider()
    {
        if (_collider.enabled == true)
        {
            _collider.enabled = false;
        }
    }
}
