using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryDisplay : MonoBehaviour
{
    public List<string> displayItems;

    public TextMeshProUGUI janelaTxt;

    public Button setaDOWN;
    public Button setaUP;

    public Button ItemNaoSelecionado1;
    public Button ItemNaoSelecionado2;
    public Button ItemNaoSelecionado3;

    public GameObject outputCraft;
    public GameObject FirstCraft;
    public GameObject SecondCraft;
    public GameObject ThirdCraft;
    public GameObject Player;
    public GameObject[] slots;

    private string item1="", item2="", item3="";

    void Start()
    {
        displayItems = new List<string>();
        //displayItems.Add("Balde");
        //displayItems.Add("BaldeVazio");
        //displayItems.Add("Wood");
        //displayItems.Add("Wood");
        //displayItems.Add("Erva");
        displayItems.Add("Wood");
        displayItems.Add("ErvaBad");
        displayItems.Add("Corda");
        //displayItems.Add("Oleo");
        //displayItems.Add("Pedra");

        Button btn = setaDOWN.GetComponent<Button>();
        btn.onClick.AddListener(ScrollDown);
        btn = setaUP.GetComponent<Button>();
        btn.onClick.AddListener(ScrollUp);

        btn = ItemNaoSelecionado1.GetComponent<Button>();
        btn.onClick.AddListener(ItemSelecionado1);
        btn = ItemNaoSelecionado2.GetComponent<Button>();
        btn.onClick.AddListener(ItemSelecionado2);
        btn = ItemNaoSelecionado3.GetComponent<Button>();
        btn.onClick.AddListener(ItemSelecionado3);


    }
    private void activeImage(Button itemSelecionado, string itemName)
    {
        Image[] images = itemSelecionado.GetComponentsInChildren<Image>();
        foreach (Image img in images)
        {
            if (img.name == itemName)
            {
                img.enabled = true;
            }
        }

    }
    private void unactiveImage(Button itemSelecionado, string itemName){
        Image[] images = itemSelecionado.GetComponentsInChildren<Image>();
        foreach (Image img in images)
        {
            if (img.name == itemName)
            {
                img.enabled = false;
            }
        }
    }
    private bool scrollup = false, scrolldown = false;
    public bool exit;
    private void cleanDisplay()
    {
        Image[] images = ItemNaoSelecionado1.GetComponentsInChildren<Image>();
        foreach (Image img in images)
        {
            if (img.name != "ItemNaoSelecionado1")
                img.enabled = false;

        }
        images = ItemNaoSelecionado2.GetComponentsInChildren<Image>();
        foreach (Image img in images)
        {
            if (img.name != "ItemNaoSelecionado2")
                img.enabled = false;

        }
        images = ItemNaoSelecionado3.GetComponentsInChildren<Image>();
        foreach (Image img in images)
        {
            if (img.name != "ItemNaoSelecionado3")
                img.enabled = false;

        }
        images = FirstCraft.GetComponentsInChildren<Image>();
        foreach (Image img in images)
        {
            img.enabled = false;

        }
        images = SecondCraft.GetComponentsInChildren<Image>();
        foreach (Image img in images)
        {
            img.enabled = false;

        }
        images = ThirdCraft.GetComponentsInChildren<Image>();
        foreach (Image img in images)
        {
            img.enabled = false;

        }
        images = outputCraft.GetComponentsInChildren<Image>();
        foreach (Image img in images)
        {
            img.enabled = false;

        }
        foreach(GameObject slot in slots)
        {
            slot.SetActive(false);
        }
    }
    public IEnumerator display()
    {
        cleanDisplay();
        exit = false;
        int posFirst = 0;

        while (true)
        {
            scrollup = false; scrolldown = false;
            if (displayItems.Count > posFirst && displayItems[posFirst] != null)
            {
                activeImage(ItemNaoSelecionado1, displayItems[posFirst]);
                item1 = displayItems[posFirst];
            }
            else item1 = "";
            if (displayItems.Count > posFirst + 1 && displayItems[posFirst + 1] != null)
            {
                activeImage(ItemNaoSelecionado2, displayItems[posFirst + 1]);
                item2 = displayItems[posFirst+1];
            }
            else item2 = "";
            if (displayItems.Count > posFirst + 2 && displayItems[posFirst + 2] != null)
            {
                activeImage(ItemNaoSelecionado3, displayItems[posFirst + 2]);
                item3 = displayItems[posFirst+2];
            }
            else item3 = "";

            yield return new WaitUntil(() => (scrollup == true || scrolldown == true || exit==true));

            if (displayItems.Count > posFirst && displayItems[posFirst] != null)
            {
                Debug.Log(displayItems[posFirst]);
                unactiveImage(ItemNaoSelecionado1, displayItems[posFirst]);
            }
            if (displayItems.Count > posFirst + 1 && displayItems[posFirst + 1] != null)
            {
                Debug.Log(displayItems[posFirst + 1]);
                unactiveImage(ItemNaoSelecionado2, displayItems[posFirst + 1]);
            }
            if (displayItems.Count > posFirst + 2 && displayItems[posFirst + 2] != null)
            {
                Debug.Log(displayItems[posFirst + 2]);
                unactiveImage(ItemNaoSelecionado3, displayItems[posFirst + 2]);
            }
            if (exit)
            {
                break;
            }
            
            else
            {
                if (scrollup)
                {
                    if(posFirst>=3)
                    {
                        posFirst -= 3;
                    }
                }
                else if(scrolldown)
                {
                    if (displayItems.Count > posFirst+2)
                    {
                        posFirst += 3;
                    }
                }
            }
            
        }
        
    }
    void ScrollDown()
    {
        scrolldown = true;
    }
    void ScrollUp()
    {
        scrollup = true;
    }

    private void clearSlots()
    {
        Image[] images = FirstCraft.GetComponentsInChildren<Image>();
        foreach (Image img in images)
        {
            img.enabled = false;

        }
        images = SecondCraft.GetComponentsInChildren<Image>();
        foreach (Image img in images)
        {
            img.enabled = false;

        }
        images = ThirdCraft.GetComponentsInChildren<Image>();
        foreach (Image img in images)
        {
            img.enabled = false;

        }
        images = outputCraft.GetComponentsInChildren<Image>();
        foreach (Image img in images)
        {
            img.enabled = false;

        }
        foreach (GameObject slot in slots)
        {
            slot.SetActive(false);
        }
        woodCounter = 1;
        badHerbCounter = 1;
    }

    private int objectsSelected = 0;
    private int slotCounter = 1;
    private int woodCounter = 0;
    private int badHerbCounter = 0;
    private int stoneCounter = 0;
    private int oilCounter = 0;
    private int cordaCounter = 0;
    private bool build = false;
    private void craftImage(string itemName)
    {
        GameObject slotNumber = null;
        Image[] images;
        Debug.Log(slotCounter);
        string auxItemName="";

        if (badHerbCounter == 2 && woodCounter == 0 && stoneCounter == 0 && oilCounter == 0 && cordaCounter == 0)
        {
            auxItemName = "Erva";
            slotNumber = outputCraft;
            slots[3].SetActive(true);
            build = true;
        }
        else if (badHerbCounter == 0 && woodCounter == 2 && stoneCounter == 0 && oilCounter == 0 && cordaCounter == 1)
        {
            auxItemName = "BaldeVazio";
            slotNumber = outputCraft;
            slots[3].SetActive(true);
            build = true;
        }
        else if (badHerbCounter == 0 && woodCounter == 1 && stoneCounter == 1 && oilCounter == 1 && cordaCounter == 0)
        {
            auxItemName = "Torcha";
            slotNumber = outputCraft;
            slots[3].SetActive(true);
            build = true;
        }
        if (build)
        {
            images = slotNumber.GetComponentsInChildren<Image>();
            foreach (Image img in images)
            {
                if (img.name == auxItemName)
                {
                    img.enabled = true;
                }
            }
            
            if (slotCounter == 2)
            {
                slotNumber = SecondCraft;
                slots[1].SetActive(true);
            }
            else if (slotCounter == 3)
            {
                slotNumber = ThirdCraft;
                slots[2].SetActive(true);
            }
            images = slotNumber.GetComponentsInChildren<Image>();
            foreach (Image img in images)
            {
                if (img.name == itemName)
                {
                    img.enabled = true;
                }
            }

        }
        else {
            if (slotCounter <= 3)
            {
                if (slotCounter == 1)
                {
                    slotNumber = FirstCraft;
                    slots[0].SetActive(true);
                }
                else if (slotCounter == 2)
                {
                    slotNumber = SecondCraft;
                    slots[1].SetActive(true);
                }
                else if (slotCounter == 3)
                {
                    slotNumber = ThirdCraft;
                    slots[2].SetActive(true);
                }

                images = slotNumber.GetComponentsInChildren<Image>();
                foreach (Image img in images)
                {
                    if (img.name == itemName)
                    {
                        img.enabled = true;
                    }
                }
            }
        }
    }
    void ItemSelecionado1()
    {
        if (build || slotCounter>3)
        {

            clearSlots();
            slotCounter = 1;
            build = false;
            woodCounter = 0;
            badHerbCounter = 0;
            stoneCounter = 0;
            oilCounter = 0;
            cordaCounter = 0;
        }
        if (item1=="Wood") {
            janelaTxt.text = "exemplo xxx madeira faz um balde dheifsd sfjeisfjisd.";
            woodCounter++;
            craftImage("Wood");
            slotCounter++;
        }
        else if (item1 == "ErvaBad")
        {
            janelaTxt.text = "exemplo xxx mcomida";
            badHerbCounter++;
            craftImage("ErvaBad");
            slotCounter++;
        }
        else if (item1 == "Corda")
        {
            janelaTxt.text = "exemplo xxx mcomida";
            cordaCounter++;
            craftImage("Corda");
            slotCounter++;
        }
        else if(item1 == "Erva")
        {
            //Eat
            Player.GetComponent<BarsController>().hungerBarGaining();
        }
    }
    void ItemSelecionado2()
    {
        if (build || slotCounter > 3)
        {

            clearSlots();
            slotCounter = 1;
            build = false;
            woodCounter = 0;
            badHerbCounter = 0;
            stoneCounter = 0;
            oilCounter = 0;
            cordaCounter = 0;
        }
        if (item2 == "Wood")
        {
            janelaTxt.text = "exemplo xxx madeira faz um balde dheifsd sfjeisfjisd.";
            woodCounter++;
            craftImage("Wood");
            slotCounter++;
        }
        else if (item2 == "ErvaBad")
        {
            janelaTxt.text = "exemplo xxx mcomida";
            badHerbCounter++;
            craftImage("ErvaBad");
            slotCounter++;
        }
        else if (item2 == "Corda")
        {
            janelaTxt.text = "exemplo xxx mcomida";
            cordaCounter++;
            craftImage("Corda");
            slotCounter++;
        }
        else if (item2 == "Erva")
        {
            //Eat
            Player.GetComponent<BarsController>().hungerBarGaining();
        }

    }
    void ItemSelecionado3()
    {
        if (build || slotCounter > 3)
        {

            clearSlots();
            slotCounter = 1;
            build = false;
            woodCounter = 0;
            badHerbCounter = 0;
            stoneCounter = 0;
            oilCounter = 0;
            cordaCounter = 0;
        }
        if (item3 == "Wood")
        {
            janelaTxt.text = "exemplo xxx madeira faz um balde dheifsd sfjeisfjisd.";
            woodCounter++;
            craftImage("Wood");
            slotCounter++;
        }
        else if (item3 == "ErvaBad")
        {
            janelaTxt.text = "exemplo xxx mcomida";
            badHerbCounter++;
            craftImage("ErvaBad");
            slotCounter++;
        }
        else if (item3 == "Corda")
        {
            janelaTxt.text = "exemplo xxx mcomida";
            cordaCounter++;
            craftImage("Corda");
            slotCounter++;
        }
        else if (item3 == "Erva")
        {
            //Eat
            Player.GetComponent<BarsController>().hungerBarGaining();
        }
    }
}
