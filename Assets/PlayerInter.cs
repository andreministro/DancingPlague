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

    public GameObject rock, barrel, roda, wood;
    public GameObject rockEText, herbsEText, rodaEText, woodEText;

    public GameObject bauAberto;
    public GameObject inventory;

    public bool firstPick=true;

    private string triggered = "";
    private int completedMissions;
    private bool offTrigger = false;

    public bool playerInteractionsEnabled;
    void Start()
    {
        DialogBox.SetActive(false);
        inventory.SetActive(false);
        playerInteractionsEnabled = false;
        if(SceneManager.GetActiveScene().name== "LVL1 - Home")
        {
            completedMissions = 0;
            StartCoroutine(secondCutscene());
            bauAberto.SetActive(false);
        }
    }

    private bool firstVisitA = true, firstVisitG = true, firstVisitS = true;
    void Update()
    {
        if (playerInteractionsEnabled)
        {
            if (Input.GetButtonDown("Inventory"))
            {
                displayInventory();
            }

            if (triggered != "")
            {
                if (Input.GetButtonDown("Interact"))
                {
                    if (triggered == "Armario")
                    {
                        pressETxt.text = "";
                        string newText = "Empty.";
                        StartCoroutine(displayDialogueText(newText, false,false));
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
                        StartCoroutine(displayDialogueText(newText, false,false));
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
                        StartCoroutine(displayDialogueText(newText, false,false));
                        if (firstVisitS)
                        {
                            completedMissions++;
                            firstVisitS = false;
                        }
                    }

                    if (triggered == "Porta")
                    {
                        pressETxt.text = "";
                        if (SceneManager.GetActiveScene().name == "LVL1 - BackHome")
                        {
                            saveDataThroughScenes();
                            SceneManager.LoadScene("LVL1 - Village");
                        }
                        else
                        {
                            if (completedMissions == 3)
                            {
                                saveDataThroughScenes();
                                SceneManager.LoadScene("LVL1 - Village");
                            }
                        }
                    }
                    if (triggered == "PortaEntrarCasa")
                    {
                        saveDataThroughScenes();
                        SceneManager.LoadScene("LVL1 - BackHome");
                    }

                    if(triggered== "Barrel") {
                        string newText = "Herbs found.";
                        StartCoroutine(displayDialogueText(newText, false, false));
                        StartCoroutine(pickUpItem("Barrel"));
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
                pressETxt.text = "Search (Press E)";
                triggered = other.tag;
                offTrigger = false;
            }

            if (other.tag == "Vela")
            {
                pressETxt.text = "Save (Press L)";
                triggered = other.tag;
                offTrigger = false;
            }

            if (other.tag == "Bau")
            {
                pressETxt.text = "Search (Press E)";
                triggered = other.tag;
                offTrigger = false;
            }

            if (other.tag == "Saco")
            {
                pressETxt.text = "Search (Press E)";
                triggered = other.tag;
                offTrigger = false;
            }

            if (other.tag == "Porta")
            {
                if (SceneManager.GetActiveScene().name == "LVL1 - BackHome") {
                    triggered = other.tag;
                    offTrigger = false;
                }
                else
                {
                    if (completedMissions == 3)
                        pressETxt.text = "Open (Press E)";
                    triggered = other.tag;
                    offTrigger = false;
                }
            }

            if (other.tag == "Pedra")
            {
                pressETxt.text = "Pickup rock";
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
                pressETxt.text = "Pickup wood";
                triggered = other.tag;
                offTrigger = false;
            }
            if (other.tag == "PortaEntrarCasa")
            {
                pressETxt.text = "Enter home";
                triggered = other.tag;
                offTrigger = false;
            }
        }

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

            if (other.tag == "Wood")
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
                        dialogueText.color = Color.magenta;
                    }
                    else if (dText[0].Equals('p'))
                    {
                        dialogueText.color = Color.black;
                    }
                    else if (dText[0].Equals('s'))
                    {
                        dialogueText.color = Color.blue;
                    }
                    else
                    {
                        //Externos
                        dialogueText.color = Color.gray;
                    }
                    dialogueText.text = dText.Substring(1, counter++);
                    yield return new WaitForSeconds(0.02f);
                }
                yield return new WaitUntil(() => (Input.GetButtonDown("Interact") == true));
            }
            gameObject.GetComponent<PlayerScript>().movePlayer = true;
        }
        else
        {
            int wordCounter = newText.Length + 1;
            int counter = 1;

            while (counter != wordCounter)
            {
                dialogueText.text = newText.Substring(0, counter++);
                yield return new WaitForSeconds(0.02f);
            }
        }
        if (first)
            yield return new WaitForSeconds(2.0f);
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
        PlayerPrefs.SetInt("firstSanity", gameObject.GetComponent<BarsController>().first ? 1 : 0);
    }

    private IEnumerator pickUpItem(string item)
    {
        if (firstPick)
        {
            Debug.Log("entrou");
            string newText = "Press I to show inventory.";
            StartCoroutine(displayDialogueText(newText, false, false));
            firstPick = false;
        }
        pressETxt.text = "+1";
        if (item == "Pedra")
        {
            rock.GetComponent<Renderer>().enabled = false;
            rockEText.SetActive(true);
        }
        if (item == "Wood")
        {
            wood.GetComponent<Renderer>().enabled = false;
            woodEText.SetActive(true);
        }
        if (item == "Barrel")
        {
            barrel.GetComponent<Renderer>().enabled = false;
            herbsEText.SetActive(true);
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
} 
