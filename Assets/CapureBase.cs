using UnityEngine;

public class CaptureBaseHandler : MonoBehaviour
{
    [SerializeField] private GameObject capturedBasePrefab;

    public void CaptureBase(GameObject baseToCapture, ResourcesManager resourcesManager, ResourceType resource, int resourceAmount, int resourcesPerSecond)
    {
        // Instancier l'asset de la base capturée
        GameObject newBase = Instantiate(capturedBasePrefab, baseToCapture.transform.position, baseToCapture.transform.rotation, baseToCapture.transform.parent);
        Destroy(baseToCapture);

        // Ajouter l'addition de ressources
        resourcesManager.AddResources(resource, resourceAmount);
        Debug.Log("Ressources ajoutées: " + resourceAmount + " de type " + resource);

        // Mettre à jour les ressources par seconde cumulées
        resourcesManager.AddResourcesPerSecond(resource, resourcesPerSecond);
        Debug.Log("Ressources par seconde ajoutées: " + resourcesPerSecond + " de type " + resource);


        // Masquer ou désactiver les éléments UI de la base
        baseToCapture.GetComponent<BaseButtonHandler>().HideInfoPanel();
    }
}
