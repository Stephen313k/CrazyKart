using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCollectable : MonoBehaviour {

    public int ammoToAdd;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject colGo = collision.gameObject;
        if(collision.gameObject.tag == "Player")
        {
            colGo.GetComponent<PlayerShoot>().UpdateAmmo(ammoToAdd);
            Destroy(gameObject);
        }
    }
}
