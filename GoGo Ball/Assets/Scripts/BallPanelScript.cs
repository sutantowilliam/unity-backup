using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallPanelScript : MonoBehaviour {

	public GameObject ballName;
	public GameObject desc;
	public GameObject price;
	public GameObject ballButton;

	public void SetBallPanel(string ballname, string desc, int price, int stat) {
		this.ballName.GetComponent<Text> ().text = ballname;
		this.desc.GetComponent<Text> ().text = desc;
		this.price.GetComponent<Text> ().text = price.ToString();
		if (stat == 0) {
			this.ballButton.GetComponent<Button> ().GetComponentInChildren<Text> ().text = "BUY";
			this.ballButton.GetComponent<Button> ().interactable = true;
		} else if (stat == 1) {
			this.ballButton.GetComponent<Button> ().GetComponentInChildren<Text> ().text = "USE";
			this.ballButton.GetComponent<Button> ().interactable = true;
		}
		else {
			this.ballButton.GetComponent<Button> ().GetComponentInChildren<Text>().text = "IN USE";
			this.ballButton.GetComponent<Button> ().GetComponentInChildren<Text>().color = new Color(0.0f, 0.0f, 0.0f, 60.0f);
			this.ballButton.GetComponent<Button> ().interactable = false;
		}
	}
}
