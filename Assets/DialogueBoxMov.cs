using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBoxMov : MonoBehaviour
{
    public GameObject dialogBox;
    void Update()
    {
        Camera camera = Camera.main;
        float halfHeight = camera.orthographicSize;
        float halfWidth = camera.aspect * halfHeight;
        float horizontalMin = -halfWidth;

        float dialogueBoxcheat = -1.8f; //AJUSTAR DEPOIS 
        dialogBox.transform.position = new Vector2(transform.position.x+horizontalMin/2+dialogueBoxcheat, dialogBox.transform.position.y);
    }
}
