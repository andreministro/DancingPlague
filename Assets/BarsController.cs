using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class BarsController : MonoBehaviour
{
    public TextMeshProUGUI pressETxt;
    public GameObject GameOver;
    public Button yes;
    public Button no;
    public GameObject madnessImage;
    public GameObject madnessImageFill;
    public GameObject madnessPlayer;
    public GameObject armacao;
    public GameObject[] hungerBar;
    public GameObject[] hungerFaces;
    public GameObject demon, demonImage;
    public int health;
    public bool isCoveringEars;
    public bool triggerArea = false;
    public bool playerEating = false;

    private float maxScale = 12.0f;
    private float maxScaleAux = 7.0f;
    private float minScale = 1.8f;
    private float alphaLevel;

    private float maxHealth=28;
    public static bool isdead=false;

    private bool lostHunger = false;
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Credits") { }
        else if (SceneManager.GetActiveScene().name == "LVL1 - Home")
        {
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

            health = PlayerPrefs.GetInt("Hungerbar", gameObject.GetComponent<BarsController>().health);

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

            Button btn = yes.GetComponent<Button>();
            btn.onClick.AddListener(ContinueGame);
            btn = no.GetComponent<Button>();
            btn.onClick.AddListener(StopGame);
            GameOver.SetActive(false);
            madnessImageFill.SetActive(false);
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Credits")
        { 
            if (playerEating)
            {
                hungerBarGaining();
            }
            sanityBarController();
        }
    }
    public bool notPause = true;
    private IEnumerator hungerBarLoosing()
    {
        //30.0f
        yield return new WaitForSeconds(15.0f);
        yield return new WaitUntil(() => notPause);
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
            //Animação cair no chão
            gameObject.GetComponent<PlayerScript>().animator.SetTrigger("IsDyingT");
            madnessPlayer.SetActive(false);
            pressETxt.text = "";
            madnessImageFill.SetActive(true);
            madnessImageFill.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.4f);
            GameOver.SetActive(true);
            gameObject.GetComponent<PlayerInter>().playerInteractionsEnabled = false;
            gameObject.GetComponent<PlayerScript>().movePlayer = false;
            lostHunger = true;
            gameObject.GetComponent<PlayerInter>().inventory.SetActive(false);
            if (SceneManager.GetActiveScene().name == "LVL2 - Forest") demonImage.SetActive(false);
        }
        
    }
    public void hungerBarGaining()
    {
        playerEating = false;
        hungerBar[health].SetActive(false);
        if (health + 15 <= 28)
            health += 15;
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

    private float alphaLevelFill = 0.0f;
    private void sanityBarController()
    {
        float step = 0.04f;
        float alphaStep = 0.004f;
        float alphaFillStep = 0.01f;
        float delayPlayerStep = 0.001f;
        float jumpMax = 2.7f;
        if (notPause)
        {
            if (isCoveringEars)
            {
                if (pressETxt.text != "")
                    pressETxt.text = "";

                gameObject.GetComponent<PlayerScript>().sanityPenalty = 0.0f;
                if (madnessImage.transform.localScale.x - step < maxScaleAux)
                {
                    if (madnessImage.transform.localScale.x - step < jumpMax) gameObject.GetComponent<PlayerScript>().canJump = false;
                    else gameObject.GetComponent<PlayerScript>().canJump = true;
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
            else if (triggerArea)
            {
                madnessImage.SetActive(true);
                if (gameObject.GetComponent<PlayerScript>().sanityPenalty < 0.5f)
                    gameObject.GetComponent<PlayerScript>().sanityPenalty += delayPlayerStep;
                if (madnessImage.transform.localScale.x - step > minScale)
                {
                    if (madnessImage.transform.localScale.x - step < jumpMax) gameObject.GetComponent<PlayerScript>().canJump = false;
                    else gameObject.GetComponent<PlayerScript>().canJump = true;
                    Vector2 localScale = new Vector2(madnessImage.transform.localScale.x - step, madnessImage.transform.localScale.y - step);
                    madnessImage.transform.localScale = localScale;
                }
                else if ((madnessImage.transform.localScale.x - step <= minScale) && alphaLevel < 0.9f)
                {
                    if (madnessImage.transform.localScale.x - step < jumpMax) gameObject.GetComponent<PlayerScript>().canJump = false;
                    else gameObject.GetComponent<PlayerScript>().canJump = true;
                    alphaLevel += alphaStep;
                    madnessImage.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaLevel);
                }
                else
                {
                    pressETxt.text = "";
                    madnessPlayer.SetActive(true);
                    madnessImageFill.SetActive(true);
                    gameObject.GetComponent<PlayerInter>().playerInteractionsEnabled = false;
                    gameObject.GetComponent<PlayerScript>().movePlayer = false;
                    gameObject.GetComponent<PlayerInter>().inventory.SetActive(false);
                    if (alphaLevelFill < 1.0f)
                    {
                        alphaLevelFill += alphaFillStep;
                        madnessImageFill.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaLevelFill);
                    }
                    else
                    {
                        GameOver.SetActive(true);
                    }
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
    private void ContinueGame()
    {
        if (gameObject.GetComponent<PlayerData>().getHealth() == -1)
        {
            StopGame();
        }
        else
        {
            if (lostHunger)
            {
                //IDLE ANIMATION AGAIN
                gameObject.GetComponent<PlayerScript>().animator.SetBool("IsAlive", true);
                health = 28;
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
                StartCoroutine(hungerBarLoosing());
            }
            else
            {
                alphaLevel = 0.5f;
                madnessImage.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaLevel);
                Vector2 localScale = new Vector2(maxScale, maxScale);
                madnessImage.transform.localScale = localScale;
                madnessImage.SetActive(false);
            }
            alphaLevelFill = 0.0f;
            madnessImageFill.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaLevelFill);
            madnessImageFill.SetActive(false);
            gameObject.GetComponent<PlayerInter>().playerInteractionsEnabled = true;
            gameObject.GetComponent<PlayerScript>().movePlayer = true;
            GameOver.SetActive(false);
            gameObject.GetComponent<PlayerInter>().inventory.SetActive(false);

            LoadSavedScene();
            lostHunger = false;
        }
    }
    public void StopGame()
    {
        gameObject.GetComponent<PlayerInter>().clearStaticVariables();
        gameObject.GetComponent<PlayerInter>().inventory.GetComponent<InventoryDisplay>().clearStaticVariables();
        gameObject.GetComponent<PlayerData>().clearStaticVariables();
        gameObject.GetComponent<PlayerScript>().clearStaticVariables();
        isdead = false;
        SceneManager.LoadScene("MainMenu");
    }

    private void LoadSavedScene()
    {
        gameObject.GetComponent<PlayerScript>().SetMonster();
        isdead = true;
        if (lostHunger)
        {
            health = 28;
        }
        else
        {
            health = gameObject.GetComponent<PlayerData>().getHealth();
        }
        gameObject.GetComponent<PlayerInter>().inventory.GetComponent<InventoryDisplay>().setItemList(gameObject.GetComponent<PlayerData>().getInventoryItems());
        SceneManager.LoadScene(gameObject.GetComponent<PlayerData>().getScene());
    }
    public void morteMonstro()
    {
        demonImage.SetActive(true);
        gameObject.GetComponent<PlayerInter>().inventory.SetActive(false);
        demon.GetComponent<Animator>().SetTrigger("IsKilling");
        madnessImageFill.SetActive(true);
        madnessImageFill.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
        GameOver.SetActive(true);
        gameObject.GetComponent<PlayerInter>().playerInteractionsEnabled = false;
        gameObject.GetComponent<PlayerScript>().movePlayer = false;
    }

    public void notDead()
    {
        isdead = false;
    }
    public bool isDead()
    {
        return isdead;
    }
}
