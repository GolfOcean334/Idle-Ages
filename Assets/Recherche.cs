using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Recherche : MonoBehaviour
{
    public int id;
    public TMP_Text TitleText;
    public TMP_Text DescText;

    [SerializeField]
    private List<Recherche> connectedResearchObjects; // List of research objects this node is connected to
    [SerializeField]
    private List<GameObject> connectionsToOthers; // List of connection lines corresponding to the research objects

    private void Awake()
    {
        // Optionally, you can initialize lists if necessary
        if (connectedResearchObjects == null) connectedResearchObjects = new List<Recherche>();
        if (connectionsToOthers == null) connectionsToOthers = new List<GameObject>();
    }

    public void UpdateUI()
    {
        // Ensure that the research ID is valid within both arrays.
        if (id < 0 || id >= ReseachTree.reseachTree.ResearchName.Length || id >= ReseachTree.reseachTree.ResearchDesc.Length)
        {
            Debug.LogError($"Invalid research ID: {id}. ResearchName or ResearchDesc array is out of bounds.");
            return;
        }

        // Update the title and description
        TitleText.text = $"{ReseachTree.reseachTree.ResearchName[id]}";
        DescText.text = $"{ReseachTree.reseachTree.ResearchDesc[id]}\nCost: 1 RP";

        // Update the color of the research item based on its purchase status
        UpdateResearchColor();

        // Update visibility and connections
        UpdateVisibilityAndConnections();
    }

    private void UpdateResearchColor()
    {
        // Update the color based on purchase status or availability
        Image researchImage = GetComponent<Image>();

        if (ReseachTree.reseachTree.isbuyed[id] > 0)
        {
            researchImage.color = Color.white;
        }
        else
        {
            researchImage.color = CanBePurchased() ? Color.green : Color.red;
        }
    }

    private void UpdateVisibilityAndConnections()
    {
        // Visibility based on whether the current research item is purchased
        bool isPurchased = ReseachTree.reseachTree.isbuyed[id] > 0;

        // Update visibility of connected research objects and their connections
        for (int i = 0; i < connectedResearchObjects.Count; i++)
        {
            if (i >= connectionsToOthers.Count)
            {
                Debug.LogWarning($"Mismatch in list sizes for {gameObject.name}. connectedResearchObjects.Count = {connectedResearchObjects.Count}, connectionsToOthers.Count = {connectionsToOthers.Count}");
                continue;
            }

            var connectedResearch = connectedResearchObjects[i];
            var connection = connectionsToOthers[i];

            // Make the current research object visible if the current research item is purchased
            connectedResearch.gameObject.SetActive(isPurchased);

            // Update visibility of the connection lines
            connection.SetActive(isPurchased);

            // Update connection color
            connection.GetComponent<Image>().color = GetConnectionColor(connectedResearch.id);
        }
    }

    private bool CanBeVisible()
    {
        // Root nodes should always be visible
        if (id == 0) return true;

        // Return true if the research is purchased or if it can be purchased (i.e., prerequisites are met)
        return ReseachTree.reseachTree.isbuyed[id] > 0 || (CanBePurchased() && ArePrerequisitesMet());
    }

    private bool ArePrerequisitesMet()
    {
        // Check if all prerequisites are met
        foreach (var prerequisiteResearch in connectedResearchObjects)
        {
            if (ReseachTree.reseachTree.isbuyed[prerequisiteResearch.id] == 0)
            {
                return false;
            }
        }
        return true;
    }

    private Color GetConnectionColor(int connectedResearchId)
    {
        if (ReseachTree.reseachTree.isbuyed[connectedResearchId] > 0)
        {
            return Color.black; // Connected research is purchased
        }
        else if (CanBePurchased())
        {
            return Color.red; // Not purchasable
        }
        else
        {
            return Color.green; // Connected research can be purchased
            
        }
    }

    public bool CanBePurchased()
    {
        return ReseachTree.reseachTree.isbuyed[id] == 0 && ReseachTree.reseachTree.ResearchPoint > 0;
    }

    public void Buy()
    {
        if (!CanBePurchased()) return;

        ReseachTree.reseachTree.ResearchPoint -= 1;
        //playerStats.AddResearchPoint(-1);
        ReseachTree.reseachTree.isbuyed[id] = 1;
        ReseachTree.reseachTree.UpdateAllResearchUI();

        UpdateProduction();

        // After purchase, update the UI to reflect the changes
        UpdateUI();
    }

    private void UpdateProduction()
    {
        var resourceManager = FindObjectOfType<ResourcesManager>();

        if (resourceManager == null)
        {
            Debug.LogWarning("ResourcesManager not found.");
            return;
        }

        if (id == 0 || id == 1 || id == 2 || id == 6 || id == 17 || id == 18 || id == 20 || id == 21 || id == 24)
        {
            resourceManager.IncreaseResource1Production();
        }

        if (id == 1 || id == 27 || id == 28 || id == 29)
        {
            resourceManager.IncreaseResource2Production();
        }

        if (id == 4 || id == 25 || id == 26)
        {
            resourceManager.IncreaseResource3Production();
        }
    }
}
