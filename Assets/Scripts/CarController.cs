using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

    public Rigidbody theRB;

   public float forwardAccel =8f, reverseAcces =4f, maxSpeed=50f, turnStrength=180f;
	// Use this for initialization
	void Start () {
        //theRB.transform.parent = null;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = theRB.transform.position;
	}

    private void FixedUpdate()
    {
       theRB.AddForce(transform.forward * forwardAccel);
    }
}
