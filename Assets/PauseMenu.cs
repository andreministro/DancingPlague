using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject player;
    public Button Resume;
    public Button Options;
    public Button Quit;

    public GameObject pauseMenu;
    void Start()
    {
        pauseMenu.SetActive(false);
        Button btn = Resume.GetComponent<Button>();
        btn.onClick.AddListener(Continue);

        /*btn = Quit.GetComponent<Button>();
        btn.onClick.AddListener(Exit);*/
    }

    // Update is called once per frame
    private bool paused = false;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //Resume.gameObject.SetActive(true); 
            if (paused)
            {
                player.GetComponent<BarsController>().notPause = true;
                pauseMenu.SetActive(false);
                player.GetComponent<PlayerInter>().playerInteractionsEnabled = true;
                Time.timeScale = 1;
            }
            else
            {
                player.GetComponent<BarsController>().notPause = false;
                pauseMenu.SetActive(true);
                player.GetComponent<PlayerInter>().playerInteractionsEnabled = false;
                Time.timeScale = 0;
            }
            paused = !paused;
        }
    }
    private void Continue()
    {
        player.GetComponent<PlayerInter>().playerInteractionsEnabled = true;
        player.GetComponent<BarsController>().notPause = true;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    /*
    private void Exit()
    {

    }*/
}

