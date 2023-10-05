using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IsDestructible
{
    yes,
    no
}

public class Explosion : MonoBehaviour
{
    [Header("Is gameobject will destroyed after explosion ?")]
    [SerializeField] AudioSource explosionSFX;
    [SerializeField] IsDestructible IsDestructible;
    [SerializeField] GameObject explosionParticle;
    [Space(15)]


    [SerializeField] float explosionPower;
    [SerializeField] float explosionRadius;

    int[] Hits;

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnGameOver, OnGameOver);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnGameOver, OnGameOver);
    }
    public void Boom(float time)
    {
        StartCoroutine(BoomIE(time));
    }

    private IEnumerator BoomIE(float time)
    {

        yield return new WaitForSecondsRealtime(time);
        explosionSFX.Play();
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        Instantiate(explosionParticle, transform.position, Quaternion.identity);
        FeedbackManager.instance.Play_gameoverExplosionFeedback();
        CineExtension.instance.Shake(1.5f, 2.3f, 1f);


        foreach (Collider item in colliders)
        {
            if (item.GetComponentInParent<Rigidbody>() != null)
            {
                item.GetComponentInParent<Rigidbody>().AddExplosionForce(explosionPower, transform.position, explosionRadius, 8f, ForceMode.Impulse);
            }
        }



        EventManager.Broadcast(GameEvent.OnGameOver);


        yield return new WaitForSecondsRealtime(0.1f);
        switch (IsDestructible)
        {
            case IsDestructible.yes:

                if (GetComponent<Rigidbody>() != null)
                {
                    GetComponent<MeshRenderer>().enabled = false;
                    GetComponent<Rigidbody>().isKinematic = true;
                }
                else
                {
                    GetComponent<MeshRenderer>().enabled = false;
                }
                break;
        }
    }

    private void OnGameOver()
    {
        this.enabled = false;
    }
}
