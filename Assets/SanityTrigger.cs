using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityTrigger : MonoBehaviour
{
    public GameObject player;

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("saiu");
        player.GetComponent<BarsController>().triggerArea = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.GetComponent<BarsController>().triggerArea = true;
    }
}
