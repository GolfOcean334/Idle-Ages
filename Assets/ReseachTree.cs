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
        isbuyed = new int[37];
        //LoadGameState();
        ResearchPoint = 1000000;

        ResearchName = new[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30"
            
    };
        ResearchDesc = new[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30"
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
            connection.gameObject.SetActive(false); // Désactiver toutes les connexions au début
        }

        // Assigner les IDs de recherche
        for (var i = 0; i < ResearchList.Count; i++)
        {
            ResearchList[i].id = i;
        }

        // Définir connectedResearch correctement
        ResearchList[0].connectedResearch = new[] { 1 };
        ResearchList[1].connectedResearch = new[] { 2, 3, 4, 5, 7};
        ResearchList[2].connectedResearch = new[] { 8, 9 , 35};
        ResearchList[3].connectedResearch = new[] { 6 };
        ResearchList[4].connectedResearch = new[] { 25 };
        ResearchList[5].connectedResearch = new[] { 27 };
        ResearchList[6].connectedResearch = new[] { 17,18,20 };
        ResearchList[7].connectedResearch = new[] { 21,19,22,23};
        ResearchList[8].connectedResearch = new[] { 10 };
        ResearchList[9].connectedResearch = new[] { 11, 12,13, 14 };
        ResearchList[10].connectedResearch = new[] { 36 };
        ResearchList[11].connectedResearch = new[] { 33 };
        ResearchList[12].connectedResearch = new[] {15};
        ResearchList[13].connectedResearch = new[] { 32};
        ResearchList[14].connectedResearch = new[] { 31 };
        //ResearchList[15].connectedResearch = new[] { };
        //ResearchList[16].connectedResearch = new[] { };
        //ResearchList[17].connectedResearch = new[] { };
        ResearchList[18].connectedResearch = new[] { 16 };
        ResearchList[19].connectedResearch = new[] { 34 };
        //ResearchList[20].connectedResearch = new[] { };
        //ResearchList[21].connectedResearch = new[] {};
        ResearchList[22].connectedResearch = new[] { 30 };
        ResearchList[23].connectedResearch = new[] { 24 };
        //ResearchList[24].connectedResearch = new[] { };
        ResearchList[25].connectedResearch = new[] { 26 };
        //ResearchList[26].connectedResearch = new[] { };
        ResearchList[27].connectedResearch = new[] { 28,29 };
        //ResearchList[28].connectedResearch = new[] { };
        //ResearchList[29].connectedResearch = new[] { };
        //ResearchList[30].connectedResearch = new[] { };



        UpdateAllResearchUI();
    }

    public void UpdateAllResearchUI()
    {
        foreach (var recherche in ResearchList)
        {
            recherche.UpdateUI();
        }
    }

    //public void SaveGameState()
    //{
    //    PlayerPrefs.SetInt("ResearchPoint", ResearchPoint);
    //    for (int i = 0; i < isbuyed.Length; i++)
    //    {
    //        PlayerPrefs.SetInt("isbuyed_" + i, isbuyed[i]);
    //    }
    //    PlayerPrefs.Save();
    //}

    //public void LoadGameState()
    //{
    //    ResearchPoint = PlayerPrefs.GetInt("ResearchPoint", 100);
    //    for (int i = 0; i < isbuyed.Length; i++)
    //    {
    //        isbuyed[i] = PlayerPrefs.GetInt("isbuyed_" + i, 0);
    //    }
    //}
}
