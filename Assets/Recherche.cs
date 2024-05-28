using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ReseachTree;

public class Recherche : MonoBehaviour
{
    public int id;

    public TMP_Text TitleText;
    public TMP_Text DescText;

    public int[] connectedResearch;

    public void UpdateUI()
    {
        TitleText.text = $"{reseachTree.ResearchName[id]}";
        DescText.text = $"{reseachTree.ResearchDesc[id]}\nCost:{reseachTree.ResearchPoint}/1 RP";

        GetComponent<Image>().color = reseachTree.isbuyed[id] > 0 ? Color.white : reseachTree.ResearchPoint > 1 ? Color.green : Color.red ;
    
        foreach (var connectedResearch in connectedResearch)
        {
            ReseachTree.reseachTree.ResearchList[connectedResearch].gameObject.SetActive(reseachTree.isbuyed[id] > 0);
            ReseachTree.reseachTree.Connectionlist[connectedResearch].SetActive(reseachTree.isbuyed[id] > 0);
        }

    }

    public void Buy()
    {
        if (reseachTree.ResearchPoint < 1 || reseachTree.isbuyed[id] == 1) return;
        reseachTree.ResearchPoint -= 1;
        reseachTree.isbuyed[id] = 1;
        reseachTree.UpdateAllResearchUI();

    }
}
