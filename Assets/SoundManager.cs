using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip door_open, barril, dirt_step, poco, tochastart, tochaContinue, gameover;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        door_open = Resources.Load<AudioClip>("door_open");
        barril = Resources.Load<AudioClip>("barril");
        poco = Resources.Load<AudioClip>("baldepoco");
        tochastart = Resources.Load<AudioClip>("tochastart");
        tochaContinue = Resources.Load<AudioClip>("tochaContinue");
        gameover = Resources.Load<AudioClip>("gameover");
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "door":
                audioSrc.PlayOneShot(door_open);
                break;

            case "barril":
                audioSrc.PlayOneShot(barril);
                break;

            case "poco":
                audioSrc.PlayOneShot(poco);
                break;

            case "tochastart":
                audioSrc.PlayOneShot(tochastart);
                break;

            case "tochaContinue":
                audioSrc.PlayOneShot(tochaContinue);
                break;

            case "gameover":
                audioSrc.PlayOneShot(gameover);
                break;
        }

    }
}