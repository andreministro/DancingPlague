using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuButtons : MonoBehaviour
{
    public GameObject player;

    public void ResumeGame()
    {
        Debug.Log("click");
        player.GetComponent<PlayerInter>().playerInteractionsEnabled = true;
        player.GetComponent<BarsController>().notPause = true;
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
