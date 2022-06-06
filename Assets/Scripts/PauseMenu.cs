using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;

    public Animator anim;

    public Slider audioSlider;

    public Volume volume;

    public GameObject blurBox;
    public GameObject bloomBox;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        audioSlider.value = StateController.audioVolumeValue;
        SoundManager.soundManager.ChangeMasterVolume(StateController.audioVolumeValue);
        audioSlider.onValueChanged.AddListener(val => SoundManager.soundManager.ChangeMasterVolume(val));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

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
            StateController.blurIntensity = 0.5f;
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

    public void Resume()
    {
        anim.SetTrigger("PauseSlideOut");
        Time.timeScale = 1f;
        gameIsPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Pause()
    {
        anim.SetTrigger("PauseSlideIn");
        Time.timeScale = 0f;
        gameIsPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
