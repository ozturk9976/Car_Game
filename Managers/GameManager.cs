using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using Unity.Mathematics;
using UnityEngine.ResourceManagement.AsyncOperations;


public class GameManager : MonoBehaviour
{

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void OnAfterSceneLoad()
    {
        PlayerPrefs.DeleteKey("PreviousScene");
    }

    [SerializeField] private string currentThemeName;
    [SerializeField] private int currentThemeIndex;
    [SerializeField] private int currentThemeTreshold;
    [SerializeField] private int currentLevelIndex;
    [Space(15)]
    [SerializeField] GameObject crashImage;
    [SerializeField] GameObject cinemachineCamera;

    CineExtension x_Recenter;
    [HideInInspector] public bool isEditorMode;

    [HideInInspector] public bool isCurrentLevelPlayedBefore;
    public bool isLevelPass;
    public bool isGameOver;
    string carKey;

    public UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<SceneInstance> sceneLoadingOps;
    [SerializeField] List<GameObject> CarList = new List<GameObject>();
    private GlobalData globalData;
    private int _currentUnlockedLevels;
    private Transform playerTransform;
    private Transform StartPosition;

    public static GameManager instance;
    private void Awake()
    {
        instance = this;

        if (Time.timeScale != 1)
        {
            SetDefaultTimeScale();
        }
    }


    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnLevelPass, OnLevelPass);
        EventManager.AddHandler(GameEvent.OnGameOver, OnGameOver);
        EventManager.AddHandler(GameEvent.OnLevelPass, PassLevel);
        EventManager.AddHandler(GameEvent.OnGameOver, WhenGameOver);

        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            isEditorMode = true;
        }


    }
    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnLevelPass, OnLevelPass);
        EventManager.RemoveHandler(GameEvent.OnGameOver, OnGameOver);
        EventManager.RemoveHandler(GameEvent.OnLevelPass, PassLevel);
        EventManager.RemoveHandler(GameEvent.OnGameOver, WhenGameOver);

        if (!isCurrentLevelPlayedBefore)
        {
            IncreasePlayedLevelOfTheme(currentThemeIndex, 1);
        }
        else
        {
            globalData.themes[currentThemeIndex - 1].Levels[currentLevelIndex - 1].IsPlayedBefore = true;
        }

    }


    private void Update()
    {

        if (isEditorMode)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                AudioManager.instance.PlayButton1_Sfx();
                RecenterCamera();
            }
        }
    }



    public void InitializeLevelData()
    {
        globalData = GlobalDataManager.instance.LoadSavedData;
        isCurrentLevelPlayedBefore = globalData.themes[DataManager.Get_ThemeIndex - 1].Levels[DataManager.Get_LevelIndex - 1].IsPlayedBefore;

        currentThemeName = DataManager.Get_ThemeName;
        currentThemeIndex = DataManager.Get_ThemeIndex;
        currentThemeTreshold = DataManager.ThemeLevelTresholds[currentThemeName];
        currentLevelIndex = DataManager.Get_LevelIndex;

        PlayerPrefs.SetString("CurrentThemeName", currentThemeName);
        PlayerPrefs.SetInt("CurrentLevelIndex", currentLevelIndex);
        PlayerPrefs.SetString("LatestPlayedLevel", SceneManager.GetActiveScene().name);

        InstantiateCar(currentThemeIndex - 1);

    }

    private void InstantiateCar(int carIndex)
    {
        StartPosition = GameObject.Find("Car Spawn Position").transform;
        carKey = CarList[carIndex].gameObject.name;
        AsyncOperationHandle<GameObject> tempCarHandle = Addressables.InstantiateAsync(carKey, StartPosition.transform.position, StartPosition.transform.rotation);
        tempCarHandle.Completed += (handle) =>
        {
            InitializeData();
            playerTransform = tempCarHandle.Result.transform;
            x_Recenter = cinemachineCamera.GetComponent<CineExtension>();
            SceneManager.MoveGameObjectToScene(transform.root.gameObject, SceneManager.GetActiveScene());
            x_Recenter.GetComponent<CineFindTarget>().SetTarget(playerTransform);

        };

    }

    private void InitializeData()
    {
        StarManager.instance.InitializeStarData();
        ThemeManager.instance.InitializeThemeData();
        InputManager.API.InitializeInputData();
        GetLevelAndThemeAsText.instance?.InitializeText();
        SettingsAPI.ExternalStartWithSettings();
    }


    public void RestartLevel()
    {
        string currentLevel = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("SceneToLoad", currentLevel);
        SceneManager.LoadScene("SceneLoader");
    }

    public void PassLevel()
    {
        FeedbackManager.instance.Play_levelpassFeedback();
        globalData = GlobalDataManager.instance.LoadSavedData;
        _currentUnlockedLevels = globalData.themes[currentThemeIndex - 1].UnlockedLevelCount;

        if (_currentUnlockedLevels < currentLevelIndex + 1)
        {
            UnlockNext(true);
        }
        else
        {
            LoadNextScene();
        }

        //Checking star count if bigger than treshold unlocks new theme
        if (StarManager.instance.currentTotalStars >= currentThemeTreshold)
        {

            UnlockNext(false);
        }
        else
        {
            LoadNextScene();
        }


    }

    public void UnlockNext(bool isNextLevel)
    {
        //Unlocking next theme or level ?
        switch (isNextLevel)
        {
            // if unlocking level
            case true:
                Debug.Log("UNLOCKİNG NEXT LEVEL");
                globalData.themes[currentThemeIndex - 1].UnlockedLevelCount = _currentUnlockedLevels + 1;
                GlobalDataManager.instance.SaveData(globalData);

                break;
            // if unlocking theme
            case false:
                Debug.Log("UNLOCKİNG NEXT THEME");
                if (currentThemeIndex + 1 > globalData.CurrentPlayableThemeCount)
                {
                    if (currentLevelIndex + 1 > 50)
                    {
                        LastLevelPlayable();
                    }
                }
                else
                {
                    globalData.UnlockedThemeCount = currentThemeIndex + 1;
                    GlobalDataManager.instance.SaveData(globalData);

                    if (currentLevelIndex + 1 > 50)
                    {
                        PlayerPrefs.SetString("SceneToLoad", "Theme" + (currentThemeIndex + 1) + "_" + 1);
                    }
                    else
                    {
                        PlayerPrefs.SetString("SceneToLoad", "Theme" + (currentThemeIndex) + "_" + (currentLevelIndex + 1));
                    }
                }
                break;
            default:
        }
    }
    public void LastLevelPlayable()
    {
        UIManager.instance.GameCompleted();
    }
    public void LoadNextScene()
    {
        string _currentThemeName = PlayerPrefs.GetString("CurrentThemeName");
        int _currentLevelIndex = PlayerPrefs.GetInt("CurrentLevelIndex");
        PlayerPrefs.SetString("SceneToLoad", _currentThemeName + "_" + (_currentLevelIndex + 1));
    }

    public void NextButton()
    {
        // old Addressables.LoadSceneAsync("Assets/Levels/Scenes/SceneLoader.unity");
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
        //sceneLoadingOps = Addressables.LoadSceneAsync("SceneLoader",LoadSceneMode.Additive);
        SceneManager.LoadScene("SceneLoader");
    }

    public void RecenterCamera()
    {
        AudioManager.instance.PlayButton1_Sfx();
        x_Recenter.ReCenter();
    }

    public void WhenGameOver()
    {

        FeedbackManager.instance.Play_gameoverFeedback();
        DOTween.KillAll();
    }

    public void ShowCollisionImage(Vector3 whereToSpawn)
    {
        GameObject _tempImage = Instantiate(crashImage, whereToSpawn, Quaternion.identity);
    }

    public void ChangeCollisionMaterial(Material _otherMaterial)
    {
        //Give other object emission and red color
        _otherMaterial.EnableKeyword("_EMISSION");
        _otherMaterial.SetColor("_EmissionColor", Color.red);
        _otherMaterial.DOColor(new Color32(255, 0, 0, 80), 0.5f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo).SetUpdate(true);
    }

    private IEnumerator SlowMo()
    {
        StartCoroutine(NormalMo());
        while (true)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 0.15f, 2f * Time.fixedDeltaTime);
            Time.fixedDeltaTime = Mathf.Lerp(Time.fixedDeltaTime, 0.002f, 2f * Time.fixedDeltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
    private IEnumerator NormalMo()
    {
        yield return new WaitForSecondsRealtime(5f);

        StopCoroutine(SlowMo());
        while (true)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 1f, 1f * Time.fixedDeltaTime);
            Time.fixedDeltaTime = Mathf.Lerp(Time.fixedDeltaTime, 0.02f, 5f * Time.fixedDeltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    private void SetDefaultTimeScale()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
    }

    public void ChangeTagOfGameobject(GameObject _object, string newTag)
    {
        _object.gameObject.tag = newTag;
    }

    public void IncreasePlayedLevelOfTheme(int currentThemeIndex, int increaseRate)
    {
        int oldData = globalData.themes[currentThemeIndex - 1].PlayedLevelCount;
        int newData = oldData + increaseRate;
        globalData.themes[currentThemeIndex - 1].PlayedLevelCount = newData;
        GlobalDataManager.instance.SaveData(globalData);
    }

    public void OnLevelPass()
    {
        isLevelPass = true;
        // this.enabled = false;
    }
    public void OnGameOver()
    {
        isGameOver = true;
        // this.enabled = false;
    }
}
