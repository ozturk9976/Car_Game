using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] RectTransform[] panelsToClose;
    [SerializeField] GameObject[] panelsToHideWhileAdjusting;
    [SerializeField] RectTransform mainCanvas;
    [SerializeField] RectTransform paintCanvas;
    [SerializeField] private RectTransform mainPanel;
    [SerializeField] private RectTransform carPanel;
    [SerializeField] private RectTransform settingsPanel;
    [SerializeField] private RectTransform gameOverPanel;
    [SerializeField] private RectTransform successPanel;
    [SerializeField] private RectTransform gameCompletedPanel;
    [SerializeField] private RectTransform pausePanel;
    [SerializeField]
    private RectTransform adjustModePanel;
    [Space(5)]


    [Header("Buttons")]
    [Space(15)]
    [SerializeField] private RectTransform mainButtons;

    [Header("Scripts")]
    [SerializeField] CarUIHelper[] carUIHelpers;

    public CarUIHelper currentCarUIHelper;

    public string whichControlsActive;

    public bool isHittingButton;
    public bool isAdjustingButtons;


    private int _currentScreenWidht;
    private int _currentScreenHeight;

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnGameOver, GameOver_UI);
        EventManager.AddHandler(GameEvent.OnLevelPass, PassLevel_UI);
        EventManager.AddHandler(GameEvent.OnGameOver, OnGameOver);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnGameOver, GameOver_UI);
        EventManager.RemoveHandler(GameEvent.OnLevelPass, PassLevel_UI);
        EventManager.RemoveHandler(GameEvent.OnGameOver, OnGameOver);
    }

    public static UIManager instance;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {

        _currentScreenHeight = Screen.height;
        _currentScreenWidht = Screen.width;

        whichControlsActive = PlayerPrefs.GetString("WhichControls", "CarUI_1");
        ChangeCurrentCarUIHelperTarget();

    }

    public void ChangeCurrentCarUIHelperTarget()
    {
        for (int i = 0; i < carUIHelpers.Length; i++)
        {
            if (whichControlsActive == carUIHelpers[i].name)
            {
                carUIHelpers[i].gameObject.SetActive(true);
                currentCarUIHelper = carUIHelpers[i];
            }
            else
            {
                carUIHelpers[i].gameObject.SetActive(false);
            }
        }
    }

    public void OpenSettingsPanel()
    {

        EventManager.Broadcast(GameEvent.OnCameraCantMove);
    }

    public void PauseGame_UI()
    {
        pausePanel.gameObject.SetActive(true);
    }

    public void AdjustButtons()
    {
        CineExtension.instance.isTouchingButton++;
        adjustModePanel.gameObject.SetActive(true);

        for (int i = 0; i < panelsToHideWhileAdjusting.Length; i++)
        {
            //panelsToHideWhileAdjusting[i].gameObject.SetActive(false);
            panelsToHideWhileAdjusting[i].GetComponent<UI_PanelAnimator>().DoAnimation();
        }
        // mainPanel.gameObject.SetActive(false);
        Debug.Log(currentCarUIHelper.gameObject.name);
        currentCarUIHelper.AdjustMode();
        Debug.Log(currentCarUIHelper.gameObject.name);
    }

    public void SaveButtons()
    {
        CineExtension.instance.isTouchingButton--;
        for (int i = 0; i < panelsToHideWhileAdjusting.Length; i++)
        {

            panelsToHideWhileAdjusting[i].GetComponent<UI_PanelAnimator>().DoDefaultPos();

        }

        mainPanel.gameObject.SetActive(true);
        currentCarUIHelper.SaveAdjusted();
        adjustModePanel.gameObject.SetActive(false);
    }

    public void DefaultButtonPositions()
    {
        CineExtension.instance.isTouchingButton--;
        for (int i = 0; i < panelsToHideWhileAdjusting.Length; i++)
        {

            panelsToHideWhileAdjusting[i].GetComponent<UI_PanelAnimator>().DoDefaultPos();
        }
        mainPanel.gameObject.SetActive(true);
        currentCarUIHelper.DefaultPosScale();
        adjustModePanel.gameObject.SetActive(false);
    }

    public void PassLevel_UI()
    {
        carPanel.gameObject.SetActive(false);
        mainPanel.gameObject.SetActive(false);
        successPanel.gameObject.SetActive(true);

    }
    public void GameOver_UI()
    {
        StartCoroutine(GameOverIEnum());
    }
    public void GameCompleted()
    {
        successPanel.gameObject.SetActive(true);
        carPanel.gameObject.SetActive(false);
        mainPanel.gameObject.SetActive(false);
        successPanel.GetComponent<SuccessPanelBehaivour>().OnGameCompletedBehaivour();
    }
    public IEnumerator GameOverIEnum()
    {
        yield return new WaitForSecondsRealtime(2);
        gameOverPanel.gameObject.SetActive(true);
    }

    public void OpenPaintCanvas()
    {
        //EventManager.Broadcast(GameEvent.OnBrake);
        CineExtension.instance.ChangeFov(52.5f);
        CineExtension.instance.ChangeTrackedObjectOffset(new Vector3(0, 0.4f, 0));
        paintCanvas.GetComponent<CanvasGroup>().DOFade(1, 2f).SetEase(Ease.OutSine);
    }

    public void ClosePaintCanvas()
    {
        //EventManager.Broadcast(GameEvent.OnBrake_UP);
        CineExtension.instance.ResetFov();
        CineExtension.instance.ResetTrackedObjectOffset();

        paintCanvas.GetComponent<CanvasGroup>().DOFade(0, 0.4f);
        // paintCanvas.DOAnchorPos(new Vector2(_currentScreenWidht * 2, 0), 0.1f).SetEase(Ease.OutSine);
        // mainCanvas.DOAnchorPos(Vector2.zero, 0.1f).SetEase(Ease.OutSine);
    }
    public void BackToMainMenu_Button()
    {
        PlayerPrefs.SetString("SceneToLoad", "MainMenu");
        SceneManager.LoadScene("SceneLoader");
    }

    public void RestartLevel_Button()
    {
        GameManager.instance.RestartLevel();
    }

    public void QuitGame_Button()
    {
        Application.Quit();
    }

    public void HideUI()
    {
        carPanel.gameObject.SetActive(false);
        mainPanel.gameObject.SetActive(false);
    }

    public void OnGameOver()
    {
        foreach (var item in panelsToClose)
        {
            item.gameObject.SetActive(false);
        }
    }
}
