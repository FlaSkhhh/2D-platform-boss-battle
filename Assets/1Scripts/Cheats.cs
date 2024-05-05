using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    public GameObject MMenu;
    public GameObject Help;
    public GameObject Video;
    string[] Cynthia;
    bool cheat=false;
    int count=0;

    void Start()
    {
        Cynthia = new string[] { "c", "y", "n", "t", "h", "i", "a" } ;
        count = 0;
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(Cynthia[count]))
            {
                count++;
            }
            else
            {
                count = 0;
            }
        }
        if (count == Cynthia.Length)
        {
            CheatReward();
        }

        if (cheat)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Fire1"))
            {
                cheat = false;
                MMenu.SetActive(true);
                Video.SetActive(false);
            }
        }
    }

    void CheatReward()
    {
        count = 0;
        MMenu.SetActive(false);
        Help.SetActive(false);
        Video.SetActive(true);
        cheat = true;
    }
}
