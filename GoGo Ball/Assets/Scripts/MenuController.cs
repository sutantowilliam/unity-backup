using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {
    public void Start()
    {
        PlayerPrefs.SetString("ballType", "plain");
    }
    public void LoadScene(string sceneName) {
        string ballType = PlayerPrefs.GetString("ballType", "none");
        if (ballType=="none")
        {
            PlayerPrefs.SetString("ballType", "plain");
        }
        GlobalDataController.Instance.scoreToBonus = 50;
        GlobalDataController.Instance.difficultyLevel = 1;
        GlobalDataController.Instance.lastDistance = 0;
        GlobalDataController.Instance.starScore = 0;
        SceneManager.LoadScene (sceneName);
	}

	public void ExitGame() {
		Application.Quit();
	}
		
}
