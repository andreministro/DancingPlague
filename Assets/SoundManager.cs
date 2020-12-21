using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip blockHitsound, blockBreaksound, paddleHitsound, blockPointsound;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        blockHitsound = Resources.Load<AudioClip> ("BLOCKKNOCK");
        blockBreaksound = Resources.Load<AudioClip>("BLOCKSMASH");
        paddleHitsound = Resources.Load<AudioClip>("PADDLEKNOCK");
        blockPointsound = Resources.Load<AudioClip>("POINTBLOCK");

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
                audioSrc.PlayOneShot(blockHitsound);
                break;

            case "blockbreak":
                audioSrc.PlayOneShot(blockBreaksound);
                break;

            case "paddlehit":
                audioSrc.PlayOneShot(paddleHitsound);
                break;

            case "pointhit":
                audioSrc.PlayOneShot(blockPointsound);
                break;

        }

    }
}
