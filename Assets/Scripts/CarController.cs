using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;


public class CarController : MonoBehaviour {
    public static float speed = 3;

    public float carSpeed;
    public float maxPos = 2.5f;

    Vector3 position;

    public GameObject myo = null;

    void Move()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");

        GetComponent<Rigidbody2D>().velocity = new Vector2(moveHorizontal, 0) * speed;
        ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo>();
       
            if (thalmicMyo.pose == Pose.WaveOut)
            {
                transform.position += Vector3.right * speed * Time.deltaTime;

            }

            else if (thalmicMyo.pose == Pose.WaveIn)
            {
               //position.x += Input.GetAxis("Horizontal") * carSpeed * Time.deltaTime;

                transform.position += Vector3.left * speed * Time.deltaTime;

             //   ExtendUnlockAndNotifyUserAction(thalmicMyo);
            }
        
    }

    // Use this for initialization
    void Start () {
        position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        Move();
   
        //limit car from going off road
         position.x = Mathf.Clamp(position.x, -maxPos, maxPos);

        //move
       //  transform.position = position;
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
