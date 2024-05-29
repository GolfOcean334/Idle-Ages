using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReseachTree : MonoBehaviour
{
    public static ReseachTree reseachTree;

    public int[] isbuyed;
    public string[] ResearchName;
    public string[] ResearchDesc;

    public List<GameObject> Connectionlist;
    public GameObject ConnectionHolder;

    public List<Recherche> ResearchList;
    public GameObject ResearchHolder;

    public int ResearchPoint;

    private void Awake()
    {
        reseachTree = this;
    }

    private void Start()
    {
        ResearchPoint = 20;

        isbuyed = new int[13];
        ResearchName = new[] {
            "Maitrise du feu",
            "Fabrication d'outil en pierre",
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
            "zdz"
        };
        ResearchDesc = new[] {
            "Là où tout commence",
            "Outil rudimentaire",
            "10% production de minerais",
            "10% production de minerais",
            "zdz",
            "zdz",
            "zdz",
            "zdz",
            "zdz",
            "zdz",
            "zdz",
            "zdz",
            "zdz"
        };

        // Initialiser les listes
        ResearchList = new List<Recherche>();
        Connectionlist = new List<GameObject>();

        // Créer un GameObject "DummyConnection"
        GameObject dummyConnection = new GameObject("DummyConnection");
        dummyConnection.transform.SetParent(ConnectionHolder.transform, false);
        Connectionlist.Add(dummyConnection);

        // Ajoutez les composants Recherche des enfants de ResearchHolder
        foreach (var recherche in ResearchHolder.GetComponentsInChildren<Recherche>())
        {
            ResearchList.Add(recherche);
        }

        // Ajoutez les objets connexion des enfants de ConnectionHolder
        foreach (Transform connection in ConnectionHolder.transform)
        {
            Connectionlist.Add(connection.gameObject);
        }

        // Assigner les IDs de recherche
        for (var i = 0; i < ResearchList.Count; i++)
        {
            ResearchList[i].id = i;
        }

        // Définir connectedResearch correctement
        ResearchList[0].connectedResearch = new[] { 1 };
        ResearchList[1].connectedResearch = new[] { 2, 3, 4, 5 };
        ResearchList[3].connectedResearch = new[] { 6, 7 };
        ResearchList[4].connectedResearch = new[] { 8 };
        ResearchList[5].connectedResearch = new[] { 9 };
        ResearchList[8].connectedResearch = new[] { 10 };
        ResearchList[9].connectedResearch = new[] { 11, 12 };

        UpdateAllResearchUI();
    }

    public void UpdateAllResearchUI()
    {
        foreach (var recherche in ResearchList)
        {
            recherche.UpdateUI();
        }
    }
}
