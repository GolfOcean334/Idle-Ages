using UnityEngine;

public class CaptureBaseHandler : MonoBehaviour
{
    [SerializeField] private GameObject capturedBasePrefab;

    public void CaptureBase(GameObject baseToCapture, ResourcesManager resourcesManager, ResourceType resource, int resourceAmount, int resourcesPerSecond)
    {
        // Instancier l'asset de la base captur�e
        GameObject newBase = Instantiate(capturedBasePrefab, baseToCapture.transform.position, baseToCapture.transform.rotation, baseToCapture.transform.parent);
        Destroy(baseToCapture);

        // Ajouter l'addition de ressources
        resourcesManager.AddResources(resource, resourceAmount);
        Debug.Log("Ressources ajout�es: " + resourceAmount + " de type " + resource);

        // Mettre � jour les ressources par seconde cumul�es
        resourcesManager.AddResourcesPerSecond(resource, resourcesPerSecond);
        Debug.Log("Ressources par seconde ajout�es: " + resourcesPerSecond + " de type " + resource);


        // Masquer ou d�sactiver les �l�ments UI de la base
        baseToCapture.GetComponent<BaseButtonHandler>().HideInfoPanel();
    }
}
