using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ReseachTree : MonoBehaviour
{
    public static ReseachTree reseachTree;
    private void Awake()
    {
        reseachTree = this;
    }

    public int[] isbuyed;
    public string[] ResearchName;
    public string[] ResearchDesc;

    public List<GameObject> Connectionlist;
    public GameObject ConnectionHolder;

    public List<Recherche> ResearchList;
    public GameObject ResearchHolder;

    public int ResearchPoint;

    private void Start()
    {
        ResearchPoint = 20;

        isbuyed = new int[10];
        ResearchName = new[] {
            "Maitrise du feu", 
            "fabrication d'outil en pierre", 
            "Outils de taille de pierre spécialisés", 
            "Techniques d'extraction minière",
            "zdz",
            "zdz",
            "zdz",
            "zdz",
            "zdz",
            "zdz",
            "zdz",
            "zdz",
            "zdz",
        };
        ResearchDesc = new[] { 
            "la ou tout comence", 
            "outil rudimentaire", "10% production de minerais", 
            "10% production de minerais",
            "zdz",
            "zdz",
            "zdz",
            "zdz",
            "zdz",
            "zdz",
            "zdz",
            "zdz",
            "zdz",
            "zdz",
            "zdz",
            "zdz",
        };

        foreach (var recherche in ResearchHolder.GetComponentsInChildren<Recherche>())
        {
            ResearchList.Add(recherche);
        }

        foreach (var connection in ConnectionHolder.GetComponentsInChildren<RectTransform>())
        {
            Connectionlist.Add(connection.gameObject);
        }

        for (var i = 0; i < ResearchList.Count; i++) 
        {
            ResearchList[i].id = i;
        }

        ResearchList[0].connectedResearch = new[] { 1, 2 };
        ResearchList[1].connectedResearch = new[] { 3 };
        ResearchList[2].connectedResearch = new[] { 10, 11, 12 };
        ResearchList[3].connectedResearch = new[] { 8, 9 };
        ResearchList[4].connectedResearch = new[] { 5 };
        //ResearchList[5].connectedResearch = new[] {};
        //ResearchList[6].connectedResearch = new[] {};
        //ResearchList[7].connectedResearch = new[] {};
        //ResearchList[8].connectedResearch = new[] {};
        //ResearchList[9].connectedResearch = new[] {};
        ResearchList[10].connectedResearch = new[] { 4 };
        //ResearchList[11].connectedResearch = new[] {};
        ResearchList[12].connectedResearch = new[] { 6, 7 };

        UpdateAllResearchUI();
    }

    public void UpdateAllResearchUI()
    {
        foreach(var recherche in ResearchList) recherche.UpdateUI();
    }
}