using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Equipement[] shopItemSO;
    public ShopTemplate[] shopPanels;
    public GameObject[] shopPanelGO;
    public Button[] myPurchaseBtns;

    private ResourcesManager resourcesManager;
    private Inventory inventory;

    void Start()
    {
        resourcesManager = FindObjectOfType<ResourcesManager>();
        inventory = FindObjectOfType<Inventory>();

        for (int i = 0; i < shopItemSO.Length; i++)
        {
            shopPanelGO[i].SetActive(true);
        }
        LoadPanels();
    }

    void Update()
    {

    }

    public void CheckIfCanBuy(int index)
    {
        if (shopItemSO[index].PriceType == 1)
        {
            if (shopItemSO[index].Price <= resourcesManager.resource1)
            {
                myPurchaseBtns[index].interactable = true;
                resourcesManager.resource1 -= shopItemSO[index].Price;
                inventory.AddItem(shopItemSO[index]); // Ajoutez l'item à l'inventaire du joueur
            }
        }
        else if (shopItemSO[index].PriceType == 2)
        {
            if (shopItemSO[index].Price <= resourcesManager.resource2)
            {
                myPurchaseBtns[index].interactable = true;
                resourcesManager.resource2 -= shopItemSO[index].Price;
                inventory.AddItem(shopItemSO[index]); // Ajoutez l'item à l'inventaire du joueur
            }
        }
        else if (shopItemSO[index].PriceType == 3)
        {
            if (shopItemSO[index].Price <= resourcesManager.resource3)
            {
                myPurchaseBtns[index].interactable = true;
                resourcesManager.resource3 -= shopItemSO[index].Price;
                inventory.AddItem(shopItemSO[index]); // Ajoutez l'item à l'inventaire du joueur
            }
        }
        else if (shopItemSO[index].PriceType == 4)
        {
            if (shopItemSO[index].Price <= resourcesManager.resource4)
            {
                myPurchaseBtns[index].interactable = true;
                resourcesManager.resource4 -= shopItemSO[index].Price;
                inventory.AddItem(shopItemSO[index]); // Ajoutez l'item à l'inventaire du joueur
            }
        }
        else
        {
            myPurchaseBtns[index].interactable = false;
        }
    }

    public void LoadPanels()
    {
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            shopPanels[i].titleText.text = shopItemSO[i].itemName;
            shopPanels[i].descText.text = shopItemSO[i].Description;
            shopPanels[i].itemImage.sprite = shopItemSO[i].icon;
            switch (shopItemSO[i].Stat1Type)
            {
                case 1:
                    shopPanels[i].Stat1.text = "Attack + " + shopItemSO[i].Stat1Value.ToString();
                    break;
                case 2:
                    shopPanels[i].Stat1.text = "Attack + " + (shopItemSO[i].StatPercentage * 100).ToString("F1") + "%";
                    break;
                case 3:
                    shopPanels[i].Stat1.text = "Defense + " + shopItemSO[i].Stat1Value.ToString();
                    break;
                case 4:
                    shopPanels[i].Stat1.text = "Attack + " + (shopItemSO[i].StatPercentage * 100).ToString("F1") + "%";
                    break;
                case 5:
                    shopPanels[i].Stat1.text = "HP : " + shopItemSO[i].Stat1Value.ToString();
                    break;
                case 6:
                    shopPanels[i].Stat1.text = "HP + " + (shopItemSO[i].StatPercentage * 100).ToString("F1") + "%";
                    break;
            }

            switch (shopItemSO[i].Stat2Type)
            {
                case 1:
                    shopPanels[i].Stat2.text = "Attack + " + shopItemSO[i].Stat2Value.ToString();
                    break;
                case 2:
                    shopPanels[i].Stat2.text = "Attack + " + (shopItemSO[i].StatPercentage * 100).ToString("F1") + "%";
                    break;
                case 3:
                    shopPanels[i].Stat2.text = "Defense + " + shopItemSO[i].Stat2Value.ToString();
                    break;
                case 4:
                    shopPanels[i].Stat2.text = "Attack + " + (shopItemSO[i].StatPercentage * 100).ToString("F1") + "%";
                    break;
                case 5:
                    shopPanels[i].Stat2.text = "HP : " + shopItemSO[i].Stat2Value.ToString();
                    break;
                case 6:
                    shopPanels[i].Stat2.text = "HP + " + (shopItemSO[i].StatPercentage * 100).ToString("F1") + "%";
                    break;
            }

            switch (shopItemSO[i].PriceType)
            {
                case 1:
                    shopPanels[i].costText.text = "Food : " + shopItemSO[i].Price.ToString();
                    break;
                case 2:
                    shopPanels[i].costText.text = "Stone : " + shopItemSO[i].Price.ToString();
                    break;
                case 3:
                    shopPanels[i].costText.text = "Wood : " + shopItemSO[i].Price.ToString();
                    break;
            }
        }
    }
}