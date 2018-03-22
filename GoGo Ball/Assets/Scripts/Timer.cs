using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static int time;
    public static int timeLeft;
    public Text countdownText;
    // Use this for initialization
    void Start()
    {
        string ballType = PlayerPrefs.GetString("ballType");
        if (ballType == "eye")
        {
            time = 22;
        }
        else if (ballType == "atom")
        {
            time = 17;
        }
        else
        {
            time = 12;
        }
        timeLeft = time;
        StartCoroutine("LoseTime");
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft <= time - 2)
        {
            countdownText.text = timeLeft.ToString();
        }
        if (timeLeft <= 0)
        {
            StopCoroutine("LoseTime");
            countdownText.text = "Time's Up!";
        }
    }
    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;

        }
    }
}
