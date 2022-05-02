using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Preloader : MonoBehaviour {

    private CanvasGroup fadeGroup;
    private float loadTime;
    private float mininumLogoTime = 3.0f;

    private void Start()
    {
        //grab canvas in scene       
        fadeGroup = FindObjectOfType<CanvasGroup>();
        //white screen
        fadeGroup.alpha = 1;
        //preload game


        //buffer the loading so logo appears
        if (Time.time < mininumLogoTime)
            loadTime = mininumLogoTime;
        else
            loadTime = Time.time;
    }

    private void Update()
    {   //fade in
        if(Time.time < mininumLogoTime)
        {
            fadeGroup.alpha = 1 - Time.time;
        }
        //fade out
        if (Time.time > mininumLogoTime && loadTime !=0 )
        {
            fadeGroup.alpha = Time.time - mininumLogoTime;
            if(fadeGroup.alpha >= 1)
            {
                SceneManager.LoadScene("Menu");
            }
        }

       
    }
}
