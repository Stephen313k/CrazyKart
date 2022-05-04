using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour {

    public GameObject[] cars;
    int carNo;

    public float maxPos = 2.2f;
    //enemy respawn time
    public float delayTimer = 1f;
    float timer; 

    // Use this for initialization
    void Start () {
        timer = delayTimer;
	}
	
	// Update is called once per frame
	void Update () {

        timer -= Time.deltaTime;

        if(timer <= 0)
        {
		    //keep spawns inside road
            Vector3 carPos = new Vector3(Random.Range(-maxPos, maxPos),transform.position.y, transform.position.z);
            //random car types
            carNo = Random.Range(0, 3);
            //where enemy spawns
            Instantiate(cars[carNo], carPos, transform.rotation);
            //timer resets
            timer = delayTimer;
        }

	}
}
