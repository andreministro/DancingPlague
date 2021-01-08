using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip dirt_footstep;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        dirt_footstep = Resources.Load<AudioClip> ("footstep_dirt");

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
            case "blockhit":
                audioSrc.PlayOneShot(dirt_footstep);
                break;

        }

    }
}
