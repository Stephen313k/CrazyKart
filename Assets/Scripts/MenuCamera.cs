using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour {
    private Vector3 startPosition;
    private Quaternion startRotation;

   // private Vector3 desiredPosition;
    private Quaternion desiredRotation;

    public Transform shopWaypoint;
    public Transform levelWaypoint;




    public Transform lookAt;

    private Vector3 desiredPosition;
    private float offset = 0.015f;
    private float distance = 0.05f;
    private void Start()
    {
        //start is equal to desired and transform 
        startPosition = desiredPosition = transform.localPosition;
        startRotation = desiredRotation = transform.localRotation;
    }

    private void Update()
    {
        //update position
        desiredPosition = lookAt.position + (-transform.forward * distance) + (transform.up * offset);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, 0.01f);
        //update rotation
        transform.LookAt(lookAt.position + (Vector3.up * offset));
        // transform.Translate(Input.acceleration.x, 0, 0);

        //   float x = Manager.Instance.GetPlayerInput().x;

        //for smoothing and changing position
        //   transform.localPosition = Vector3.Lerp(transform.localPosition, desiredPosition + new Vector3 (0,x,0) * 0.01f, 0.01f);
        //    transform.localRotation = Quaternion.Lerp(transform.localRotation, desiredRotation, 1f);

    }

    //go back to menu
    public void BackToMainMenu()
    {
        desiredPosition = startPosition;
        desiredRotation = startRotation;
    }

    //go to the shop menu
    public void MoveToShop()
    {
        desiredPosition = shopWaypoint.localPosition;
        desiredRotation = shopWaypoint.localRotation;
    }

    //go to the shop menu
    public void MoveToLevel()
    {
        desiredPosition = levelWaypoint.localPosition;
        desiredRotation = levelWaypoint.localRotation;
    }


}
