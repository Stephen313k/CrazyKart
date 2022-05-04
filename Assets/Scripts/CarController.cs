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

    public UIManager ui;
    public AudioManager am;
    //for myo armband
    public GameObject myo = null;

    //myo armband
    void Move()
    {
        //horizontal axis for car
        float moveHorizontal = Input.GetAxis("Horizontal");

        //second peremeter is 0 for vector2, only changing horizontal
        GetComponent<Rigidbody2D>().velocity = new Vector2(moveHorizontal, 0) * speed;
        ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo>();

        if (thalmicMyo.pose == Pose.WaveOut)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        else if (thalmicMyo.pose == Pose.WaveIn)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;

        }
    }

    private void Awake()
    {
        //play car sound
        am.carSound.Play();

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

    }

    //collisions hitting enemy car
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            //delete the car, display menu and stop car sound
            Destroy(gameObject);
            ui.GameOver();
            am.carSound.Stop();
        }
    }
}
