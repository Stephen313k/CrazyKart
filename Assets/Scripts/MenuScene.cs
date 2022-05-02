using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScene : MonoBehaviour {

    private CanvasGroup fadeGroup;
    private float fadeInSpeed = 0.33f;


    public Transform colorPanel;
    public Transform trailPanel;

    private void Start()
    {
        //grab the canvas group
        fadeGroup = FindObjectOfType<CanvasGroup>();
        //coloured screen
        fadeGroup.alpha = 1;

        //button events
        InitShop();
    }

    private void Update()
    {
        //fade in
        fadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeInSpeed;
    }

    private void InitShop()
    {
        //for developer reference error
        if (colorPanel == null || trailPanel == null)
            Debug.Log("Assign trail and colours");
        //for all children under colour pannel, find button and onclick
        int i = 0;
        foreach (Transform t in colorPanel)
        {
            int currentIndex = i;
            //get button
            Button b = t.GetComponent<Button>();
            //use index
            b.onClick.AddListener(() => OnColourSelect(currentIndex));
            i++;
        }
        //reset index and do the same for trail button
        i = 0;
    
        foreach (Transform t in trailPanel)
        {
            int currentIndex = i;
            //get button
            Button b = t.GetComponent<Button>();
            //use index
            b.onClick.AddListener(() => OnTrailSelect(currentIndex));
            i++;
        }
    }

    //BUTTONS
    //selecting colour
    private void OnColourSelect(int currentIndex)
    {
        Debug.Log("Selecting colour button");
    }
    //selecting trail
    private void OnTrailSelect(int currentIndex)
    {
        Debug.Log("Selecting trail button");

    }
  
    public void OnPlayClick()
    {
        Debug.Log("Play button");
    }

    public void OnOtherClick()
    {
        Debug.Log("Other button");
    }

    public void OnColourBuySet()
    {
        Debug.Log("colour buyset");
    }

    public void OnTrailBuySet()
    {
        Debug.Log("trail buyset");
    }
}



