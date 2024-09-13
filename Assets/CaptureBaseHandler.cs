using UnityEngine;
using System.Collections.Generic;
using System.IO;

// Classe principale pour g�rer les bases captur�es
public class CaptureBaseHandler : MonoBehaviour
{
    [SerializeField] private GameObject capturedBasePrefab;
    [SerializeField] private ResourcesManager resourcesManager;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private GameObject allBasesParent;

    private List<CapturedBaseData> capturedBasesList = new List<CapturedBaseData>();

    private void Start()
    {
        LoadCapturedBases();
        RestoreCapturedBases();
    }

    public void CaptureBase(GameObject baseToCapture, ResourceType resource, int resourceAmount, int resourcesPerSecond)
    {
        // Convertir la position en espace local avant d'instancier la base captur�e
        Vector3 localPosition = allBasesParent.transform.InverseTransformPoint(baseToCapture.transform.position);
        Destroy(baseToCapture);

        resourcesManager.AddResources(resource, resourceAmount);
        Debug.Log("Ressources ajout�es: " + resourceAmount + " de type " + resource);

        resourcesManager.AddResourcesPerSecond(resource, resourcesPerSecond);
        Debug.Log("Ressources par seconde ajout�es: " + resourcesPerSecond + " de type " + resource);

        ReseachTree.reseachTree.ResearchPoint += 1;
        // Enregistrer la position locale
        CapturedBaseData capturedBaseData = new CapturedBaseData(localPosition, resource, resourceAmount, resourcesPerSecond);
        capturedBasesList.Add(capturedBaseData);
        SaveCapturedBases();
        RestoreCapturedBases();
    }



    void SaveCapturedBases()
    {
        string json = JsonUtility.ToJson(new CapturedBaseDataList { CapturedBases = capturedBasesList });
        File.WriteAllText(Application.persistentDataPath + "/capturedBases.json", json);
    }


    // M�thode pour charger les bases captur�es
    void LoadCapturedBases()
    {
        string path = Application.persistentDataPath + "/capturedBases.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            CapturedBaseDataList loadedData = JsonUtility.FromJson<CapturedBaseDataList>(json);
            capturedBasesList = loadedData.CapturedBases;
        }
    }


    // Exemple de v�rification des positions
    void RestoreCapturedBases()
    {
        foreach (var baseData in capturedBasesList)
        {
            // Convertir la position locale en position world space
            Vector3 worldPosition = allBasesParent.transform.TransformPoint(baseData.Position);
            Debug.Log("Restoring base at position: " + worldPosition); // V�rifiez les positions restaur�es
            GameObject newBase = Instantiate(capturedBasePrefab, worldPosition, Quaternion.identity, allBasesParent.transform);
            resourcesManager.AddResources(baseData.Resource, baseData.ResourceAmount);
            resourcesManager.AddResourcesPerSecond(baseData.Resource, baseData.ResourcesPerSecond);
        }
    }




}

// Classe pour stocker les donn�es des bases captur�es
[System.Serializable]
public class CapturedBaseData
{
    public Vector3 Position;
    public ResourceType Resource; // Assurez-vous que ResourceType est d�fini quelque part dans votre projet
    public int ResourceAmount;
    public int ResourcesPerSecond;

    public CapturedBaseData(Vector3 position, ResourceType resource, int resourceAmount, int resourcesPerSecond)
    {
        Position = position;
        Resource = resource;
        ResourceAmount = resourceAmount;
        ResourcesPerSecond = resourcesPerSecond;
    }
}

// Classe pour envelopper la liste des donn�es des bases captur�es
[System.Serializable]
public class CapturedBaseDataList
{
    public List<CapturedBaseData> CapturedBases;
}
