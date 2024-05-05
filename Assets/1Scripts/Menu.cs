using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject gameManager;
    private GameObject MMenu;
    private GameObject Help;

    void Start()
    {
        MMenu = transform.GetChild(0).gameObject;
        Help = transform.GetChild(1).gameObject;
    }

    public void Play() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Resume()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        gameManager.GetComponent<GameManager>().Resume();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        SceneManager.LoadScene(1);
    }

    public void Info()
    {
        MMenu.SetActive(false);
        Help.SetActive(true);
    }

    public void Back()
    {
        MMenu.SetActive(true);
        Help.SetActive(false);
    }
}
