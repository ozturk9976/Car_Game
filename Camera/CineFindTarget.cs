using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CineFindTarget : MonoBehaviour
{
    CinemachineFreeLook cm;

    private void Awake()
    {
        cm = GetComponent<CinemachineFreeLook>();
    }
    public void SetTarget(Transform target)
    {
        cm.m_Follow = target;
        cm.m_LookAt = target;
    }
}
