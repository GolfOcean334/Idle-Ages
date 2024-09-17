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
        InitializeResearchTree();
        InitializeConnections();
        AssignResearchIDs();
        UpdateAllResearchUI();
    }

    private void InitializeResearchTree()
    {
        isbuyed = new int[37];
        ResearchPoint = 10000;

        ResearchName = new[]
        {
            "Feu maîtrisé", "Fabrication d'outils en pierre", "Fabrication d'armes",
            "Navigation rudimentaire", "Technologie d'abattage d'arbres améliorée",
            "Outils de taille de pierre spécialisés", "Techniques de chasse",
            "Construction de structure simples", "Guerrier", "Lancier",
            "Construction de boucliers rudimentaires", "Formation de groupes de chasseurs-guerriers",
            "Développement de signaux de communication", "Techniques de camouflage",
            "Fabrication de pointes de flèches améliorées", "Armée préhistorique",
            "Cavalier", "Techniques de pêche", "Domestication des animaux",
            "Poterie rudimentaire", "Techniques de conservation des aliments",
            "Élevage de plantes comestibles", "Réseaux de routes", "Technique de collecte de l'eau",
            "Agriculture primitive", "Système de poulies pour le transport du bois",
            "Technologie de coupe du bois sous l'eau", "Techniques d'extraction minière",
            "Systèmes de grappins pour l'extraction de pierre", "Mines de silex",
            "Antiquité", "Invention de l'écriture", "Roue", "Métallurgie",
            "Construction navale", "Astronomie ancienne", "Cartographie primitive"
        };

        ResearchDesc = new[]
        {
            "+1 production nourriture", "+1 production nourriture \n +1 production pierre", "+1 production nourriture",
            "+1 production de bois", "+1 production de bois", "+1 production nourriture", "+1 production nourriture",
            "+1 production de bois", "+1 production nourriture", "+1 production pierre", "+1 production de bois",
            "+1 production nourriture", "+1 production pierre", "+1 production nourriture", "+1 production nourriture",
            "+1 production de bois", "+1 production nourriture", "+1 production de bois", "+1 production pierre",
            "+1 production nourriture", "+1 production nourriture", "+1 production de bois", "+1 production pierre",
            "+1 production de bois", "+1 production nourriture", "+1 production de bois", "+1 production pierre",
            "+1 production pierre", "+1 production pierre", "+1 production de bois", "+1 production de nourriture",
            "+1 production pierre", "+1 production de nourriture", "+1 production de nourriture",
            "+1 production de nourriture", "+1 production de nourriture", "+1 production de nourriture"
        };

        ResearchList = new List<Recherche>();
    }

    private void InitializeConnections()
    {
        Connectionlist = new List<GameObject>();

        foreach (Transform connection in ConnectionHolder.transform)
        {
            Connectionlist.Add(connection.gameObject);
            connection.gameObject.SetActive(false); // Deactivate all connections at start
        }
    }

    private void AssignResearchIDs()
    {
        foreach (var recherche in ResearchHolder.GetComponentsInChildren<Recherche>())
        {
            ResearchList.Add(recherche);
        }

        for (var i = 0; i < ResearchList.Count; i++)
        {
            ResearchList[i].id = i;
        }
    }

    public void UpdateAllResearchUI()
    {
        foreach (var recherche in ResearchList)
        {
            recherche.UpdateUI();
        }
    }
}
