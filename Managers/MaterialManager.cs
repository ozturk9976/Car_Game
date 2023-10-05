using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class MaterialClass
{
    public Material _material;
    public TextureClass[] _textures;
}


[System.Serializable]
public class TextureClass
{
    public Texture2D _baseMap;
    public Texture2D _normalMap;
}


[System.Serializable]
public class MaterialManager : MonoBehaviour
{

    /// <summary>
    /// This class is used for changing in game textures 
    /// </summary>
    [SerializeField] private MaterialClass[] materialClasses;

    void Awake()
    {
        ChangeMaterialTextures();
    }

    public void ChangeMaterialTextures()
    {
        foreach (var mat in materialClasses)
        {
            int itemNum = Random.Range(0, mat._textures.Length);
            mat._material.SetTexture("_BaseMap", mat._textures[itemNum]._baseMap);
            mat._material.SetTexture("_BumpMap", mat._textures[itemNum]._normalMap);

        }
    }
}
