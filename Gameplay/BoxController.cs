using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnGameOver, OnGameOver);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnGameOver, OnGameOver);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Prop") || other.gameObject.tag.Equals("Plane"))
        {
            Collided();
            EventManager.Broadcast(GameEvent.OnGameOver);
        }
    }

    private void Collided()
    {
        Vector3 _point = transform.position;

        GameObject newTrans = new GameObject();
        newTrans.transform.SetParent(this.transform);
        newTrans.transform.position = _point;

        CineExtension.instance.GameOverCamera(newTrans.transform, newTrans.transform);
        Material _material = this.GetComponent<MeshRenderer>().material;
        GameManager.instance.ChangeCollisionMaterial(_material);
    }
    private void OnGameOver()
    {
        this.enabled = false;
    }
}
