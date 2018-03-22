using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopPlayerController : MonoBehaviour
{
    public bool isGrounded;
    public float gravity;
    public float jumpSpeed;
    private float fallSpeed;
    public float moveSpeed;
    public CharacterController characterController;
    private Vector3 moveDirection;
    public GameObject woodenCanvas;
    public GameObject atomCanvas;
    public GameObject plainCanvas;
    public GameObject eyeBallCanvas;
	public Camera mainCamera;
	public Camera panelCamera;
	public GameObject shopControllerScript;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void OnDestroy()
    {
        //GlobalDataController.Instance.star = star;
    }
    private void Update()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, 0, Input.GetAxis("Vertical") * moveSpeed);
        //distToGround = GetComponent<Collider>().bounds.extents.y;
        if (Input.GetButtonUp("Jump") && characterController.isGrounded)
        {
            fallSpeed = -jumpSpeed;
        } else if (!characterController.isGrounded)
        {
            fallSpeed += gravity * Time.deltaTime;
        } else
        {
            if (fallSpeed > 0) fallSpeed = 0;
        }
        moveDirection.y = -fallSpeed;
		moveDirection = mainCamera.transform.TransformDirection(moveDirection);
        characterController.Move(moveDirection * Time.deltaTime);
    }
  
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EyeBallBox"))
        {
			Time.timeScale =0.0f;
			mainCamera.gameObject.SetActive (false);
			eyeBallCanvas.gameObject.SetActive(true);
			panelCamera.gameObject.SetActive (true);
			shopControllerScript.GetComponent<ShopController> ().LoadPanel (2);
        }
        else if (other.gameObject.CompareTag("WoodenBallBox"))
        {
			Time.timeScale =0.0f;
			mainCamera.gameObject.SetActive (false);
			woodenCanvas.gameObject.SetActive(true);
			panelCamera.gameObject.SetActive (true);
			shopControllerScript.GetComponent<ShopController> ().LoadPanel (3);
        }
        else if (other.gameObject.CompareTag("AtomBallBox"))
        {
			Time.timeScale =0.0f;
			mainCamera.gameObject.SetActive (false);
			atomCanvas.gameObject.SetActive(true);
			panelCamera.gameObject.SetActive (true);
			shopControllerScript.GetComponent<ShopController> ().LoadPanel (1);
        }
        else if (other.gameObject.CompareTag("PlainBallBox"))
        {
			Time.timeScale =0.0f;
			mainCamera.gameObject.SetActive (false);
			plainCanvas.gameObject.SetActive(true);
			panelCamera.gameObject.SetActive (true);
			shopControllerScript.GetComponent<ShopController> ().LoadPanel (0);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("EyeBallBox"))
        {
            eyeBallCanvas.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("WoodenBallBox"))
        {
            woodenCanvas.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("AtomBallBox"))
        {
            atomCanvas.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("PlainBallBox"))
        {
            plainCanvas.gameObject.SetActive(false);
        }
    }
}

