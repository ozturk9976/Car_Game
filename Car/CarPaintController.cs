
using UnityEngine;
using DG.Tweening;

public class CarPaintController : MonoBehaviour
{
    [Header("Default Colors")]
    [SerializeField] private Color32 _defaultCarColor;
    [SerializeField] private Color32 _defaultRimColor;
    [SerializeField] Material currentCarMaterial;
    [SerializeField] Material currentRimMaterial;


    private int CarMat_R;
    private int CarMat_G;
    private int CarMat_B;

    private int RimMat_R;
    private int RimMat_G;
    private int RimMat_B;



    public static CarPaintController instance;
    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        CarMat_R = PlayerPrefs.GetInt(currentCarMaterial.name + "R", _defaultCarColor.r);
        CarMat_G = PlayerPrefs.GetInt(currentCarMaterial.name + "G", _defaultCarColor.g);
        CarMat_B = PlayerPrefs.GetInt(currentCarMaterial.name + "B", _defaultCarColor.b);

        RimMat_R = PlayerPrefs.GetInt(currentRimMaterial.name + "R", _defaultRimColor.r);
        RimMat_G = PlayerPrefs.GetInt(currentRimMaterial.name + "G", _defaultRimColor.g);
        RimMat_B = PlayerPrefs.GetInt(currentRimMaterial.name + "B", _defaultRimColor.b);
    }

    private void Start()
    {
        Color32 newCar_Color = new Color32((byte)CarMat_R, (byte)CarMat_G, (byte)CarMat_B, 255);
        Color32 newRim_Color = new Color32((byte)RimMat_R, (byte)RimMat_G, (byte)RimMat_B, 255);
        currentCarMaterial.SetColor("_BaseColor", newCar_Color);
        currentRimMaterial.SetColor("_BaseColor", newRim_Color);

    }
    public void PaintCar(Color32 color)
    {

        FeedbackManager.instance?.Play_paintFeedback();
        AudioManager.instance?.PlaySpray_SFX();
        currentCarMaterial.SetColor("_BaseColor", color);
        PlayerPrefs.SetInt(currentCarMaterial.name + "R", color.r);
        PlayerPrefs.SetInt(currentCarMaterial.name + "G", color.g);
        PlayerPrefs.SetInt(currentCarMaterial.name + "B", color.b);
    }
    public void PaintRim(Color32 color)
    {
        FeedbackManager.instance?.Play_paintFeedback();
        AudioManager.instance?.PlaySpray_SFX();
        currentRimMaterial.SetColor("_BaseColor", color);
        PlayerPrefs.SetInt(currentRimMaterial.name + "R", color.r);
        PlayerPrefs.SetInt(currentRimMaterial.name + "G", color.g);
        PlayerPrefs.SetInt(currentRimMaterial.name + "B", color.b);
    }
}
