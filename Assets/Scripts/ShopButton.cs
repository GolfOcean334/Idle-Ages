using UnityEngine;

public class LoseResourcesButton : MonoBehaviour
{
    public ResourcesManager resourcesManager;
    public int resourceType;
    public int amount;

    public void OnButtonClick()
    {
        resourcesManager.LoseResources(resourceType, amount);
    }
}
