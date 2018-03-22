using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CobaPlayerPref : MonoBehaviour {
    
    // Use this for initialization
    void Start () {
        PlayerPrefs.SetString("ballType", "plain");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void useBall(string choosenType)
    {
        PlayerPrefs.SetString("ballType", choosenType);
    }

    public void playGame()
    {
        GlobalDataController.Instance.scoreToBonus = 50;
        GlobalDataController.Instance.difficultyLevel = 1;
        GlobalDataController.Instance.lastDistance = 0;
        GlobalDataController.Instance.starScore = 0;
        SceneManager.LoadScene("MainGame");
    }
}
