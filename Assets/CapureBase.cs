using UnityEngine;

public class CaptureBaseHandler : MonoBehaviour
{
    [SerializeField] private GameObject capturedBasePrefab;

    public void CaptureBase(GameObject baseToCapture, ResourcesManager resourcesManager, ResourceType resource, int resourceAmount)
    {
        GameObject newBase = Instantiate(capturedBasePrefab, baseToCapture.transform.position, baseToCapture.transform.rotation, baseToCapture.transform.parent);
        Destroy(baseToCapture);

        // Ajouter l'addition de ressources
        //resourcesManager.AddResources(resource, resourceAmount);

        // Masquer ou désactiver les éléments UI de la base
        baseToCapture.GetComponent<BaseButtonHandler>().HideInfoPanel();
    }
}
