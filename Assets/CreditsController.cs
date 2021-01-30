using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Interact"))
        {
            Debug.Log("Quit");
            Application.Quit();
        }
        
    }
}
