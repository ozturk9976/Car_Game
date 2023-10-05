using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LaserOnOff
{
    Yes,
    No
}

public class LaserRaycaster : MonoBehaviour
{
    [SerializeField] private LaserOnOff laserOnOff;
    [SerializeField] Color32 laserColor;
    [SerializeField] private LineRenderer lineRenderer;
    [Header("Rotation Speed")]

    [SerializeField] Transform startPos;
    private Vector3 startLinePos;
    private Vector3 endLinePos;




    [Header("Laser Lenght")]
    [SerializeField] float laserLenght;


    [Header("Eğer lazer açılıp kapanmıyacvaksa burayı boş bırak!")]
    [SerializeField] private float laserOnOffTimer;
    bool canRaycast;

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnGameOver, OnGameOver);
        EventManager.AddHandler(GameEvent.OnLevelPass, OnGameOver);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnGameOver, OnGameOver);
        EventManager.RemoveHandler(GameEvent.OnLevelPass, OnGameOver);
    }
    private void Start()
    {
        lineRenderer.enabled = true;
        canRaycast = true;
        lineRenderer.startColor = laserColor;
        lineRenderer.endColor = laserColor;

        switch (laserOnOff)
        {
            case LaserOnOff.Yes:
                StartCoroutine(OnOff());
                break;
            case LaserOnOff.No:
                break;
        }

    }

    private void Update()
    {

        if (canRaycast)
        {
            RaycastHit hit;
            if (Physics.Raycast(startPos.position, startPos.transform.forward, out hit, laserLenght))
            {
                // Debug.DrawRay(startPos.position, startPos.transform.forward * 15, Color.blue, 1);
                endLinePos = hit.point;

                if (hit.collider.gameObject.tag.Equals("Player"))
                {
                    GetComponentInParent<Explosion>().Boom(0.1f);
                    EventManager.Broadcast(GameEvent.OnGameOver);
                    this.enabled = false;
                    lineRenderer.enabled = false;
                }

            }
            else
            {
                endLinePos = startPos.position + startPos.transform.forward * laserLenght;
            }
        }

        lineRenderer.SetPosition(0, startPos.transform.position);
        lineRenderer.SetPosition(1, endLinePos);

    }

    private IEnumerator OnOff()
    {
        while (true)
        {
            yield return new WaitForSeconds(laserOnOffTimer);
            lineRenderer.enabled = false;
            canRaycast = false;
            yield return new WaitForSeconds(laserOnOffTimer);
            lineRenderer.enabled = true;
            canRaycast = true;
        }
    }

    private void OnGameOver()
    {
        this.enabled = false;
        lineRenderer.enabled = false;
        StopAllCoroutines();
    }
}
