using UnityEngine;
using System.Collections;

public class Accelerometer : MonoBehaviour
{



    // gravity constant
    public float g = 9.8f;
    public float speed = 2;

    private void Update()
    {
        // normalize axis
        Physics.gravity = new Vector3(
            Input.acceleration.x,
            Input.acceleration.z,
           speed
        ) * g;
    }

}