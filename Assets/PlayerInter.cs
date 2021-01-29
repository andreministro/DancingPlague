using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInter : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI pressETxt;
    public GameObject DialogBox;

    public GameObject rock, barrel, roda, wood, oleo, corda, barrilAberto;
    public GameObject rockEText, herbsEText, rodaEText, woodEText, oleoEText, cordaEText, paoEText, facaEText;

    public GameObject bauAberto;
    public GameObject inventory;
    public GameObject fogueira;
    public GameObject fogueiraTrigger;
    public GameObject pedregulho, pedregulhoTrigger;
    public GameObject lightcandle;
    public GameObject faca;

    public bool firstPick=true;
    private static bool firstInterPoco = true;

    private string triggered = "";
    private int completedMissions;
    private bool offTrigger = false;

    public bool playerInteractionsEnabled;


    private static bool firstSanity = true;
    //private static int sceneMarket;
    void Start()
    {
        DialogBox.SetActive(false);
        if (SceneManager.GetActiveScene().name == "LVL1 - Home" && gameObject.GetComponent<BarsController>().isDead() == true)
            completedMissions = 3;

        if (SceneManager.GetActiveScene().name== "LVL1 - Home" && gameObject.GetComponent<BarsController>().isDead()==false)
        {
            completedMissions = 0;
            StartCoroutine(secondCutscene());
            bauAberto.SetActive(false);
            inventory.SetActive(false);
            playerInteractionsEnabled = false;
        }
        else
        {
            playerInteractionsEnabled = true;
            inventory.SetActive(false);
        }
        LoadScene();
    }

    private bool firstVisitA = true, firstVisitG = true, firstVisitS = true;
    void Update()
    {
        if (playerInteractionsEnabled)
        {
            if (SceneManager.GetActiveScene().name != "LVL1 - Home")
            {
                if (Input.GetButtonDown("Inventory"))
                {
                    displayInventory();
                }
            }

            if (triggered != "")
            {
                if (Input.GetButtonDown("Interact"))
                {
                    if (triggered == "Armario")
                    {
                        pressETxt.text = "";
                        string newText = "Empty.";
                        StartCoroutine(displayDialogueText(newText, false, false));
                        if (firstVisitA)
                        {
                            completedMissions++;
                            firstVisitA = false;
                        }
                    }

                    if (triggered == "Bau")
                    {
                        bauAberto.SetActive(true);
                        pressETxt.text = "";
                        string newText = "Empty.";
                        StartCoroutine(displayDialogueText(newText, false, false));
                        if (firstVisitG)
                        {
                            completedMissions++;
                            firstVisitG = false;
                        }
                    }

                    if (triggered == "Saco")
                    {
                        pressETxt.text = "";
                        string newText = "Empty.";
                        StartCoroutine(displayDialogueText(newText, false, false));
                        if (firstVisitS)
                        {
                            completedMissions++;
                            firstVisitS = false;
                        }
                    }

                    if (triggered == "Porta")
                    {
                        pressETxt.text = "";
                        if (completedMissions == 3)
                        {
                            SoundManager.PlaySound("door");
                            saveDataThroughScenes();
                            SceneManager.LoadScene("LVL1 - Village");
                        }
                    }
                    if (triggered == "PortaEndGame")
                    {
                        pressETxt.text = "";
                        if (completedMissions == 1)
                        {
                            SoundManager.PlaySound("door");
                            saveDataThroughScenes();
                            SceneManager.LoadScene("EndGameScene");
                        }
                    }
                    if (triggered == "PortaEntrarCasa")
                    {
                        StartCoroutine(displayDialogueText("I should get some bread first.", false, true));
                    }

                    if (triggered == "Barrel")
                    {
                        string newText = "Herbs found.";
                        StartCoroutine(displayDialogueText(newText, false, false));
                        StartCoroutine(pickUpItem("Barrel"));
                        barrilAberto.SetActive(true);
                        SoundManager.PlaySound("barril");
                    }
                    if (triggered == "Pedra")
                    {
                        StartCoroutine(pickUpItem("Pedra"));
                    }
                    if (triggered == "Roda")
                    {
                        string newText = "Nothing found.";
                        StartCoroutine(displayDialogueText(newText, false, false));
                    }
                    if (triggered == "Wood")
                    {
                        StartCoroutine(pickUpItem("Wood"));
                    }
                    if (triggered == "Corda")
                    {
                        StartCoroutine(pickUpItem("Corda"));
                    }
                    if (triggered == "Oleo")
                    {
                        StartCoroutine(pickUpItem("Oleo"));
                    }
                    if (triggered == "BancaVerde")
                    {
                        string newText = "-Hello there. Wanna buy some green glowy powder?";
                        StartCoroutine(displayDialogueText(newText, false, false));
                    }
                    if (triggered == "BancaLaranja")
                    {
                        string newText = "-Good morning Sir , care for some berries?";
                        StartCoroutine(displayDialogueText(newText, false, false));
                    }
                    if (triggered == "BancaCiano")
                    {
                        string newText = "-Chop..chop..chop.";
                        StartCoroutine(displayDialogueText(newText, false, false));
                    }
                    if (triggered == "BancaVermelha")
                    {
                        string newText = "-Would you like some bread?";
                        StartCoroutine(displayDialogueText(newText, false, false));
                        pressETxt.text = "Buy bread";
                        StartCoroutine(buyBread());
                    }
                    if (triggered == "BancaAmarela")
                    {
                        string newText = "-Hey kid! Don't touch the food!";
                        //MIUDO A FUGIR
                        StartCoroutine(displayDialogueText(newText, false, false));
                    }
                    if (triggered == "BancaPreta")
                    {
                        string newText = "-Hey lad, you look strong! How about a sword?";
                        StartCoroutine(displayDialogueText(newText, false, true));
                    }
                    if (triggered == "SenhorSentado")
                    {
                        string newText = "-Hey man, have you heard what happen to those people... forget it let's think positive";
                        StartCoroutine(displayDialogueText(newText, false, false));
                    }
                    if (triggered == "FilhoMae")
                    {
                        string newText = "-Mom i am scared... what if i start going crazy too?";
                        StartCoroutine(displayDialogueText(newText, false, false));
                    }
                    if (triggered == "MulherVenderChao")
                    {
                        string newText = "-Please buy my vegetables.";
                        StartCoroutine(displayDialogueText(newText, false, false));
                    }
                    if (triggered == "Guarda")
                    {
                        string newText = "-Don't look at me peasant.";
                        StartCoroutine(displayDialogueText(newText, false, false));
                    }
                    if (triggered == "Poco")
                    {
                        pressETxt.text = "";
                        if (inventory.GetComponent<InventoryDisplay>().checkItemList("BaldeVazio"))
                        {
                            inventory.GetComponent<InventoryDisplay>().deleteItemList("BaldeVazio");
                            inventory.GetComponent<InventoryDisplay>().addItemList("Balde");
                            string newText = "Filled bucket.";
                            StartCoroutine(displayDialogueText(newText, false, true));
                            SoundManager.PlaySound("poco");
                        }
                        else if (inventory.GetComponent<InventoryDisplay>().checkItemList("Balde"))
                        {
                            string newText = "My bucket is already filled wtih water.";
                            StartCoroutine(displayDialogueText(newText, false, true));
                        }
                        else
                        {
                            string newText = "Bucket required.";
                            StartCoroutine(displayDialogueText(newText, false, true));
                        }
                    }
                    if (triggered == "SemAbrigo")
                    {
                        string newText = "-Behold, the day of the Lord comes, cruel, with wrath and fierce anger!";
                        StartCoroutine(displayDialogueText(newText, false, false));
                    }
                    if (triggered == "Fogueira")
                    {
                        pressETxt.text = "";
                        if (inventory.GetComponent<InventoryDisplay>().checkItemList("Balde"))
                        {
                            gameObject.GetComponent<PlayerScript>().animator.SetTrigger("IsWatering");
                            fogueira.GetComponent<Animator>().SetTrigger("UseWater");
                            gameObject.GetComponent<PlayerScript>().animator.SetBool("IsAsh", true);
                            inventory.GetComponent<InventoryDisplay>().deleteItemList("Balde");
                            inventory.GetComponent<InventoryDisplay>().addItemList("BaldeVazio");
                            fogueira.GetComponent<Collider2D>().isTrigger = true;
                            fogueiraTrigger.SetActive(false);
                        }
                        else if (!inventory.GetComponent<InventoryDisplay>().checkItemList("Balde"))
                        {
                            string newText = "My bucket does not have water.";
                            StartCoroutine(displayDialogueText(newText, false, true));
                        }
                        else if (!inventory.GetComponent<InventoryDisplay>().checkItemList("BaldeVazio"))
                        {
                            string newText = "I don't have a bucket.";
                            StartCoroutine(displayDialogueText(newText, false, true));

                        }

                    }
                    if (triggered == "NightVillageToHome")
                    {
                        saveDataThroughScenes();
                        SceneManager.LoadScene("LVL4 - BackHome");
                    }
                    if (triggered == "Faca")
                    {
                        StartCoroutine(pickUpItem("Faca"));
                    }
                    if (triggered == "Vela")
                    {
                       lightcandle.SetActive(true);
                       completedMissions = 1;
                    }
                }
                else if (Input.GetButtonDown("Save"))
                {
                    if (triggered == "Vela")
                    {
                        if (SceneManager.GetActiveScene().name == "LVL4 - BackHome") {
                            if (completedMissions == 1)
                            {
                                gameObject.GetComponent<PlayerData>().PlayerDataSave();
                                string newText = "Game saved.";
                                StartCoroutine(displayDialogueText(newText, false, true));
                            }
                        }
                        else
                        {
                            gameObject.GetComponent<PlayerData>().PlayerDataSave();
                            string newText = "Game saved.";
                            StartCoroutine(displayDialogueText(newText, false, true));
                        }
                    }
                }
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (playerInteractionsEnabled)
        {
            if (other.tag == "Armario")
            {
                pressETxt.text = "Search";
                triggered = other.tag;
                offTrigger = false;
            }

            if (other.tag == "Vela")
            {
                if(SceneManager.GetActiveScene().name == "LVL4 - BackHome") {
                    if (completedMissions == 1)
                    {
                        pressETxt.text = "Save (Press L)";
                        triggered = other.tag;
                        offTrigger = false;
                    }
                    else if (completedMissions == 0 && gameObject.GetComponent<PlayerScript>().getLit()==true)
                    {
                        pressETxt.text = "Light the candle";
                        triggered = other.tag;
                        offTrigger = false;
                    }
                }
                else
                {
                    pressETxt.text = "Save (Press L)";
                    triggered = other.tag;
                    offTrigger = false;
                }
            }

            if (other.tag == "Bau")
            {
                pressETxt.text = "Search";
                triggered = other.tag;
                offTrigger = false;
            }

            if (other.tag == "Saco")
            {
                pressETxt.text = "Search";
                triggered = other.tag;
                offTrigger = false;
            }

            if (other.tag == "Porta")
            {

                if (completedMissions == 3)
                    pressETxt.text = "Open";
                triggered = other.tag;
                offTrigger = false;
            }

            if (other.tag == "Pedra")
            {
                pressETxt.text = "Pick up stone";
                triggered = other.tag;
                offTrigger = false;
            }
            if (other.tag == "Roda")
            {
                pressETxt.text = "Search wheel";
                triggered = other.tag;
                offTrigger = false;
            }

            if (other.tag == "Barrel")
            {
                pressETxt.text = "Search barrel";
                triggered = other.tag;
                offTrigger = false;
            }

            if (other.tag == "Wood")
            {
                pressETxt.text = "Pick up wood";
                triggered = other.tag;
                offTrigger = false;
            }
            if (other.tag == "Corda")
            {
                pressETxt.text = "Pick up rope";
                triggered = other.tag;
                offTrigger = false;
            }
            if (other.tag == "Oleo")
            {
                pressETxt.text = "Pick up oil";
                triggered = other.tag;
                offTrigger = false;
            }
            if (other.tag == "PortaEntrarCasa")
            {
                pressETxt.text = "Enter";
                triggered = other.tag;
                offTrigger = false;
            }
            if (other.tag == "PortaEntrarMercado")
            {
                saveDataThroughScenes();
                SceneManager.LoadScene("LVL3 - Market");
            }
            if (other.tag == "BancaVerde")
            {
                pressETxt.text = "Talk";
                triggered = other.tag;
                offTrigger = false;
            }
            if (other.tag == "BancaLaranja")
            {
                pressETxt.text = "Talk";
                triggered = other.tag;
                offTrigger = false;
            }
            if (other.tag == "BancaCiano")
            {
                pressETxt.text = "Talk";
                triggered = other.tag;
                offTrigger = false;
            }
            if (other.tag == "BancaVermelha")
            {
                pressETxt.text = "Talk";
                triggered = other.tag;
                offTrigger = false;
            }
            if (other.tag == "BancaAmarela")
            {
                pressETxt.text = "Talk";
                triggered = other.tag;
                offTrigger = false;
            }
            if (other.tag == "BancaPreta")
            {
                pressETxt.text = "Talk";
                triggered = other.tag;
                offTrigger = false;
            }
            if (other.tag == "SenhorSentado")
            {
                pressETxt.text = "Talk";
                triggered = other.tag;
                offTrigger = false;
            }
            if (other.tag == "FilhoMae")
            {
                pressETxt.text = "Talk";
                triggered = other.tag;
                offTrigger = false;
            }
            if (other.tag == "MulherVenderChao")
            {
                pressETxt.text = "Talk";
                triggered = other.tag;
                offTrigger = false;
            }
            if (other.tag == "Guarda")
            {
                pressETxt.text = "Talk";
                triggered = other.tag;
                offTrigger = false;
            }
            if(other.tag== "TriggerCutSceneMarket")
            {
                if (sceneMarket == 0)
                {
                    if (gameObject.GetComponent<PlayerScript>().cutSceneMarket)
                        gameObject.GetComponent<PlayerScript>().cutScene = true;
                }
            }
            if(other.tag == "MarketToVillage")
            {
                saveDataThroughScenes();
                SceneManager.LoadScene("LVL1 - Village");
            }
            if (other.tag == "EnterForestMarket")
            {
                saveDataThroughScenes();
                SceneManager.LoadScene("LVL2 - Forest");
            }
            if(other.tag == "CrouchHelper")
            {
                pressETxt.text= "Crouch (Press LCtrl)";
            }
            if (other.tag == "SForestToMarket")
            {
                saveDataThroughScenes();
                SceneManager.LoadScene("LVL3 - Market");
            }
            if (other.tag == "EnterBigForest")
            {
                saveDataThroughScenes();
                SceneManager.LoadScene("LVL2 - Big Forest");
            }
            if (other.tag == "BigForestToNightVillage")
            {
                saveDataThroughScenes();
                SceneManager.LoadScene("LVL4 - NightVillage");
            }
            if (other.tag == "BigForestBackForest")
            {
                saveDataThroughScenes();
                SceneManager.LoadScene("LVL2 - Forest");
            }
            if (other.tag == "Poco")
            {
                if (firstInterPoco)
                {
                    string newText = "I am kinda thirsty. Some water would be nice.";
                    StartCoroutine(displayDialogueText(newText, false, true));
                    firstInterPoco = false;
                }
                pressETxt.text = "Get water";
                triggered = other.tag;
                offTrigger = false;
            }
            if (other.tag == "SemAbrigo")
            {
                pressETxt.text = "Talk";
                triggered = other.tag;
                offTrigger = false;
            }
            if (other.tag == "Fogueira")
            {
                pressETxt.text = "Put out fire";
                triggered = other.tag;
                offTrigger = false;
            }

            if(other.tag == "Demon")
            {
                gameObject.GetComponent<PlayerScript>().enterMonster = true;
            }
            if (other.tag == "LShiftWarning" && firstSanity)
            {
                StartCoroutine(displayDialogueText("I am feeling dizzy... What is going on?",false, true));
                pressETxt.text = "Cover ears (Press LShift)";
                firstSanity = false;
            }

            if(other.tag == "NightVillageToHome")
            {
                pressETxt.text = "Enter";
                triggered = other.tag;
                offTrigger = false;
            }

            if(other.tag == "RockFalling")
            {
                pedregulho.GetComponent<Animator>().SetTrigger("IsRockFalling");
                pedregulhoTrigger.SetActive(false);
                StartCoroutine(rockDialog());
            }
            if (other.tag == "Faca")
            {
                pressETxt.text = "Pick up knife";
                triggered = other.tag;
                offTrigger = false;
            }
        }

    }

    public IEnumerator rockDialog()
    {
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(displayDialogueText("That was way too close. Damn nobles!", false, true));
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (playerInteractionsEnabled)
        {
            if (other.tag == "Armario")
            {
                pressETxt.text = "";
                triggered = "";
                offTrigger = true;
            }

            if (other.tag == "Vela")
            {
                pressETxt.text = "";
                triggered = "";
                offTrigger = true;
            }

            if (other.tag == "Bau")
            {
                pressETxt.text = "";
                triggered = "";
                offTrigger = true;
            }

            if (other.tag == "Saco")
            {
                pressETxt.text = "";
                triggered = "";
                offTrigger = true;
            }

            if (other.tag == "Porta")
            {
                pressETxt.text = "";
                triggered = "";
                offTrigger = true;
            }

            if (other.tag == "Pedra")
            {
                pressETxt.text = "";
                triggered = "";
                offTrigger = true;
            }
            if (other.tag == "Roda")
            {
                pressETxt.text = "";
                triggered = "";
                offTrigger = true;
            }
            if (other.tag == "Barrel")
            {
                pressETxt.text = "";
                triggered = "";
                offTrigger = true;
            }
            if (other.tag == "Corda")
            {
                pressETxt.text = "";
                triggered = "";
                offTrigger = true;
            }
            if (other.tag == "Wood")
            {
                pressETxt.text = "";
                triggered = "";
                offTrigger = true;
            }
            if (other.tag == "Oleo")
            {
                pressETxt.text = "";
                triggered = "";
                offTrigger = true;
            }
            if (other.tag == "PortaEntrarCasa")
            {
                pressETxt.text = "";
                triggered = "";
                offTrigger = true;
            }
            if (other.tag == "PortaEntrarMercado")
            {
                pressETxt.text = "";
                triggered = "";
                offTrigger = true;
            }
            if (other.tag == "BancaVerde")
            {
                pressETxt.text = "";
                triggered = "";
                offTrigger = true;
            }
            if (other.tag == "BancaLaranja")
            {
                pressETxt.text = "";
                triggered = "";
                offTrigger = true;
            }
            if (other.tag == "BancaCiano")
            {
                pressETxt.text = "";
                triggered = "";
                offTrigger = true;
            }
            if (other.tag == "BancaVermelha")
            {
                pressETxt.text = "";
                triggered = "";
                offTrigger = true;
            }
            if (other.tag == "BancaAmarela")
            {
                pressETxt.text = "";
                triggered = "";
                offTrigger = true;
            }
            if (other.tag == "BancaPreta")
            {
                pressETxt.text = "";
                triggered = "";
                offTrigger = true;
            }
            if (other.tag == "SenhorSentado")
            {
                pressETxt.text = "";
                triggered = "";
                offTrigger = true;
            }
            if (other.tag == "FilhoMae")
            {
                pressETxt.text = "";
                triggered = "";
                offTrigger = true;
            }
            if (other.tag == "MulherVenderChao")
            {
                pressETxt.text = "";
                triggered = "";
                offTrigger = true;
            }
            if (other.tag == "Guarda")
            {
                pressETxt.text = "";
                triggered = "";
                offTrigger = true;
            }
            if (other.tag == "Poco")
            {
                pressETxt.text = "";
                triggered = "";
                offTrigger = true;
            }
            if (other.tag == "SemAbrigo")
            {
                pressETxt.text = "";
                triggered = "";
                offTrigger = true;
            }
            if (other.tag == "Fogueira")
            {
                pressETxt.text = "";
                triggered = "";
                offTrigger = true;
            }
            if (other.tag == "CrouchHelper")
            {
                pressETxt.text = "";
                triggered = "";
                offTrigger = true;
            }
            if (other.tag == "LShiftWarning")
            {
                pressETxt.text = "";
                triggered = "";
                offTrigger = true;
            }
            if (other.tag == "Faca")
            {
                pressETxt.text = "";
                triggered = "";
                offTrigger = true;
            }
        }
    }
    public IEnumerator displayDialogueText(string newText, bool multipleDialogues, bool first)
    {
        triggered = ""; //um bocado cheat
        DialogBox.SetActive(true);
        if (multipleDialogues)
        {
            gameObject.GetComponent<PlayerScript>().movePlayer = false;
            string[] auxNewText = newText.Split('\n');
            foreach (string dText in auxNewText)
            {
                DialogBox.SetActive(true);
                int wordCounter = dText.Length;
                int counter = 1;
                while (counter != wordCounter)
                {
                    if (dText[0].Equals('m'))
                    {
                        dialogueText.color = new Color32(165,62,204, 255);
                    }
                    else if (dText[0].Equals('p'))
                    {
                        dialogueText.color = Color.black;
                    }
                    else if (dText[0].Equals('s'))
                    {
                        dialogueText.color = new Color32(12, 182, 180, 255);
                    }
                    else
                    {
                        //Externos
                        dialogueText.color = new Color32(150, 113, 74, 255);
                    }
                    dialogueText.text = dText.Substring(1, counter++);
                    yield return new WaitForSeconds(0.02f);
                }
                yield return new WaitUntil(() => (Input.GetButtonDown("Interact")));
            }
            gameObject.GetComponent<PlayerScript>().movePlayer = true;
            pressETxt.text = "";
        }
        else
        {
            int wordCounter = newText.Length + 1;
            int counter = 1;
            int initPos = 0;
            dialogueText.color = Color.black;
            if (newText[0].Equals('-'))
            {
                dialogueText.color = new Color32(150, 113, 74, 255);
                initPos = 1;
                counter++;
                wordCounter--;
            }
            while (counter != wordCounter)
            {
                dialogueText.text = newText.Substring(initPos, counter++);
                yield return new WaitForSeconds(0.02f);
            }
        }
        if (first)
            yield return new WaitForSeconds(3.0f);
        else
            yield return new WaitUntil(() => (Input.GetButtonDown("Interact") == true) || offTrigger == true);
        dialogueText.text = "";
        DialogBox.SetActive(false);

    }
    public IEnumerator secondCutscene()
    {
        yield return new WaitUntil(() => completedMissions==3);
        yield return new WaitForSeconds(0.5f);
        playerInteractionsEnabled = false;
        gameObject.GetComponent<PlayerScript>().cutSceneNumber = 2;
        gameObject.GetComponent<PlayerScript>().cutScene = true;
    }
    private void saveDataThroughScenes()
    {
        PlayerPrefs.SetInt("Hungerbar", gameObject.GetComponent<BarsController>().health);
    }
    private IEnumerator buyBread()
    {
        yield return new WaitForSeconds(0);
        yield return new WaitUntil(() => (Input.GetButtonDown("Interact") == true) || offTrigger == true);
        pressETxt.text = "";
        if (offTrigger != true)
        {
            yield return new WaitForSeconds(0);
            if (inventory.GetComponent<InventoryDisplay>().checkItemList("Pao"))
            {
                string newText = "-I don't have any more money.";
                StartCoroutine(displayDialogueText(newText, false, false));
            }
            else
            {
                StartCoroutine(pickUpItem("Pao"));
                string newText = "-Thank you. Please come again Sir.";
                StartCoroutine(displayDialogueText(newText, false, false));
            }
        }
    }
    private IEnumerator pickUpItem(string item)
    {
        /*if (firstPick)
        {
            Debug.Log("entrou");
            string newText = "Press I to show inventory.";
            StartCoroutine(displayDialogueText(newText, false, false));
            firstPick = false;
        }*/
        pressETxt.text = "+1";
        if (item == "Pedra")
        {
            rock.GetComponent<Renderer>().enabled = false;
            rockEText.SetActive(true);
            if (!inventory.GetComponent<InventoryDisplay>().checkItemList("Pedra"))
            {
                inventory.GetComponent<InventoryDisplay>().addItemList("Pedra");
            }
        }
        if (item == "Wood")
        {
            wood.GetComponent<Renderer>().enabled = false;
            woodEText.SetActive(true);
            if (!inventory.GetComponent<InventoryDisplay>().checkItemList("Wood"))
            {
                inventory.GetComponent<InventoryDisplay>().addItemList("Wood");
            }
        }
        if (item == "Barrel")
        {
            barrel.GetComponent<Renderer>().enabled = false;
            herbsEText.SetActive(true);
            if (!inventory.GetComponent<InventoryDisplay>().checkItemList("ErvaBad"))
            {
                inventory.GetComponent<InventoryDisplay>().addItemList("ErvaBad");
            }
        }
        if (item == "Oleo")
        {
            oleo.GetComponent<Renderer>().enabled = false;
            oleoEText.SetActive(true);
            if (!inventory.GetComponent<InventoryDisplay>().checkItemList("Oleo"))
            {
                inventory.GetComponent<InventoryDisplay>().addItemList("Oleo");
            }
        }
        if (item == "Corda")
        {
            corda.GetComponent<Renderer>().enabled = false;
            cordaEText.SetActive(true);
            if (!inventory.GetComponent<InventoryDisplay>().checkItemList("Corda"))
            {
                inventory.GetComponent<InventoryDisplay>().addItemList("Corda");
            }
        }
        if (item == "Pao")
        {
            paoEText.SetActive(true);
            if (!inventory.GetComponent<InventoryDisplay>().checkItemList("Pao"))
            {
                inventory.GetComponent<InventoryDisplay>().addItemList("Pao");
            }
        }
        if (item == "Faca")
        {
            facaEText.SetActive(true);
            if (!inventory.GetComponent<InventoryDisplay>().checkItemList("Faca"))
            {
                inventory.GetComponent<InventoryDisplay>().addItemList("Faca");
            }
        }
        yield return new WaitForSeconds(1.0f);
        if (item == "Pedra")
        {
            rock.SetActive(false);
            rockEText.SetActive(false);
        }
        else if (item == "Wood")
        {
            wood.SetActive(false);
            woodEText.SetActive(false);
        }
        else if (item == "Barrel")
        {
            barrel.SetActive(false);
            herbsEText.SetActive(false);
        }
        else if (item == "Corda")
        {
            corda.SetActive(false);
            cordaEText.SetActive(false);
        }
        else if (item == "Oleo")
        {
            oleo.SetActive(false);
            oleoEText.SetActive(false);
        }
        else if (item == "Pao")
        {
            paoEText.SetActive(false);
        }
        else if (item == "Faca")
        {
            facaEText.SetActive(false);
            faca.SetActive(false);
        }
        pressETxt.text = "";
    }
    private void displayInventory()
    {
        if (inventory.activeSelf)
        {
            //sair
            inventory.SetActive(false);
            inventory.GetComponent<InventoryDisplay>().exit = true;
        }
        else
        {
            StartCoroutine(inventory.GetComponent<InventoryDisplay>().display());
            inventory.SetActive(true);
        }
    }

    private static int sceneMarket = 0;
    private static int sceneHome = 0;
    private static int sceneForest = 0;
    private static int sceneBigForest = 0;
    private static int sceneVillage = 0;
    private void LoadScene()
    {
        if (SceneManager.GetActiveScene().name == "LVL1 - Home")
        {
            if (sceneHome > 0)
            {
                if (gameObject.GetComponent<BarsController>().isDead()==true)
                {
                    gameObject.transform.position = gameObject.GetComponent<PlayerData>().getPosition();
                }
            }
            sceneHome++;
        }
        if (SceneManager.GetActiveScene().name == "LVL1 - Village")
        {
            if (sceneVillage > 0)
            {
                if (inventory.GetComponent<InventoryDisplay>().checkItemList("ErvaBad"))
                {
                    barrel.SetActive(false);
                }
                if (inventory.GetComponent<InventoryDisplay>().checkItemList("Pedra"))
                {
                    rock.SetActive(false);
                }
                if (inventory.GetComponent<InventoryDisplay>().checkItemList("Wood"))
                {
                    wood.SetActive(false);
                }
                //posição do jogador
                if (gameObject.GetComponent<BarsController>().isDead() == true)
                {
                    //saiu de casa
                    gameObject.transform.position = new Vector2(2.76f, -3.65f);
                    gameObject.GetComponent<BarsController>().notDead();
                }
                else
                {
                    //saiu do mercado
                    gameObject.transform.position = new Vector2(53.76f, -3.65f);
                    gameObject.GetComponent<PlayerScript>().FlipPlayer();
                }
            }
            else
            {
                StartCoroutine(displayDialogueText("To show inventory press I", false, true));
            }
            sceneVillage++;
        }
        if (SceneManager.GetActiveScene().name == "LVL3 - Market")
        {
            if (sceneMarket > 0)
            {
                if (inventory.GetComponent<InventoryDisplay>().checkItemList("Corda"))
                {
                    corda.SetActive(false);
                }
                if (inventory.GetComponent<InventoryDisplay>().checkItemList("Oleo"))
                {
                    oleo.SetActive(false);
                }
                if (inventory.GetComponent<InventoryDisplay>().checkItemList("ErvaBad"))
                {
                    barrel.SetActive(false);
                }
                if (gameObject.GetComponent<BarsController>().isDead() == true)
                {
                    //reborn
                    gameObject.transform.position = gameObject.GetComponent<PlayerData>().getPosition();
                    gameObject.GetComponent<BarsController>().notDead();
                    //Cutscene do mercado não, dar load do cavalo e dançarinos ?
                }
            }
            sceneMarket++;
        }
        if (SceneManager.GetActiveScene().name == "LVL2 - Forest")
        {
            if (sceneForest > 0)
            {
                //inventário aqui
                if (gameObject.GetComponent<BarsController>().isDead() == true)
                {
                    gameObject.GetComponent<BarsController>().notDead();
                }
                else
                {
                    gameObject.transform.position = new Vector2(22.63f, -3.88f);
                    gameObject.GetComponent<PlayerScript>().FlipPlayer();
                }
            }
            sceneForest++;
        }
        if (SceneManager.GetActiveScene().name == "LVL2 - Big Forest")
        {
            if (sceneBigForest > 0)
            {
                //inventário aqui

                if (gameObject.GetComponent<BarsController>().isDead() == true)
                {
                    //reborn
                    gameObject.transform.position = gameObject.GetComponent<PlayerData>().getPosition();
                    gameObject.GetComponent<BarsController>().notDead();
                }
            }
            sceneBigForest++;
        }
        if (SceneManager.GetActiveScene().name == "LVL4 - NightVillage")
        {
            StartCoroutine(displayDialogueText("It's night already... no wonder it was so dark in the forest.",false, true));
            if (inventory.GetComponent<InventoryDisplay>().checkItemList("ErvaBad"))
            {
                barrel.SetActive(false);
            }
            if (inventory.GetComponent<InventoryDisplay>().checkItemList("Pedra"))
            {
                rock.SetActive(false);
            }
            if (inventory.GetComponent<InventoryDisplay>().checkItemList("Wood"))
            {
                wood.SetActive(false);
            }
        }
    }
} 
