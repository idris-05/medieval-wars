using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Data.Common;
// using Unity.VisualScripting;
using UnityEngine.Audio;
using Unity.Mathematics;
using UnityEngine.Accessibility;

public class AudioManager : MonoBehaviour
{
    public GameObject OFFButton;
    public GameObject ONButton;
    public AudioSource clickSFX;

    private bool isMuted = false;

    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioMixer audioMixer;

    void Start()
    {
        //Load User's last Music Volume change:
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            Save(0.35f);
        }
        Load();
    }

    //Change MusicVolume:
     public void SetMusicVolume()
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volumeSlider.value) * 20);
        Save(volumeSlider.value);
    }

    public void Reset()
    {
        ONButton.SetActive(true);
        OFFButton.SetActive(false);
        SetunMuted();
        Save(0.35f);
        Load();
    }

    public void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    //Save the Player's volume change.
    public void Save(float volumeValue)
    {
        PlayerPrefs.SetFloat("musicVolume", volumeValue);
    }

    /*----------------------------------------------------------------*/

    public void SetMuted()
    {
        isMuted = true;
    }

    public void SetunMuted()
    {
        isMuted = false;
    }

    public void Playclick()
    {
        if (isMuted == false)
        {
            clickSFX.Play();
        }
    }


}
