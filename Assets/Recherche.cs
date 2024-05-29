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

            // Vérifiez si l'index est valide avant de l'utiliser
            if (connectedResearchId >= 0 && connectedResearchId < ReseachTree.reseachTree.ResearchList.Count)
            {
                ReseachTree.reseachTree.ResearchList[connectedResearchId].gameObject.SetActive(isUnlocked);
            }

            // Assurez-vous que l'index est dans les limites de Connectionlist
            if (connectedResearchId > 0 && connectedResearchId < ReseachTree.reseachTree.Connectionlist.Count)
            {
                ReseachTree.reseachTree.Connectionlist[connectedResearchId].SetActive(isUnlocked);
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
