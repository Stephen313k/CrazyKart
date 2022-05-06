using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;


public class CarController : MonoBehaviour {
    public static float speed = 3;

    public float carSpeed = 20; ///
    public float maxPos = 2.5f;

    Vector3 position;

    public UIManager ui;
    public AudioManager am;
    //for myo armband
    public GameObject myo = null;

    //bool for android platform
    bool currentPlatformAndroid = false;

    Rigidbody2D rb;

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
        rb = GetComponent<Rigidbody2D>();

        //if platform is android set to true
        #if UNITY_ANDROID
                currentPlatformAndroid = true;
        #else
                currentPlatformAndroid = false;
        #endif

        //play car sound
        am.carSound.Play();

    }
    void Start () {
        // initialization
        position = transform.position;

        if (currentPlatformAndroid == true)
        {
            Debug.Log("Android");
        }
        else
        {
            Debug.Log("Windows");
        }


	}
	
	// Update is called once per frame
	void Update () {
        if(currentPlatformAndroid == true) //android
        {
            //android movement
            //  TouchMove();
            AccelerometerMove();
        }
        else //windows
        {   //myo-armband movement
            Move();
        }
         

    }

    //collisions hitting enemy car
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            //delete the car, display menu and stop car sound
            gameObject.SetActive(false);
            ui.GameOver();
            am.carSound.Stop();
        }
    }

    //ANDROID ONLY BELOW

/*
    public void TouchMove()
    {
        if(Input.touchCount > 0)
        {
            //first touch is stored
            Touch touch = Input.GetTouch(0);
            //touch position left, car goes left. determine left or right on screen with greater or less than middlepoint
            float middle = Screen.width / 2;

            if(touch.position.x < middle && touch.phase == TouchPhase.Began)
            {
                MoveLeft();
            }

            else if (touch.position.x > middle && touch.phase == TouchPhase.Began)
            {
                MoveRight();
            }          
        }
        else
            {
                SetVelocityZero();
            }
    }
*/
    //android move left button
    public void MoveLeft()
    {
        //carspeed is negatative for left
        rb.velocity = new Vector2(-carSpeed, 0);
    }
    //android move right button
    public void MoveRight()
    {
        rb.velocity = new Vector2(carSpeed, 0);

    }
    //android stops moving
    public void SetVelocityZero()
    {
        rb.velocity = Vector2.zero;
    }

    public void AccelerometerMove()
    {
        //getting x axis
        float x = Input.acceleration.x;
        Debug.Log("x = " + x);
        //check value of x, negative left. positive right
        if(x < -0.1f)
        {
            MoveLeft();
        }else if(x > 0.1f)
        {
            MoveRight();
        }
        else //if phone is not tilted 
        {
            SetVelocityZero(); //dont move car
        }
    }
}
