using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarsController : MonoBehaviour
{
    private float sanityBar;
    public bool loosingSanity;
    void Start()
    {
        sanityBar = 1.0f;
        loosingSanity = false;
    }

    void Update()
    {
        if (loosingSanity)
        {
            sanityBarController(true);
        }
    }

    private void sanityBarController(bool loosing)
    {
        //OnTriggerStay
        if(loosing)
            sanityBar -= 0.1f;
        else
            sanityBar -= 0.1f;
    }
}
