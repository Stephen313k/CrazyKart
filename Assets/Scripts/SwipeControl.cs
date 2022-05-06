using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeControl : MonoBehaviour
{
    private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private bool isDraging = false;
    private Vector2 startTouch, swipeDelta;

    public GameObject swiper;

    private void Update()
    {
        //resetting every frame
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;

        if(Input.touches.Length > 0)
        {
            //touch registered
            if(Input.touches[0].phase == TouchPhase.Began)
            {
                isDraging = true;
                tap = true;
                startTouch = Input.touches[0].position;

            }
            //end of touch
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDraging = false;
                Reset();
            }

            //distance
            swipeDelta = Vector2.zero;
            if (isDraging)
            {
                if(Input.touches.Length > 0)
                {
                    swipeDelta = Input.touches[0].position - startTouch;
                }
            }

            //crossing deadzone
            if(swipeDelta.magnitude > 120)
            {
                //direction
                float x = swipeDelta.x;
                float y = swipeDelta.y;
                if(Mathf.Abs(x) > Mathf.Abs(y))
                {
                    //left or right
                    if (x < 0)
                        swipeLeft = true;
                    else
                        swipeRight = true;
                }
                else
                {
                    //up or down
                    if (y < 0)
                        swipeDown = true;
                    else
                        swipeUp = true;
                }

                Reset();
            }
        }

        if (SwipeRight)
            swiper.GetComponent<UIManager>().Play();

        if (SwipeLeft)
            swiper.GetComponent<UIManager>().Exit();

        if (SwipeUp)
            swiper.GetComponent<UIManager>().Resume();

        if(SwipeDown)
            swiper.GetComponent<UIManager>().Pause();


    }

    public void Reset()
    {
        //reset
        startTouch = swipeDelta = Vector2.zero;
        isDraging = false;
    }
    public Vector2 SwipeDelta {get { return swipeDelta; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }


}
