using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayer : MonoBehaviour {
	// Update is called once per frame
    //moves player forward on menu
	private void Update () {
        transform.position += Vector3.forward * 3 * Time.deltaTime;
	}
}
