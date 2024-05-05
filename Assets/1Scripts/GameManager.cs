using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Reflection;

public class GameManager : MonoBehaviour
{
    public GameObject completeLevelUI;
    public GameObject failedLevelUI;
    public GameObject pauseUI;
    public GameObject player;
    public AudioManager audioM;
    public bool gamePaused = false;
    bool gameEnded = false;

    public void Awake()
    {
        gamePaused = false;
        player.GetComponentInChildren<PlayerMovement>().enabled = true;
        Time.timeScale = 1;
    }

    public void Start()
    {
        audioM.Play("MainTheme");
    }

    public void GameOver()
    {
        if (gameEnded == false)
        {
            audioM.Play("Death");
            failedLevelUI.SetActive(true);
            //sound.Play("DarkDied");
            Invoke("Restart", 6f);
            gameEnded = true;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void Victory()
    {
        audioM.Play("BossDeath");
        completeLevelUI.SetActive(true);
        Invoke("MainMenu", 2f);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Resume()
    {
        gamePaused = false;
        player.GetComponentInChildren<PlayerMovement>().enabled = true;
        player.GetComponentInChildren<Animator>().SetBool("Blocking", false); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gamePaused)
        {
            gamePaused = true;
            Time.timeScale = 0;
            pauseUI.SetActive(true);
            player.GetComponentInChildren<PlayerMovement>().enabled = false;
        }
    }
}
