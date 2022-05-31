using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Volume volume;
    private bool bloomEnabled;
    private bool mBlurEnabled;

    public GameObject blurBox;
    public GameObject bloomBox;

    // Start is called before the first frame update
    void Start()
    {
        bloomEnabled = true;
        mBlurEnabled = true;

        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
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
        Bloom bloom;

        if (volume.profile.TryGet<Bloom>(out bloom) && bloomEnabled)
        {
            bloom.intensity.value = 0f;
            bloomEnabled = false;
            bloomBox.SetActive(false);
        }
        else
        {
            bloom.intensity.value = 1.2f;
            bloomEnabled = true;
            bloomBox.SetActive(true);
        }
    }

    public void MotionBlurOnOff()
    {
        MotionBlur motionBlur;

        if (volume.profile.TryGet<MotionBlur>(out motionBlur) && mBlurEnabled)
        {
            motionBlur.intensity.value = 0f;
            mBlurEnabled = false;
            blurBox.SetActive(false);
        }
        else
        {
            motionBlur.intensity.value = 0.5f;
            mBlurEnabled = true;
            blurBox.SetActive(true);
        }
    }
}
