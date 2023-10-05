using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.ResourceManagement.AsyncOperations;




public class SceneChangeData_DDOL : MonoBehaviour
{

    private bool clearPreviousScene;
    public string previousScene;
    public string currentScene;
    public Dictionary<string, AsyncOperationHandle<SceneInstance>> _handles = new Dictionary<string, AsyncOperationHandle<SceneInstance>>();


    public static SceneChangeData_DDOL instance;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        instance = this;
    }
    private void Start()
    {
        SceneManager.activeSceneChanged += ChangedActiveScene;
    }

    private void ChangedActiveScene(Scene current, Scene next)
    {
        Debug.Log("it worked");
        if (Time.timeScale != 1)
        {
            Time.timeScale = 1;
        }

        if (Time.fixedDeltaTime != 0.02f)
        {
            Time.fixedDeltaTime = 0.02f;
        }
    }

    public void AddScene(string levelName, AsyncOperationHandle<SceneInstance> operationHandle)
    {

        if (_handles.ContainsKey(levelName))
        {
            return;
        }


        _handles.Add(levelName, operationHandle);
        Debug.Log("ADDED SCENE IS " + levelName);


    }

    public void ClearPreviousScene(string levelName)
    {
        if (_handles.ContainsKey(levelName))
        {
            Debug.Log("Trying to clear " + _handles[levelName].Result.Scene.name);
            var ops = Addressables.UnloadSceneAsync(_handles[levelName]);
            ops.Completed += (opsAsync) => { _handles.Remove(levelName); };
        }
        else return;

    }

}