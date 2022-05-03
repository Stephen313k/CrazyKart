using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScene : MonoBehaviour {
    private CanvasGroup fadeGroup;
    private float fadeInDuration = 2;
    private bool gameStarted;

    public void Start()
    {
        //get the canvas group in scene
        fadeGroup = FindObjectOfType<CanvasGroup>();
        //fade to full colour
        fadeGroup.alpha = 1;
    }

    private void Update()
    {   
        //fading in, similar to the sceneloader/splash screen
        if(Time.timeSinceLevelLoad <= fadeInDuration)
        {
            //initial fade-in
            fadeGroup.alpha = 1 - (Time.timeSinceLevelLoad / fadeInDuration); 
        }
        //if fade in is completed, and game isnt started 
        else if(!gameStarted)
        {
            //fade is gone and and game starts
            fadeGroup.alpha = 0;
            gameStarted = true;
        }

    }

    public void CompleteLevel()
    {
        //complete level and sace
        SaveManager.Instance.CompleteLevel(Manager.Instance.currentLevel);

        //go to level selection when returning to menu 
        Manager.Instance.menuFocus = 1; //1 is levels
        ExitScene();

    }

    public void ExitScene()
    {
        SceneManager.LoadScene("Menu");
    }
}
