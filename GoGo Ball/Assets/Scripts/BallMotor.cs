using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMotor : MonoBehaviour {
	private CharacterController controller;

	private float speed = 5.0f;
	private Vector3 moveVector;
	private float verticalVelocity = 0.0f;
	private float gravity = 12.0f;

	private float jumpSpeed = 8.0f;

	private float starPoint = 5.0f;

	private float animationDuration = 2.0f;

	private bool isDead = false;

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController> ();
        string ballType = PlayerPrefs.GetString("ballType");
        if (ballType == "eye")
        {
            starPoint = 7;
        }
        else if (ballType == "wooden")
        {
            starPoint = 6;
        }
        else
        {
            starPoint = 5;
        }
    }
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(0, 0, -30) * Time.deltaTime * 5);

		if (isDead) {
			return;
		}

		if (Time.time < animationDuration) {
			controller.Move (Vector3.forward * speed * Time.deltaTime);
			return;
		}

		moveVector = Vector3.zero;

		if (Input.GetButtonUp ("Jump") && controller.isGrounded) {
			verticalVelocity = jumpSpeed;
		} else if (controller.isGrounded) {
			verticalVelocity = 0.0f;
		} else {
			verticalVelocity -= gravity * Time.deltaTime;
		}
        float xSpeed = Input.GetAxis("Horizontal");
        moveVector.x = xSpeed*speed;
		moveVector.y = verticalVelocity;
		moveVector.z = speed;

		controller.Move (moveVector * Time.deltaTime);
	}

	public void SetSpeed(float modifier) {
		speed += 0.25f * modifier;
	}

	private void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("Star")) {
			other.gameObject.SetActive (false);
			GetComponent<Score> ().AddStarScore (starPoint);
		} else if (other.gameObject.CompareTag ("Obstacle")) {
			Death ();
		}
	}

	private void Death(){
		isDead = true;
		GetComponent<Score> ().OnDeath ();
		Time.timeScale = 0.0f;
	}
}
