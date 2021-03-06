using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Core;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI timeCheck;

    void Start()
    {
        timeText.text = Timer.GetTimerTime();
    }

    public void Restart()
    {
        FindObjectOfType<Timer>().timeValue = 0;
        SceneManager.LoadScene(1);
    }

    public void Menu()
    {
        FindObjectOfType<Timer>().timeValue = 0;
        SceneManager.LoadScene(0);
    }
}
