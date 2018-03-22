using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSelection : MonoBehaviour {
    public GameObject eyeBall;
    public GameObject woodenBall;
    public GameObject atomBall;
    public GameObject plainBall;
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
		
	}
}
