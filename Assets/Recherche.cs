using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Recherche : MonoBehaviour
{
    public int id;
    public TMP_Text TitleText;
    public TMP_Text DescText;
    public int[] connectedResearch;

    public void UpdateUI()
    {
        TitleText.text = $"{ReseachTree.reseachTree.ResearchName[id]}";
        DescText.text = $"{ReseachTree.reseachTree.ResearchDesc[id]}\nCost: 1 RP";

        GetComponent<Image>().color = ReseachTree.reseachTree.isbuyed[id] > 0 ? Color.white : ReseachTree.reseachTree.ResearchPoint > 0 ? Color.green : Color.red;

        foreach (var connectedResearchId in connectedResearch)
        {
            bool isUnlocked = ReseachTree.reseachTree.isbuyed[id] > 0;
            bool canBePurchased = ReseachTree.reseachTree.ResearchPoint > 0 && ReseachTree.reseachTree.isbuyed[connectedResearchId] == 0;

            if (connectedResearchId >= 0 && connectedResearchId < ReseachTree.reseachTree.ResearchList.Count)
            {
                ReseachTree.reseachTree.ResearchList[connectedResearchId].gameObject.SetActive(isUnlocked);
            }

            if (connectedResearchId >= 0 && connectedResearchId < ReseachTree.reseachTree.Connectionlist.Count)
            {
                GameObject connection = ReseachTree.reseachTree.Connectionlist[connectedResearchId];
                Image connectionImage = connection.GetComponent<Image>();

                if (isUnlocked)
                {
                    connection.SetActive(true); // Activer la connexion
                    if (ReseachTree.reseachTree.isbuyed[connectedResearchId] > 0)
                    {
                        connectionImage.color = Color.black; // Liaison entre deux technologies acquises
                    }
                    else if (canBePurchased)
                    {
                        connectionImage.color = Color.green; // Liaison entre une technologie acquise et une technologie qui peut être achetée
                    }
                    else
                    {
                        connectionImage.color = Color.red; // Liaison entre une technologie acquise et une technologie qui ne peut pas être achetée
                    }
                }
                else
                {
                    connection.SetActive(false); // Désactiver la liaison
                }
            }
        }
    }

    public void Buy()
    {
        if (ReseachTree.reseachTree.ResearchPoint < 1 || ReseachTree.reseachTree.isbuyed[id] == 1) return;

        ReseachTree.reseachTree.ResearchPoint -= 1;
        ReseachTree.reseachTree.isbuyed[id] = 1;
        ReseachTree.reseachTree.UpdateAllResearchUI();
    }
}
