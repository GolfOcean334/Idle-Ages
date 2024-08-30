using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Recherche : MonoBehaviour
{
    public int id;
    public TMP_Text TitleText;
    public TMP_Text DescText;
    public List<Recherche> connectedResearchObjects; // Set in Inspector

    public void UpdateUI()
    {
        // Update the title and description
        TitleText.text = $"{ReseachTree.reseachTree.ResearchName[id]}";
        DescText.text = $"{ReseachTree.reseachTree.ResearchDesc[id]}\nCost: 1 RP";

        // Update the color of the research item based on its purchase status
        GetComponent<Image>().color = ReseachTree.reseachTree.isbuyed[id] > 0
            ? Color.white
            : CanBePurchased()
                ? Color.green
                : Color.red;

        // Toggle visibility of connected research and connections
        foreach (var connectedResearch in connectedResearchObjects)
        {
            bool isPurchased = ReseachTree.reseachTree.isbuyed[id] > 0;
            connectedResearch.gameObject.SetActive(isPurchased);

            // Toggle visibility of connection line
            int connectionIndex = connectedResearchObjects.IndexOf(connectedResearch);
            if (connectionIndex >= 0 && connectionIndex < ReseachTree.reseachTree.Connectionlist.Count)
            {
                GameObject connection = ReseachTree.reseachTree.Connectionlist[connectionIndex];
                connection.SetActive(isPurchased);

                if (isPurchased)
                {
                    // Update the color of the connection line
                    connection.GetComponent<Image>().color = ReseachTree.reseachTree.isbuyed[connectedResearch.id] > 0
                        ? Color.black // Both are purchased
                        : Color.green; // Connected item is purchasable
                }
            }
        }
    }

    public bool CanBePurchased()
    {
        return ReseachTree.reseachTree.isbuyed[id] == 0 && ReseachTree.reseachTree.ResearchPoint > 0;
    }

    public void Buy()
    {
        if (ReseachTree.reseachTree.ResearchPoint < 1 || ReseachTree.reseachTree.isbuyed[id] == 1) return;

        ReseachTree.reseachTree.ResearchPoint -= 1;
        ReseachTree.reseachTree.isbuyed[id] = 1;
        ReseachTree.reseachTree.UpdateAllResearchUI();

        // Example: Check if a specific research increases production
        if (id == 0 || id == 1 || id == 2 || id == 6 || id == 17 || id == 18 || id == 20 || id == 21 || id == 24)
        {
            FindObjectOfType<ResourcesManager>().IncreaseResource1Production();
        }

        if (id == 1 || id == 27 || id == 28 || id == 29)
        {
            FindObjectOfType<ResourcesManager>().IncreaseResource2Production();
        }

        if (id == 4 || id == 25 || id == 26)
        {
            FindObjectOfType<ResourcesManager>().IncreaseResource3Production();
        }
    }
}
