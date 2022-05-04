using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackMove : MonoBehaviour {

    public float speed;
    Vector2 offset;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //track will move with the speed, infinite wrap
        offset = new Vector2(0, Time.time * speed);
        //get the renderer, track moves downwards
        GetComponent<Renderer>().material.mainTextureOffset = offset;
	}
}
