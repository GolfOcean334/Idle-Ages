using System;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class ReseachTree : MonoBehaviour
{
    public static ReseachTree reseachTree;

    private string SaveFilePath => Path.Combine(Application.persistentDataPath, "playerSave.json");

    public int[] isbuyed;
    public string[] ResearchName;
    public string[] ResearchDesc;

    public int ResearchPoint;


    public List<GameObject> Connectionlist;
    public GameObject ConnectionHolder;

    public List<Recherche> ResearchList;
    public GameObject ResearchHolder;

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

        Load();
    }

    private void InitializeResearchTree()
    {
        isbuyed = new int[37];

        ResearchPoint = 1;

        ResearchName = new[]
            {
            "Feu maetrise", "Fabrication d'outils en pierre", "Fabrication d'armes",
            "Navigation rudimentaire", "Technologie d'abattage d'arbres amelioree",
            "Outils de taille de pierre specialises", "Techniques de chasse",
            "Construction de structure simples", "Guerrier", "Lancier",
            "Construction de boucliers rudimentaires", "Formation de groupes de chasseurs-guerriers",
            "Developpement de signaux de communication", "Techniques de camouflage",
            "Fabrication de pointes de fleches ameliorees", "Armee prehistorique",
            "Cavalier", "Techniques de peche", "Domestication des animaux",
            "Poterie rudimentaire", "Techniques de conservation des aliments",
            "elevage de plantes comestibles", "Reseaux de routes", "Technique de collecte de l'eau",
            "Agriculture primitive", "Systeme de poulies pour le transport du bois",
            "Technologie de coupe du bois sous l'eau", "Techniques d'extraction miniere",
            "Systemes de grappins pour l'extraction de pierre", "Mines de silex",
            "Antiquite", "Invention de l'ecriture", "Roue", "Metallurgie",
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

        Save();
    }


    public void Save()
    {
        // Crée une instance de PlayerData avec les données actuelles
        PlayerData data = new PlayerData
        {
            ResearchPoint = ResearchPoint,
        };

        // Sérialiser les données en JSON
        string jsonData = JsonUtility.ToJson(data, true);

        // Écrire les données JSON dans un fichier
        File.WriteAllText(SaveFilePath, jsonData);

        Debug.Log("Données sauvegardées dans " + SaveFilePath);
    }

    public void Load()
    {
        if (File.Exists(SaveFilePath))
        {
            // Lire les données JSON du fichier
            string jsonData = File.ReadAllText(SaveFilePath);

            // Désérialiser les données JSON en une instance de PlayerData
            PlayerData data = JsonUtility.FromJson<PlayerData>(jsonData);

            ResearchPoint = data.ResearchPoint;
        }
    }
}