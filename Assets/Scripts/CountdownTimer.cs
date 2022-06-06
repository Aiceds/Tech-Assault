using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public GameManager gameManager;

    public float timeValue = 120f;
    public Text timeText;

    // Update is called once per frame
    void Update()
    {
        if(timeValue > 0)
        {
            timeValue -= Time.deltaTime;
        }
        else
        {
            timeValue = 0f;
        }

        if (timeValue < 0)
        {
            gameManager.LoseGame();
        }

        DisplayTime(timeValue);
    }

    void DisplayTime(float timeToDisplay)
    {
        if(timeToDisplay < 0f)
        {
            timeToDisplay = 0f;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliseconds = timeToDisplay % 1 * 1000;

        timeText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}
