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
        buildWood = 1;
        buildHerb = 1;
    }
    private void craftImage(GameObject slotNumber, string itemName)
    {
        Image[] images = slotNumber.GetComponentsInChildren<Image>();
        foreach (Image img in images)
        {
            if (img.name == itemName)
            {
                img.enabled = true;
            }
        }
    }
    private int buildWood=1;
    private int buildHerb = 1;
    void ItemSelecionado1()
    {
        if (item1=="Wood") {
            janelaTxt.text = "exemplo xxx madeira faz um balde dheifsd sfjeisfjisd.";
            if (buildWood == 1)
            {
                clearSlots();
                craftImage(FirstCraft, "Wood");
                slots[0].SetActive(true);
                buildWood++;
            }
            else if (buildWood == 2)
            {
                craftImage(SecondCraft, "Wood");
                slots[1].SetActive(true);
                buildWood++;
            }
            else if (buildWood == 3)
            {
                craftImage(ThirdCraft, "Wood");
                slots[2].SetActive(true);
                craftImage(outputCraft, "BaldeVazio");
                slots[3].SetActive(true);
                displayItems.Add("BaldeVazio");
                buildWood = 0;
            }
        }
        else if (item1 == "ErvaBad")
        {
            janelaTxt.text = "exemplo xxx mcomida";
            if (buildHerb == 1)
            {
                clearSlots();
                craftImage(FirstCraft, "ErvaBad");
                slots[0].SetActive(true);
                buildHerb++;
            }
            else if (buildHerb == 2)
            {
                craftImage(SecondCraft, "ErvaBad");
                slots[1].SetActive(true);
                craftImage(outputCraft, "Erva");
                slots[3].SetActive(true);
                displayItems.Add("Erva");
                buildHerb = 0;
            }
        }
        else if(item1 == "Erva")
        {
            //Eat
            Player.GetComponent<BarsController>().hungerBarGaining();
        }
    }
    void ItemSelecionado2()
    {
        if (item2 == "Wood")
        {
            if (buildWood == 1)
            {
                clearSlots();
                craftImage(FirstCraft, "Wood");
                slots[0].SetActive(true);
                buildWood++;
            }
            else if (buildWood == 2)
            {
                craftImage(SecondCraft, "Wood");
                slots[1].SetActive(true);
                buildWood++;
            }
            else if (buildWood == 3)
            {
                craftImage(ThirdCraft, "Wood");
                slots[2].SetActive(true);
                craftImage(outputCraft, "BaldeVazio");
                slots[3].SetActive(true);
                displayItems.Add("BaldeVazio");
                buildWood = 0;
            }
        }
        else if (item2 == "ErvaBad")
        {
            janelaTxt.text = "exemplo xxx mcomida junta duas";
            if (buildHerb == 1)
            {
                clearSlots();
                craftImage(FirstCraft, "ErvaBad");
                slots[0].SetActive(true);
                buildHerb++;
            }
            else if (buildHerb == 2)
            {
                craftImage(SecondCraft, "ErvaBad");
                slots[1].SetActive(true);
                craftImage(outputCraft, "Erva");
                slots[3].SetActive(true);
                displayItems.Add("Erva");
                buildHerb = 0;
            }
        }
        else if (item2 == "Erva")
        {
            //Eat
            Player.GetComponent<BarsController>().hungerBarGaining();
        }

    }
    void ItemSelecionado3()
    {
        Debug.Log("ItemNaoSelecionado3");
    }
}
