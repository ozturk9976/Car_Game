using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPanelBehaivour : MonoBehaviour
{
    [SerializeField] private List<GameObject> stars = new List<GameObject>();
    public static StarPanelBehaivour instance;
    private void Awake()
    {
        instance = this;
    }

    public void DestroyStars()
    {
        stars[stars.Count - 1].gameObject.GetComponent<StarBehaivour>().Destroy();
        stars.RemoveAt(stars.Count - 1);
    }
}
