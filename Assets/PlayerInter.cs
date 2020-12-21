using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerInter : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI pressETxt;
    public GameObject DialogBox;

    private string triggered = "";
    private int completedMissions; //Para obrigar o jogador a fazer tudo antes de sair de casa
    private bool offTrigger = false;

    public bool playerInteractionsEnabled;
    void Start()
    {
        DialogBox.SetActive(false);
        playerInteractionsEnabled = false;
    }

    private bool firstVisitA = true, firstVisitG = true, firstVisitS = true;
    void Update()
    {
        if (playerInteractionsEnabled)
        {
            if (triggered != "")
            {
                if (Input.GetButtonDown("Interact"))
                {
                    if (triggered == "Armario")
                    {
                        pressETxt.text = "";
                        string newText = "Empty closet.";
                        StartCoroutine(displayDialogueText(newText, false));
                        if (firstVisitA)
                        {
                            completedMissions++;
                            firstVisitA = false;
                        }
                    }

                    if (triggered == "Gaveta")
                    {
                        pressETxt.text = "";
                        string newText = "Empty closet."; //Para teste
                        StartCoroutine(displayDialogueText(newText, false));
                        //DIZER Q TEM UMA CHAVE E QUE APANHOU?
                        if (firstVisitG)
                        {
                            completedMissions++;
                            firstVisitG = false;
                        }
                    }

                    if (triggered == "Saco")
                    {
                        pressETxt.text = "";
                        string newText = "Empty closet.";
                        StartCoroutine(displayDialogueText(newText, false));
                        if (firstVisitS)
                        {
                            completedMissions++;
                            firstVisitS = false;
                        }
                    }

                    if (triggered == "Porta")
                    {
                        pressETxt.text = "";
                        if (completedMissions == 3) //AJUSTAR AO NÚMERO CERTO
                        {
                            string newText = "Bye bye bitches";
                            StartCoroutine(displayDialogueText(newText, false));
                        }
                    }
                }
            }
        }

    }
    public IEnumerator displayDialogueText(string newText, bool multipleDialogues)
    {
        triggered = ""; //um bocado cheat
        DialogBox.SetActive(true);
        if (multipleDialogues)
        {
            gameObject.GetComponent<PlayerScript>().movePlayer = false;
            string[] auxNewText = newText.Split('\n');
            foreach (string dText in auxNewText)
            {

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

        yield return new WaitUntil(() => (Input.GetButtonDown("Interact") == true) || offTrigger == true);
        dialogueText.text = "";
        DialogBox.SetActive(false);

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

            if (other.tag == "Gaveta")
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
                pressETxt.text = "Open (Press E)";
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

            if (other.tag == "Gaveta")
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
        }
    }
} 
