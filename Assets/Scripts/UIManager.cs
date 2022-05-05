using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;


public class UIManager : MonoBehaviour {

    //menu buttons
    public GameObject buttons;
    public bool GameIsPaused = false;
    public Text scoreText;

    int score;
    bool gameOver;

    public GameObject pauseMenuUI;

    //for myo armband
    public GameObject myo = null;

    // Use this for initialization
    void Start () {

        buttons.SetActive(false);
        gameOver = false;
        score = 0;
        //updating score slowly 
        InvokeRepeating("ScoreUpdate", 1.0f,0.5f);
	}
	
	// Update is called once per frame
	void Update () {
        scoreText.text = "Score: " + score;

        ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo>();

        if (thalmicMyo.pose == Pose.DoubleTap)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void ScoreUpdate()
    {
        //if the game is in play
        if (!gameOver)
        {
            //incrememnt score by 1
            score += 1;
        }
        
    }
    public void GameOver()
    {
        gameOver = true;
        //display menu buttons
         buttons.SetActive(true);
        
    }

    public void Play()
    {
        Application.LoadLevel("Level1");
    }


    //Method which unpauses the game
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    //Method which pauses the game
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Replay()
    {
        //load current level again
        Application.LoadLevel(Application.loadedLevel);
    }

    public void Menu()
    {
        //load menu
        Application.LoadLevel("Menu");
    }

    public void Exit()
    {
        //quit application
        Application.Quit();
    }
}
