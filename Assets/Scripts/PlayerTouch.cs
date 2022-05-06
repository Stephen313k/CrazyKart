using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTouch : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private Vector2 currentPosition;
    private Vector2 endTouchPosition;
    private bool stopTouch = false;

    public float swipeRange;
    public float tapRange;

    public Text outputText;
    public GameObject cardObject;
    // Update is called once per frame
    void Update()
    {
        Swipe();
    }

    public void Swipe()
    {
        //getting the first touch
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }
        //seeing how far touch has travelled
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            currentPosition = Input.GetTouch(0).position;
            Vector2 Distance = currentPosition - startTouchPosition;

            //when player stops touching screen
            if(Distance.x < -swipeRange)
            {
                //left
                stopTouch = true;
            }
            else if(Distance.x > swipeRange)
            {
                outputText.text = "r"; 

                //right
                stopTouch = true;
            }
            else if (Distance.y > swipeRange)
            {
                cardObject.GetComponent<UIManager>().Play();                    //up
                stopTouch = true;
            }
            else if (Distance.y < -swipeRange)
            {
                cardObject.GetComponent<UIManager>().Pause();                    

                //down
                stopTouch = true;
            }
        
    }
        //end phase of touching
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            stopTouch = false;

            endTouchPosition = Input.GetTouch(0).position;
            Vector2 Distance = endTouchPosition - startTouchPosition;

            if(Mathf.Abs(Distance.x) < tapRange && Mathf.Abs(Distance.y) < tapRange)
            {
                outputText.text = "rSSSSSSS";

            }
        }
    }



}
