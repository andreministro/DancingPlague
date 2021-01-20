using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip door_open, barril, dirt_step;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        //dirt_step = Resources.Load<AudioClip>("dirt_step");
        door_open = Resources.Load<AudioClip>("door_open");
        barril = Resources.Load<AudioClip>("barril");
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "door":
                audioSrc.PlayOneShot(door_open);
                break;

            case "barril":
                audioSrc.PlayOneShot(barril);
                break;

            /*case "dirt_step":
                audioSrc.loop = true;
                audioSrc.clip = dirt_step;
                audioSrc.Play();
                break;*/
        }

    }
}
