using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameControlScript : MonoBehaviour
{
    public GameObject pausePanel;
    // Use this for initialization
    void Start()
    {
       
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void pauseGame()
    {
        Time.timeScale = 0.0f;
        pausePanel.gameObject.SetActive(true);
    }

    public void resumeGame()
    {
        Time.timeScale = 1.0f;
        pausePanel.gameObject.SetActive(false);
    }
    public void backToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

	public void restartGame() {
		SceneManager.LoadScene ("MainGame");
	}
}
