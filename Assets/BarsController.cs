using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BarsController : MonoBehaviour
{
    public TextMeshProUGUI pressETxt;

    public GameObject madnessImage;
    public GameObject armacao;
    public GameObject[] hungerBar;
    public GameObject[] hungerFaces;
    public int health;
    public bool isCoveringEars;
    public bool triggerArea = false;
    public bool playerEating = false;
    public bool first;

    private float maxScale = 14.0f;
    private float maxScaleAux = 7.0f;
    private float minScale = 2.0f;
    private float alphaLevel;

    private float maxHealth=28;
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "LVL1 - Home")
        {
            first = true;
            health = 25;
            for (int i = 0; i <= maxHealth; i++)
            {
                hungerBar[i].SetActive(false);
            }
            for (int i = 0; i <= 2; i++)
            {
                hungerFaces[i].SetActive(false);
            }
            hungerBar[health].SetActive(true);
            hungerFaces[0].SetActive(true);
        }
        else if (SceneManager.GetActiveScene().name != "LVL1 - Home")
        {
            alphaLevel = 0.5f;
            madnessImage.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaLevel);
            first = PlayerPrefs.GetInt("firstSanity") == 1 ? true : false;

            health = PlayerPrefs.GetInt("Hungerbar", gameObject.GetComponent<BarsController>().health);
            /*hungerBar[health] = GameObject.Find("/Canvas/HungerBar/vida");
            for (int i=health; i >= 0; i--)
            {
                hungerBar[i] = GameObject.Find("/Canvas/HungerBar/vida-" + i);
            }*/
            for (int i = 0; i <= maxHealth; i++)
            {
                hungerBar[i].SetActive(false);
            }
            for (int i = 0; i <= 2; i++)
            {
                hungerFaces[i].SetActive(false);
            }
            
            hungerBar[health].SetActive(true);
            if (health < 10)
                hungerFaces[2].SetActive(true);
            else if (health < 20)
                hungerFaces[1].SetActive(true);
            else
                hungerFaces[0].SetActive(true);
            
            StartCoroutine(hungerBarLoosing());
        }
    }

    void Update()
    {
        /*if (SceneManager.GetActiveScene().name == "LVL1 - Home") {
            if (gameObject.GetComponent<PlayerScript>().cutScene==false){
                {
                    hungerBar[health].SetActive(true);
                    hungerFaces[0].SetActive(true);
                    armacao.SetActive(true);
                }
            }
            else
            {
                armacao.SetActive(false);
                hungerBar[health].SetActive(false);
                hungerFaces[0].SetActive(false);
            }
        }
        else
        {*/
        if (playerEating)
        {
            hungerBarGaining();
        }
        sanityBarController();
        
    }
    private IEnumerator hungerBarLoosing()
    {
        yield return new WaitForSeconds(30.0f);
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
            StartCoroutine(hungerBarLoosing());
        }
        else
        {
            //Dead

        }
        
    }
    public void hungerBarGaining()
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
        float alphaStep = 0.004f;
        float delayPlayerStep = 0.001f;

        if (isCoveringEars)
        {
            if (pressETxt.text != "")
                pressETxt.text = "";

            gameObject.GetComponent<PlayerScript>().sanityPenalty =0.0f;
            if (madnessImage.transform.localScale.x - step < maxScaleAux)
            {
                Vector2 localScale = new Vector2(madnessImage.transform.localScale.x + step, madnessImage.transform.localScale.y + step);
                madnessImage.transform.localScale = localScale;
            }
            else if (madnessImage.transform.localScale.x - step >= maxScaleAux && alphaLevel > 0.0f && madnessImage.activeSelf)
            {
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
            if (first)
            {
                pressETxt.text = "Cover ears (Press R)";
                first = false;
            }
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
        else if (!triggerArea)
        {
            gameObject.GetComponent<PlayerScript>().sanityPenalty = 0.0f;
            if (madnessImage.transform.localScale.x - step < maxScaleAux)
            {
                Vector2 localScale = new Vector2(madnessImage.transform.localScale.x + step, madnessImage.transform.localScale.y + step);
                madnessImage.transform.localScale = localScale;
            }
            else if (madnessImage.transform.localScale.x - step >= maxScaleAux && alphaLevel > 0.0f && madnessImage.activeSelf)
            {
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
    }
}
