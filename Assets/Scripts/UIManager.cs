using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

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

        //PLAY PAUSE AND EXIT APPLICATION REPLACED WITH VOICE RECOG AS MYO ARMBAND IS SENSITIVE AND DONT WANT TO ACCIDENTALLY START OR QUIT APPLICATION MID-GAME

      /*  ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo>();
        //double tap pauses or resumes the game, same as esc key
        if (thalmicMyo.pose == Pose.DoubleTap || Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
                ExtendUnlockAndNotifyUserAction(thalmicMyo);
            }
            else
            {
                Pause();
                ExtendUnlockAndNotifyUserAction(thalmicMyo);
            }
        }
        
        if(thalmicMyo.pose == Pose.FingersSpread)
        {
            Play();
        }*/
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
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Replay()
    {
        //load current level again
        SceneManager.LoadScene(1);
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

    void ExtendUnlockAndNotifyUserAction(ThalmicMyo myo)
    {
        ThalmicHub hub = ThalmicHub.instance;

        if (hub.lockingPolicy == LockingPolicy.Standard)
        {
            myo.Unlock(UnlockType.Timed);
        }

        myo.NotifyUserAction();
    }
}
