using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CarCollider : MonoBehaviour
{
    [SerializeField] private GameObject explosionWheelObject;
    private int crashCount;
    private Material _otherMaterial;
    private GameObject lastHitObject;


    public static CarCollider instance;
    private void Awake()
    {
        instance = this;
    }

    public void OnCollisionEnter(Collision other)
    {
        if (!GameManager.instance.isGameOver && !GameManager.instance.isLevelPass)
        {
            Vector3 contactPoint = other.contacts[0].point;
            if (other.gameObject.tag.Equals("Mechanic"))
            {
                CollisionState(other.gameObject, contactPoint);
                GameOver(other.gameObject, contactPoint);

            }
            else if (other.gameObject.tag.Equals("NonMechanic"))
            {
                CollideWithNonMechanic(other.gameObject, contactPoint);
            }
        }
    }

    public void CollideWithNonMechanic(GameObject otherGo, Vector3 contactPoint)
    {
        StartCoroutine(LockOtherObject(otherGo, 0.5f));
        CollisionState(otherGo, contactPoint);

        //If other gameobject has rigidbody add force to other obj
        if (otherGo.GetComponent<Rigidbody>() != null)
        {
            Rigidbody rb = otherGo.gameObject.GetComponent<Rigidbody>();
            float _forcePower = (CarController.instance.Get_Speed / 2.1f) * rb.mass;
            rb.AddForce(Vector3.up * _forcePower, ForceMode.Impulse);
        }

        if (otherGo.TryGetComponent<MeshRenderer>(out MeshRenderer meshRenderer))
        {
            _otherMaterial = otherGo.GetComponent<MeshRenderer>().material;
        }
        else if (otherGo.TryGetComponent<MeshRenderer>(out MeshRenderer meshRendrer1))
        {
            _otherMaterial = otherGo.transform.parent.GetComponentInParent<MeshRenderer>().material;
        }
        else
        {
            _otherMaterial = otherGo.transform.parent.GetComponentInChildren<MeshRenderer>().material;
        }

        crashCount++;
        if (crashCount == 3)
        {
            GameOver(otherGo, contactPoint);
        }
        else
        {
            StarManager.instance.DecreaseStars();
        }
    }


    private void CollisionState(GameObject otherGo, Vector3 contactPoint)
    {
        GameManager.instance.ShowCollisionImage(contactPoint);
        PlayCollisionSfx(otherGo);
        FeedbackManager.instance.Play_crashFeedback();
    }

    private void PlayCollisionSfx(GameObject otherGo)
    {
        var collisionLayer = otherGo.layer;
        var layerMask = LayerMask.LayerToName(collisionLayer);
        switch (layerMask)
        {
            case "Metal":
                AudioManager.instance.PlayMetalCollisionSFX();
                break;
            case "Concrete":
                AudioManager.instance.PlayConcreteCollisionSFX();
                break;
            case "Glass":
                AudioManager.instance.PlayGlassCollisionSFX();
                break;
            case "RigidbodyNonMechanics":
                AudioManager.instance.PlayMetalCollisionSFX();
                break;
        }
    }

    private void GameOver(GameObject otherGo, Vector3 contactPoint)
    {
        EventManager.Broadcast(GameEvent.OnGameOver);
        otherGo.layer = LayerMask.NameToLayer("CollidedGameobject");

        //Explosion
        if (otherGo.GetComponent<Explosion>() != null)
        {
            otherGo.GetComponent<Explosion>().Boom(1.8f);
        }

        //Game over camera follow and lookat object
        GameObject newTransform = new GameObject();
        var pos = GameObject.FindGameObjectWithTag("Player").transform.position;
        newTransform.transform.position = new Vector3(pos.x, pos.y + 1, pos.z);
        newTransform.AddComponent<MoveTowardsTarget>();
        newTransform.GetComponent<MoveTowardsTarget>().target = contactPoint;
        CineExtension.instance.GameOverCamera(newTransform.transform, otherGo.gameObject.transform);


        //Changing collision gameobjects material
        if (otherGo.gameObject.GetComponent<MeshRenderer>() != null)
        {
            _otherMaterial = otherGo.gameObject.GetComponent<MeshRenderer>().material;
        }
        else
        {
            MeshRenderer[] _otherMaterials = otherGo.gameObject.GetComponentsInChildren<MeshRenderer>();

            foreach (var item in _otherMaterials)
            {
                _otherMaterial = item.material;
            }
        }
        GameManager.instance.ChangeCollisionMaterial(_otherMaterial);
    }

    public IEnumerator LockOtherObject(GameObject otherObject, float lockTimer)
    {
        Debug.Log("Object Locked");
        List<GameObject> list = GetAllChilds(otherObject.transform.parent.gameObject);

        otherObject.transform.parent.gameObject.tag = "Locked";
        foreach (var item in list)
        {
            item.tag = "Locked";
        }
        yield return new WaitForSeconds(lockTimer);

        otherObject.tag = "NonMechanic";
        foreach (var item in list)
        {
            item.tag = "NonMechanic";
        }
    }



    public List<GameObject> GetAllChilds(GameObject Go)
    {
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < Go.transform.childCount; i++)
        {
            list.Add(Go.transform.GetChild(i).gameObject);
        }
        return list;
    }

}

