using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;




public class StaticMusicPlayer : MonoBehaviour
{
    /*[RuntimeInitializeOnLoadMethod]
    static void OnRuntimeInitialized()
    {
        AsyncOperationHandle<GameObject> asyncOperationHandle = Addressables.LoadAssetAsync<GameObject>("Theme Default Prefabs");

        asyncOperationHandle.Completed += AsyncOps_Completed;
        Debug.Log("Runtime initialized: First scene loaded: After Awake is called.");

       static void AsyncOps_Completed(AsyncOperationHandle<GameObject> asyncOps) { if (asyncOps.Status == AsyncOperationStatus.Succeeded) { Instantiate(asyncOps.Result); } }
    }
    */
    private static AudioClip[] ThemeMusics;

    public static StaticMusicPlayer instance;
    void Start()
    {
        // if (instance == null)
        // 
        //     instance = this;
        //     DontDestroyOnLoad(this.gameObject);
        // }
        // else if (instance != this)
        // {
        //     Destroy(this.gameObject);
        // }

        // int _musicID = Random.Range(1, 7);
        // gameObject.AddComponent<AudioSource>();
        // var clip = Resources.Load<AudioClip>("Audio/Music/" + _musicID);
        // AudioSource _source = GetComponent<AudioSource>();

        // AudioMixerGroup _mixer = AudioManager.instance.musicMixerGroup;
        // _source.outputAudioMixerGroup = _mixer;

        // _source.loop = true;
        // _source.volume = 0.65f;
        // _source.clip = clip;
        // _source.Play();
    }

}
