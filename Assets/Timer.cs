using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeValue;

    public TextMeshProUGUI timeText;

    public static Timer current;

    void Start()
    {
        if (current == null)
        {
            current = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update() => UpdateTime();

    public void UpdateTime()
    {
        if (CoreManager.current.scene.scenes[CoreManager.current.scene.currentBuildIndex].gameScene && !PauseManager.isPaused)
        {
            timeText.gameObject.SetActive(true);

            timeValue += Time.deltaTime;

            timeText.text = GetTimerTime();
        }
        else timeText.gameObject.SetActive(false);
    }

    public static string GetTimerTime()
    {
        int minutes = (int) Mathf.Floor(current.timeValue / 60);
        int seconds = Mathf.RoundToInt(current.timeValue % 60);
        int milliseconds = (int) (current.timeValue % 1 * 1000);
        if (milliseconds > 99) milliseconds /= 10;

        string time = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
        time = time.Remove(time.Length - 1, 1);

        int checkTimeValue = ((minutes * 3) + (seconds * 1) + (milliseconds * 3));
        checkTimeValue = (int) (Mathf.Ceil(checkTimeValue / 10) * 10) - checkTimeValue;

        if (checkTimeValue < 0) checkTimeValue *= -1;

        Debug.LogError("GG");

        return time + "<size=10>" + checkTimeValue + "</size>";
    }
}
