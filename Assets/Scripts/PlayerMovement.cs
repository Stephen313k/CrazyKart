using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private CharacterController controller;

    private float baseSpeed = 10.0f;
    private float rotSpeedX = 3.0f;
    private float rotSpeedY = 1.5f;
}

/*

    // Move object using accelerometer
    float speed = 10.0f;
    private void Start()
    {
        controller = GetComponent<CharacterController>();

    }
    private void Update()
    {

        Vector3 dir = Vector3.zero;

        // we assume that device is held parallel to the ground
        // and Home button is in the right hand

        // remap device acceleration axis to game coordinates:
        //  1) XY plane of the device is mapped onto XZ plane
        //  2) rotated 90 degrees around Y axis
        dir.x = -Input.acceleration.y;
        dir.z = Input.acceleration.x;

        // clamp acceleration vector to unit sphere
        if (dir.sqrMagnitude > 1)
            dir.Normalize();

        Vector3 yaw = dir.x * transform.right * rotSpeedX * Time.deltaTime;
        Vector3 pitch = dir.y * transform.up * rotSpeedY * Time.deltaTime;
        Vector3 directon = yaw + pitch;


        // Make it move 10 meters per second instead of 10 meters per frame...
        dir *= Time.deltaTime;

        // Move object
        transform.Translate(dir * speed);
    }
    

        //player goes forward
        //Vector3 moveVector = transform.forward * baseSpeed;


        //get players input
       // Vector3 inputs = Manager.Instance.GetPlayerInput(); ////////////////////////

        //delta direction


        //limit player turning completely
       // float maxX = Quaternion.LookRotation(moveVector + dir).eulerAngles.x;
        //if its within the limit add the direction
       // if(maxX < 90 && maxX > 70 || maxX > 270 && maxX < 290)
       // {
            //too far in movement
       // }
       // else
       // {
            // add direction to current move 
         //   moveVector += dir;
            //player face where they are going
           // transform.rotation = Quaternion.LookRotation(moveVector);
       // }
        //move player
       // controller.Move(direction * Time.deltaTime);

   
    }
}
*/