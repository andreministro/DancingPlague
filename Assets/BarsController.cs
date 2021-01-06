using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarsController : MonoBehaviour
{
    public GameObject madnessImage;
    public GameObject[] hungerBar;
    public GameObject[] hungerFaces;
    private int health;
    public bool isCoveringEars;
    public bool triggerArea = false;
    public bool playerEating = false;

    private float maxScale = 14.0f;
    private float maxScaleAux = 7.0f;
    private float minScale = 2.0f;
    private float alphaLevel;
    void Start()
    {
        alphaLevel = 0.5f;
        madnessImage.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaLevel);
        //madnessImage.SetActive(true);

        health = 28;
        for(int i=0; i <= health; i++)
        {
            hungerBar[i].SetActive(false);
        }
        for (int i = 0; i <= 2; i++)
        {
            hungerFaces[i].SetActive(false);
        }
        hungerBar[health].SetActive(true);
        hungerFaces[0].SetActive(true);
        StartCoroutine(hungerBarLoosing());
    }

    void Update()
    {
        triggerArea = true;
        if (playerEating)
        {
            hungerBarGaining();
        }
        //sanityBarController();
    }
    private IEnumerator hungerBarLoosing()
    {
        yield return new WaitForSeconds(1.0f); //signal
        hungerBar[health].SetActive(false);
        if (health > 0) { 
            health--;
            hungerBar[health].SetActive(true);
            if (health < 10)
            {
                hungerFaces[1].SetActive(false);
                hungerFaces[2].SetActive(true);
            }
            else if(health<20 && health >= 10)
            {
                hungerFaces[0].SetActive(false);
                hungerFaces[1].SetActive(true);
            }
        }
        StartCoroutine(hungerBarLoosing());
    }
    private void hungerBarGaining()
    {
        playerEating = false;
        hungerBar[health].SetActive(false);
        if (health + 10 <= 28)
            health += 10;
        else
            health = 28;
        hungerBar[health].SetActive(true);
        if (health < 20)
        {
            hungerFaces[2].SetActive(false);
            hungerFaces[1].SetActive(true);
        }
        else
        {
            hungerFaces[1].SetActive(false);
            hungerFaces[0].SetActive(true);
        }
    }

    private void sanityBarController()
    {
        float step = 0.04f;
        float alphaStep = 0.005f;
        float delayPlayerStep = 0.002f;

        if (isCoveringEars && triggerArea)
        {
            gameObject.GetComponent<PlayerScript>().sanityPenalty =0.0f;
            if (madnessImage.transform.localScale.x - step < maxScaleAux)
            {
                Vector2 localScale = new Vector2(madnessImage.transform.localScale.x + step, madnessImage.transform.localScale.y + step);
                madnessImage.transform.localScale = localScale;
            }
            else if (madnessImage.transform.localScale.x - step >= maxScaleAux && alphaLevel > 0.0f && madnessImage.activeSelf)
            {
                //Debug.Log("aqui");
                alphaLevel -= step;
                madnessImage.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaLevel);
            }
            else
            {
                alphaLevel = 0.5f;
                madnessImage.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaLevel);
                Vector2 localScale = new Vector2(maxScale, maxScale);
                madnessImage.transform.localScale = localScale;
                madnessImage.SetActive(false);
            }
        }
        else if(triggerArea)
        {
            madnessImage.SetActive(true);
            if(gameObject.GetComponent<PlayerScript>().sanityPenalty<0.5f)
                gameObject.GetComponent<PlayerScript>().sanityPenalty += delayPlayerStep;
            if (madnessImage.transform.localScale.x - step > minScale)
            {
                Vector2 localScale = new Vector2(madnessImage.transform.localScale.x - step, madnessImage.transform.localScale.y - step);
                madnessImage.transform.localScale = localScale;
            }
            else if ((madnessImage.transform.localScale.x - step <= minScale) && alphaLevel<0.8f)
            {
                alphaLevel += alphaStep;
                madnessImage.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaLevel);
            }
        }
    }
}
