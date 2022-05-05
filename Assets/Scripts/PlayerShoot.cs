using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

public class PlayerShoot : MonoBehaviour {

    //for myo armband
    public GameObject myo = null;

    public Transform shootFrom;
    public GameObject bullet;

    public float shootSpeed, shootTimer;
    public int maxAmmo;

    public int currentAmmo;
    private bool isShooting;

    public Text ammoText;

    //bool for android platform
    bool currentPlatformAndroid = false;

    // Use this for initialization
    void Start () {
        currentAmmo = maxAmmo;
        isShooting = false;
	}

    private void Awake()
    {
        //if platform is android set to true
        #if UNITY_ANDROID
                        currentPlatformAndroid = true;
        #else
                currentPlatformAndroid = false;
        #endif

    }
    // Update is called once per frame
    void Update () {

        if (currentPlatformAndroid == true) //android
        {
            //android shoot
            TouchShoot();
        }
        else //windows
        {
            ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo>();

            if (Input.GetButton("Fire1") && !isShooting && currentAmmo > 0 || thalmicMyo.pose == Pose.Fist && !isShooting && currentAmmo > 0)
            {
                StartCoroutine(Shoot());
                UpdateAmmo(-1);//minus one ammo after shoot
            }
        }
    }

    //for shooting delay
    IEnumerator Shoot()
    {
        isShooting = true;
        Debug.Log("Shot");
        GameObject newBullet = Instantiate(bullet, shootFrom.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2( 0f,shootSpeed * Time.fixedDeltaTime);

        yield return new WaitForSeconds(shootTimer);
        isShooting = false;

    }

    public void UpdateAmmo(int ammo)
    {
        currentAmmo += ammo;
        if(currentAmmo > maxAmmo)
        {
            currentAmmo = maxAmmo;
        }
        ammoText.text = "Ammo: " + currentAmmo;

    }

    //android
    public void TouchShoot()
    {
        if (Input.touchCount > 0)
        {
            //first touch is stored
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                StartCoroutine(Shoot());
                UpdateAmmo(-1);//minus one ammo after shoot
            }
        }
    }
}
