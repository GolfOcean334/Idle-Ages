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
        ResearchPoint = 200;

        isbuyed = new int[32];
        ResearchName = new[] {
            "Feu maîtrisé",
            "Fabrication d'outils en pierre",
            "Fabrication d'armes",
            "Navigation rudimentaire",
            "Technologie d'abattage d'arbres améliorée",
            "Outils de taille de pierre spécialisés",
            "Techniques de chasse",
            "Construction de structure simples",
            "Guerrier",
            "Lancier",
            "Construction de boucliers rudimentaires",
            "Formation de groupes de chasseurs-guerriers",
            "Développement de signaux de communication",
            "Techniques de camouflage",
            "Fabrication de pointes de flèches améliorées",
            "Armée préhistorique",
            "Cavalier",
            "Techniques de pêche",
            "Domestication des animaux",
            "Poterie rudimentaire",
            "Techniques de conservation des aliments",
            "Élevage de plantes comestibles",
            "Réseaux de routes",
            "Technique de collecte de l'eau",
            "Agriculture primitive",
            "Système de poulies pour le transport du bois",
            "Technologie de coupe du bois sous l'eau",
            "Techniques d'extraction minière",
            "Systèmes de grappins pour l'extraction de pierre",
            "Mines de silex",
            "Antiquité",
            "Médecine précoce"
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
            "30",
            "31"
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
        ResearchList[1].connectedResearch = new[] { 2, 3, 4, 5, 7 };
        ResearchList[2].connectedResearch = new[] { 8, 9 };
        ResearchList[3].connectedResearch = new[] { 6 };
        ResearchList[4].connectedResearch = new[] { 25 };
        ResearchList[5].connectedResearch = new[] { 27 };
        ResearchList[6].connectedResearch = new[] { 17, 18, 20 };
        ResearchList[7].connectedResearch = new[] { 19, 21, 22, 23 };
        ResearchList[8].connectedResearch = new[] { 10 };
        ResearchList[9].connectedResearch = new[] { 11, 12, 13, 14 };
        ResearchList[12].connectedResearch = new[] { 15 };
        ResearchList[18].connectedResearch = new[] { 16 };
        ResearchList[21].connectedResearch = new[] { 31 };
        ResearchList[22].connectedResearch = new[] { 30 };
        ResearchList[23].connectedResearch = new[] { 24 };
        ResearchList[25].connectedResearch = new[] { 26 };
        ResearchList[27].connectedResearch = new[] { 28, 29 };

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


