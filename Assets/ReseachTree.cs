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

    public int ResearchPoint;

    public List<Recherche> ResearchList;
    public GameObject ResearchHolder;

    private void Awake()
    {
        reseachTree = this;
    }

    private void Start()
    {
        isbuyed = new int[37];
        //LoadGameState();

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
    };
        ResearchDesc = new[] {
            "+1 production nourriture",
            "+1 production nourriture \n +1 production pierre",
            "+1 production nourriture",
            "3",
            "+1 production de bois",
            "5",
            "+1 production nourriture",
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
            "+1 production nourriture",
            "+1 production nourriture",
            "19",
            "+1 production nourriture",
            "+1 production nourriture",
            "22",
            "23",
            "+1 production nourriture",
            "+1 production de bois",
            "+1 production de bois",
            "+1 production pierre",
            "+1 production pierre",
            "+1 production pierre",
            "30",
        };




        // Initialize ResearchList and Connectionlist
        ResearchList = new List<Recherche>();
        Connectionlist = new List<GameObject>();

        foreach (var recherche in ResearchHolder.GetComponentsInChildren<Recherche>())
        {
            ResearchList.Add(recherche);
        }

        foreach (Transform connection in ConnectionHolder.transform)
        {
            Connectionlist.Add(connection.gameObject);
            connection.gameObject.SetActive(false); // Deactivate all connections at start
        }

        // Assign research IDs
        for (var i = 0; i < ResearchList.Count; i++)
        {
            ResearchList[i].id = i;
        }

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