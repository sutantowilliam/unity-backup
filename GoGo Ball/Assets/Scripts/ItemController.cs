using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour {
    private Rigidbody rb;
    private Vector3 gravity;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        gravity = new Vector3(0, -2, 0);

    }
	
	// Update is called once per frame
	void Update () {
        rb.velocity = gravity;
        if (Timer.timeLeft <= 0)
        {
            Destroy(gameObject);
        }
    }
}
