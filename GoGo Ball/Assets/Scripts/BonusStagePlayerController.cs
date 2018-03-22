using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BonusStagePlayerController : MonoBehaviour
{
    public Text countText;
    public Text winText;
    public bool isGrounded;
    public float gravity;
    public float jumpSpeed;
    private float fallSpeed;
    public float moveSpeed;
    public CharacterController characterController;
    public float distToGround;
    public AudioSource goodAudio;
    public AudioSource badAudio;
    private float starPoint;
    public float lastDistance;
    public float starScore;
    public Light directionalLight;

    void Start()
    {
        lastDistance = GlobalDataController.Instance.lastDistance;
        starScore = GlobalDataController.Instance.starScore;

        goodAudio.Stop();
		badAudio.Stop();
        distToGround = GetComponent<Collider>().bounds.extents.y;
        characterController = GetComponent<CharacterController>();
        SetCountText();
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

    private void OnDestroy()
    {
        GlobalDataController.Instance.starScore = starScore;
    }
    private void Update()
    {
        IsGrounded();
        Fall();
        Jump();
        Move();
    }
    void Move()
    {
        float xSpeed = Input.GetAxis("Horizontal");
        if (xSpeed != 0) characterController.Move(new Vector3(xSpeed, 0) * moveSpeed * Time.deltaTime);
        transform.Rotate(new Vector3(xSpeed, 0, 0));
    }
    void IsGrounded()
    {
        isGrounded = (Physics.Raycast(transform.position, -transform.up, distToGround + 0.1f));
    }
    void Jump()
    {
        if (Input.GetButtonUp("Jump") && isGrounded)
        {
            fallSpeed = -jumpSpeed;
        }
    }
    void Fall()
    {
        if (!isGrounded)
        {
            fallSpeed += gravity * Time.deltaTime;
        }
        else
        {
            if (fallSpeed > 0) fallSpeed = 0;
        }
        characterController.Move(new Vector3(0, -fallSpeed * Time.deltaTime));
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GoodItem"))
        {
            if (!goodAudio.isPlaying)
            {
                goodAudio.Play();
            }
            other.gameObject.SetActive(false);
            starScore = starScore + 5;
            SetCountText();
        }
        if (other.gameObject.CompareTag("BadItem"))
        {
            if (!badAudio.isPlaying)
            {
                badAudio.Play();
            }
            directionalLight.intensity=0.2f;
            other.gameObject.SetActive(false);
            Timer.timeLeft-= 2;
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = (lastDistance + starScore).ToString();
    }
}
