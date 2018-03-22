using System.Collections;
using System.Collections.Generic;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Score : MonoBehaviour {
	private float score = 0.0f;
    private float lastDistance;
    private float starScore;
	public EnterName enterName;
	public GameObject enterNamePanel;

	private int difficultyLevel = 1;
	private int scoreToNextLevel;
	private int scoreToBonus = 50;

	private bool isDead = false;

	public Text scoreText;
	public Text distanceText;

    private void Start()
    {
        scoreToBonus = GlobalDataController.Instance.scoreToBonus;
        difficultyLevel = GlobalDataController.Instance.difficultyLevel;
        lastDistance = GlobalDataController.Instance.lastDistance;
        starScore = GlobalDataController.Instance.starScore;
        Debug.Log("scoreToBonus:"+ scoreToBonus);
        Debug.Log("difficultyLevel:" + difficultyLevel);
        Debug.Log("lastDistance:" + lastDistance);
        Debug.Log("starScore:" + starScore);

    }
    // Update is called once per frame
    void Update () {
		if (isDead) {
			return;
		}

		if (score >= scoreToBonus){
			ToBonusGame ();
            GlobalDataController.Instance.scoreToBonus = scoreToBonus;
            GlobalDataController.Instance.difficultyLevel = difficultyLevel;
            lastDistance = lastDistance + transform.position.z;
            GlobalDataController.Instance.lastDistance = lastDistance;
            GlobalDataController.Instance.starScore = starScore;
            
            SceneManager.LoadScene ("BonusGame");
		}
		if (score >= scoreToNextLevel) {
			LevelUp ();
		}

		distanceText.text = ((int)transform.position.z+lastDistance).ToString ();
        score = lastDistance + transform.position.z + starScore;
        Debug.Log(score.ToString());
		scoreText.text = ((int)score).ToString();
	}
    private IEnumerator Waiting()
    {
        yield return new WaitForSeconds(3);
    }
    void ToBonusGame(){
		scoreToBonus += scoreToBonus + 100;
	}

	void LevelUp() {
		scoreToNextLevel = 50 * difficultyLevel;
		difficultyLevel++;

		GetComponent<BallMotor>().SetSpeed (difficultyLevel);
	}

	public void OnDeath(){
		isDead = true;
		enterNamePanel.SetActive (true);
		enterName.ToogleEnterName (score, lastDistance + transform.position.z);
	}

	public void AddStarScore(float addition){
		starScore += addition;
	}
}
