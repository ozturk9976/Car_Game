using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosCube : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 25, 25, 0.15f);
        Gizmos.DrawCube(transform.position, this.transform.localScale);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            EventManager.Broadcast(GameEvent.OnGameOver);
        }
    }
}
