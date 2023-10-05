using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_BoxVisualizer : MonoBehaviour
{
    [SerializeField] private Color32 gizmosColor;
    private void OnDrawGizmos()
    {
        Gizmos.matrix = this.transform.localToWorldMatrix;
        Gizmos.color = gizmosColor;
        Gizmos.DrawCube(Vector3.zero, transform.localScale);

    }
}
