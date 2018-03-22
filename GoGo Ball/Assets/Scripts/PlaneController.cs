using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider other) {
        if (other.gameObject.CompareTag("GoodItem"))
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("BadItem"))
        {
            Destroy(other.gameObject);
        }
    }
}
