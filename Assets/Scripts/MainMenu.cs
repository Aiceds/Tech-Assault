using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Volume volume;

    public Slider audioSlider;

    public GameObject blurBox;
    public GameObject bloomBox;

    // Start is called before the first frame update
    void Start()
    {
        StateController.bloomEnabled = true;
        StateController.blurEnabled = true;
        StateController.audioVolumeValue = 0.5f;

        Time.timeScale = 1f;

        audioSlider.value = StateController.audioVolumeValue;
        SoundManager.soundManager.ChangeMasterVolume(StateController.audioVolumeValue);
        audioSlider.onValueChanged.AddListener(val => SoundManager.soundManager.ChangeMasterVolume(val));
    }

    // Update is called once per frame
    void Update()
    {
        #region Bloom Updates
        Bloom bloom;
        if (volume.profile.TryGet<Bloom>(out bloom))
        {
            bloom.intensity.value = StateController.bloomIntensity;
        }

        if (StateController.bloomEnabled == true)
        {
            StateController.bloomIntensity = 1.2f;
            bloomBox.SetActive(true);
        }
        else if (StateController.bloomEnabled == false)
        {
            StateController.bloomIntensity = 0f;
            bloomBox.SetActive(false);
        }
        #endregion

        #region Blur Updates
        MotionBlur motionBlur;
        if (volume.profile.TryGet<MotionBlur>(out motionBlur))
        {
            motionBlur.intensity.value = StateController.blurIntensity;
        }

        if (StateController.blurEnabled == true)
        {
            StateController.blurIntensity = 1.2f;
            blurBox.SetActive(true);
        }
        else if (StateController.blurEnabled == false)
        {
            StateController.blurIntensity = 0f;
            blurBox.SetActive(false);
        }
        #endregion

        StateController.audioVolumeValue = audioSlider.value;
    }

    public void PlayMedium()
    {
        SceneManager.LoadScene("Main Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BloomOnOff()
    {
        if (StateController.bloomEnabled == true)
        {
            StateController.bloomEnabled = false;
        }
        else
        {
            StateController.bloomEnabled = true;
        }
    }

    public void MotionBlurOnOff()
    {
        if (StateController.blurEnabled == true)
        {
            StateController.blurEnabled = false;
        }
        else
        {
            StateController.blurEnabled = true;
        }
    }
}
