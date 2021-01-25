﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int health;
    public float[] position;
    public int scene;
    public GameObject bar;

    public PlayerData (PlayerData player)
    {
       health = bar.GetComponent<BarsController>().health;
       position = new float[3];
       position[0] = player.transform.position.x;
       position[1] = player.transform.position.y;
       position[2] = player.transform.position.z;
         
    }
}
