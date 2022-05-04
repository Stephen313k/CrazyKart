using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {


    public float carSpeed;
    public float maxPos = 2.5f;

    Vector3 position;

	// Use this for initialization
	void Start () {
        position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        //key press
        position.x += Input.GetAxis("Horizontal") * carSpeed * Time.deltaTime;

        //limit car from going off road
        position.x = Mathf.Clamp(position.x, -maxPos, maxPos);

        //move
        transform.position = position;
	}

    //collisions hitting enemy car
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
