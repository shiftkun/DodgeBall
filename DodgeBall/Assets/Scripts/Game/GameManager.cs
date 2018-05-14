﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public MyPlayer p1;
    public MyPlayer p2;

    public GameObject healthScript;
    public GameObject pauseScript;
    public GameObject gameoverScript;

    public GameObject gameoverScreen;

    private BgMusicController bgMusicController;

    private bool gameIsPaused;
    private bool gameIsOver;

    private bool musicTensedUp;

    // Use this for initialization
    void Start () {
        Time.timeScale = 1f;
        gameIsOver = false;
        gameIsPaused = false;
        musicTensedUp = false;
        bgMusicController = GameObject.Find("BGMusic").GetComponent<BgMusicController>();
        bgMusicController.PlayGameMusic();
	}

    // Update is called once per frame
    void Update() {

        //Check Pause
        CheckForPause();

        //Check health
        CheckHealth();
        
	}

    private void CheckForPause()
    {
        //Fixes thing where user can still bring up pause screen on gameover screen
        if (!gameIsOver)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!gameIsPaused)
                {
                    pauseScript.GetComponent<PauseManager>().PauseGame();
                }
                else
                {
                    pauseScript.GetComponent<PauseManager>().ResumeGame();
                }
            }
        }
    }

    private void CheckHealth()
    {
        //Updates the heart ui 
        healthScript.gameObject.GetComponent<HealthManager>().UpdateHealthUI(p1.health, p2.health);

        if(!musicTensedUp && (p1.health == 1 || p2.health == 1))
        {
            musicTensedUp = true;
            bgMusicController.PlayTenseMusic();
        }
        else if (p1.health == 0 || p2.health == 0)
        {
            //If health is 0, game ends
            gameIsOver = true;
            int winner = (p2.health == 0) ? 1 : 2;
            EndGame(winner);
        }
    }

    private void EndGame(int winner)
    {
        //end game
        Time.timeScale = 0f;
        gameoverScreen.SetActive(true);

        //Send winning info
        gameoverScript.gameObject.GetComponent<GameOverManager>().ShowGameOverInfo(winner);
    }
}