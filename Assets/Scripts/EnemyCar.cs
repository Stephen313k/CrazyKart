﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCar : MonoBehaviour {


    public float speed = 5f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //move vertical, Y direction
        transform.Translate(new Vector3(0,1,0) * speed * Time.deltaTime);
	}
}
