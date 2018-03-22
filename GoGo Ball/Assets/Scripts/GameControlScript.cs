using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControlScript : MonoBehaviour {
    public GameObject restartButton;
    public GameObject pausePanel;
    public GameObject plainBall;
    public GameObject eyeBall;
    public GameObject atomBall;
    public GameObject woodenBall;
    public GameObject bonusLabel;

	// Use this for initialization
	void Start () {
        string ballType = PlayerPrefs.GetString("ballType");
        if (ballType == "eye")
        {
            eyeBall.SetActive(true);
        }
        else if (ballType == "wooden")
        {
            woodenBall.SetActive(true);
        }
        else if (ballType == "atom")
        {
            atomBall.SetActive(true);
        }
        else
        {
            plainBall.SetActive(true);
        }
    }
	// Update is called once per frame
	void Update () {
        if (Timer.timeLeft<=10)
        {
            bonusLabel.SetActive(false);
        }
		if (Timer.timeLeft<=0)
        {
            SceneManager.LoadScene("MainGame");
        }
	}
    public void pauseGame()
    {
        Time.timeScale = 0.0f;
        pausePanel.gameObject.SetActive(true);
    }

    public void resumeGame()
    {
        Time.timeScale =1.0f;
        pausePanel.gameObject.SetActive(false);
    }
    public void restartScene()
    {
        restartButton.gameObject.SetActive(false);
        Timer.timeLeft = 5;
        SceneManager.LoadScene("SceneCoba");
    }
    public void backToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
