using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class ResourcesManager : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;

    [SerializeField] public TextMeshProUGUI resource1Text;
    [SerializeField] public TextMeshProUGUI resource2Text;
    [SerializeField] public TextMeshProUGUI resource3Text;
    [SerializeField] public TextMeshProUGUI resource4Text;

    private readonly int maxResource = 500000;

    void Start()
    {
        playerStats.Load();
        StartCoroutine(GenerateResources());
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    void Update()
    {
        resource1Text.text = FormatResource(playerStats.resource1);
        resource2Text.text = FormatResource(playerStats.resource2);
        resource3Text.text = FormatResource(playerStats.resource3);
        resource4Text.text = FormatResource(playerStats.resource4);
    }

    void OnApplicationQuit()
    {
        playerStats.Save();
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            playerStats.Save();
        }
    }

    IEnumerator GenerateResources()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            // Ajouter des ressources chaque seconde
            playerStats.resource1 = Mathf.Min(playerStats.resource1 + playerStats.resources1PerSecond, maxResource);
            playerStats.resource2 = Mathf.Min(playerStats.resource2 + playerStats.resources2PerSecond, maxResource);
            playerStats.resource3 = Mathf.Min(playerStats.resource3 + playerStats.resources3PerSecond, maxResource);
            playerStats.resource4 = Mathf.Min(playerStats.resource4 + playerStats.resources4PerSecond, maxResource);

            Debug.Log("Ressources g�n�r�es: \n Ressource1 : " + playerStats.resource1 + " Ressource2 : " + playerStats.resource2 + " Ressource3 : " + playerStats.resource3 + " Ressource4 : " + playerStats.resource4);
        }
    }

    string FormatResource(int resource)
    {
        if (resource >= 1000000)
        {
            return (resource / 1000000f).ToString("F1") + "M";
        }
        else if (resource >= 1000)
        {
            return (resource / 1000f).ToString("F1") + "k";
        }
        else
        {
            return resource.ToString();
        }
    }

    public void AddResources(ResourceType resource, int resourceAmount)
    {
        switch (resource)
        {
            case ResourceType.Wood:
                playerStats.resource3 = Mathf.Min(playerStats.resource3 + resourceAmount, maxResource);
                break;
            case ResourceType.Stone:
                playerStats.resource2 = Mathf.Min(playerStats.resource2 + resourceAmount, maxResource);
                break;
            case ResourceType.Food:
                playerStats.resource1 = Mathf.Min(playerStats.resource1 + resourceAmount, maxResource);
                break;
        }
    }

    public void AddResourcesPerSecond(ResourceType resource, int resourcesPerSecond)
    {
        switch (resource)
        {
            case ResourceType.Wood:
                playerStats.resources3PerSecond += resourcesPerSecond;
                Debug.Log("Ressources par seconde pour Wood: " + playerStats.resources3PerSecond);
                break;
            case ResourceType.Stone:
                playerStats.resources2PerSecond += resourcesPerSecond;
                Debug.Log("Ressources par seconde pour Stone: " + playerStats.resources2PerSecond);
                break;
            case ResourceType.Food:
                playerStats.resources1PerSecond += resourcesPerSecond;
                Debug.Log("Ressources par seconde pour Food: " + playerStats.resources1PerSecond);
                break;
        }
    }

    void OnSceneChanged(Scene current, Scene next)
    {
        playerStats.Save();
    }
    public void IncreaseResource1Production()
    {
        playerStats.resources1PerSecond = Mathf.FloorToInt(playerStats.resources1PerSecond + 1f);
    }

    public void IncreaseResource2Production()
    {
        playerStats.resources2PerSecond = Mathf.FloorToInt(playerStats.resources2PerSecond + 1f);
    }

    public void IncreaseResource3Production()
    {
        playerStats.resources3PerSecond = Mathf.FloorToInt(playerStats.resources4PerSecond + 1f);
    }

    public void IncreaseResource4Production()
    {
        playerStats.resources4PerSecond = Mathf.FloorToInt(playerStats.resources4PerSecond + 1f);
    }


}
