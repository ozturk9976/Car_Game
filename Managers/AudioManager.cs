using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public List<AudioSource> themeMusics = new List<AudioSource>();
    [SerializeField] private AudioMixerGroup musicMixerGroup;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioSource mainMenuMusic;

    [SerializeField] private AudioSource button1_sfx;
    [SerializeField] private AudioSource button2_sfx;
    [SerializeField] private AudioSource gear_sfx;
    [SerializeField] private AudioSource confetti_sfx;
    [SerializeField] private AudioSource rain_sfx;
    [SerializeField] private AudioSource spray_sfx;


    [SerializeField] private AudioSource metalCollision;
    [SerializeField] private AudioSource concreteCollision;
    [SerializeField] private AudioSource glassCollision;


    public static AudioManager instance;
    private void Awake()
    {
        instance = this;
    }

    public void MuteSFX()
    {
        mixer.SetFloat("SFX", -80);
    }
    public void UnmuteSFX()
    {
        mixer.SetFloat("SFX", 0);
    }
    public void MuteMusic()
    {
        mixer.SetFloat("Music", -80);
    }
    public void UnmuteMusic()
    {
        mixer.SetFloat("Music", 0);
    }
    public void PlayMainMenuMusic()
    {
        mainMenuMusic.Play();
    }
    public void PlayTheme1Music()
    {
        themeMusics[0].Play();
    }
    public void PlayButton1_Sfx()
    {
        button1_sfx.Play();
    }
    public void PlayButton2_Sfx()
    {
        button2_sfx.Play();
    }
    public void PlayConfetti_Sfx()
    {

    }
    public void PlayGear_Sfx()
    {
        gear_sfx.Play();
    }
    public void PlaySpray_SFX()
    {
        if (spray_sfx != null)
        {
            if (!spray_sfx.isPlaying)
            {
                spray_sfx.Play();
            }
        }
    }


    #region Collision SFX
    public void PlayMetalCollisionSFX()
    {
        metalCollision.Play();
    }

    public void PlayConcreteCollisionSFX()
    {
        concreteCollision.Play();
    }

    public void PlayGlassCollisionSFX()
    {
        glassCollision.Play();
    }
    #endregion
}
