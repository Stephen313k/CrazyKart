using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    //menu buttons
    public GameObject buttons;

    public Text scoreText;
    int score;
    bool gameOver;

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

    //for pausing the game
    public void Pause()
    {
        if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
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
