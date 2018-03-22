using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCameraController : MonoBehaviour {
    public GameObject player;
    private Vector3 offset;
    public float rotateSpeed;
    public Transform pivot;
	// Use this for initialization
	void Start () {
        offset = player.transform.position-transform.position;
        pivot.transform.position = player.transform.position;
        pivot.transform.parent = player.transform;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        player.transform.Rotate(0, horizontal, 0);
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        pivot.Rotate(-vertical, 0, 0);
        float desiredYAngle = player.transform.eulerAngles.y;
        float desiredXAngle = pivot.transform.eulerAngles.x;
        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        transform.position = player.transform.position -(rotation* offset);
        transform.LookAt(player.transform);
	}
}
