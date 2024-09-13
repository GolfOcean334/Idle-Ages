using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Recherche : MonoBehaviour
{
    public int id;
    public TMP_Text TitleText;
    public TMP_Text DescText;
    public List<Recherche> connectedResearchObjects; // List of research objects this node is connected to
    public List<GameObject> connectionsToOthers; // List of connection lines corresponding to the research objects

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

        // Update visibility and color of connected research and connections
        for (int i = 0; i < connectedResearchObjects.Count; i++)
        {
            // Check if corresponding connection exists
            if (i >= connectionsToOthers.Count)
            {
                Debug.LogWarning($"Mismatch in list sizes for {gameObject.name}. connectedResearchObjects.Count = {connectedResearchObjects.Count}, connectionsToOthers.Count = {connectionsToOthers.Count}");
                continue; // Skip if there is a mismatch in the list sizes
            }

            var connectedResearch = connectedResearchObjects[i];
            var connection = connectionsToOthers[i];

            // Connection should always be active
            connection.SetActive(true);

            // Set the connection color based on whether the connected research is purchased
            connection.GetComponent<Image>().color = ReseachTree.reseachTree.isbuyed[connectedResearch.id] > 0
                ? Color.black  // Connected research is purchased
                : Color.green; // Connected research is not purchased
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
        //playerStats.AddResearchPoint(-1);
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

        // After purchase, update the UI to reflect the changes
        UpdateUI();
    }
}
