using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    public List<string> displayItems;

    public Button setaDOWN;
    public Button setaUP;

    public Button ItemNaoSelecionado1;
    public Button ItemNaoSelecionado2;
    public Button ItemNaoSelecionado3;

    void Start()
    {
        displayItems = new List<string>();
        displayItems.Add("Balde");
        displayItems.Add("BaldeVazio");
        displayItems.Add("Erva");
        displayItems.Add("ErvaBad");
        displayItems.Add("Oleo");
        displayItems.Add("Pedra");
        displayItems.Add("Wood");

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
    public IEnumerator display()
    {
        Image[] images = ItemNaoSelecionado1.GetComponentsInChildren<Image>();
        foreach (Image img in images)
        {
            if(img.name!= "ItemNaoSelecionado1")
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
        exit = false;
        int posFirst = 0;

        while (true)
        {
            scrollup = false; scrolldown = false;
            Debug.Log("pos=" + posFirst);
            if (displayItems.Count > posFirst && displayItems[posFirst] != null)
            {
                Debug.Log(displayItems[posFirst]);
                activeImage(ItemNaoSelecionado1, displayItems[posFirst]);
            }
            if (displayItems.Count > posFirst + 1 && displayItems[posFirst + 1] != null)
            {
                Debug.Log(displayItems[posFirst + 1]);
                activeImage(ItemNaoSelecionado2, displayItems[posFirst + 1]);
            }
            if (displayItems.Count > posFirst + 2 && displayItems[posFirst + 2] != null)
            {
                Debug.Log(displayItems[posFirst+2]);
                activeImage(ItemNaoSelecionado3, displayItems[posFirst + 2]);
            }

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
        Debug.Log("You have clicked the button!");
        scrolldown = true;
    }
    void ScrollUp()
    {
        //Debug.Log("You have clicked the button!");
        scrollup = true;
    }
    void ItemSelecionado1()
    {
        
    }
    void ItemSelecionado2()
    {

    }
    void ItemSelecionado3()
    {

    }
}
