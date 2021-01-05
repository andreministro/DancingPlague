using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarsController : MonoBehaviour
{
    private float sanityBar;
    public bool loosingSanity;
    public GameObject madnessImage;

    public bool isCoveringEars;
    public bool triggerArea = false;

    private float maxScale = 14.0f;
    private float maxScaleAux = 7.0f;
    private float minScale = 2.0f;
    private float alphaLevel;
    void Start()
    {
        alphaLevel = 0.5f;
        madnessImage.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaLevel);
        //madnessImage.SetActive(true);
    }

    void Update()
    {
        triggerArea = true;
        
        //sanityBarController();
    }
    private void hungerBarController()
    {
        
    }
    private void sanityBarController()
    {
        float step = 0.04f;
        float alphaStep = 0.005f;

        if (isCoveringEars && triggerArea)
        {
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
            if (madnessImage.transform.localScale.x - step > minScale)
            {
                Vector2 localScale = new Vector2(madnessImage.transform.localScale.x - step, madnessImage.transform.localScale.y - step);
                madnessImage.transform.localScale = localScale;
            }
            else if ((madnessImage.transform.localScale.x - step <= minScale) && alphaLevel<1.0f)
            {
                alphaLevel += alphaStep;
                madnessImage.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaLevel);
            }
        }
    }
}
