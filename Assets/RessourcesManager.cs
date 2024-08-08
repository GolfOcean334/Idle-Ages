using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class ResourcesManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resource1Text;
    [SerializeField] private TextMeshProUGUI resource2Text;
    [SerializeField] private TextMeshProUGUI resource3Text;
    [SerializeField] private TextMeshProUGUI resource4Text;

    public int resource1;
    public int resource2;
    public int resource3;
    public int resource4;

    private int resources1PerSecond = 0;
    private int resources2PerSecond = 0;
    private int resources3PerSecond = 0;
    private int resources4PerSecond = 0;

    private readonly int maxResource = 500000;

    void Start()
    {
        LoadResources();
        StartCoroutine(GenerateResources());
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    void Update()
    {
        UpdateResourceTexts();
    }

    void OnApplicationQuit()
    {
        SaveResources();
        SaveLogoutTime();
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveResources();
            SaveLogoutTime();
        }
    }

    IEnumerator GenerateResources()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            // Ajouter des ressources chaque seconde
            resource1 = Mathf.Min(resource1 + resources1PerSecond, maxResource);
            resource2 = Mathf.Min(resource2 + resources2PerSecond, maxResource);
            resource3 = Mathf.Min(resource3 + resources3PerSecond, maxResource);
            resource4 = Mathf.Min(resource4 + resources4PerSecond, maxResource);

            Debug.Log("Ressources générées: \n Ressource1 : " + resource1 + " Ressource2 : " + resource2 + " Ressource3 : " + resource3 + " Ressource4 : " + resource4);

            UpdateResourceTexts();
        }
    }

    public void UpdateResourceTexts()
    {
        resource1Text.text = FormatResource(resource1);
        resource2Text.text = FormatResource(resource2);
        resource3Text.text = FormatResource(resource3);
        resource4Text.text = FormatResource(resource4);
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
                resource3 = Mathf.Min(resource3 + resourceAmount, maxResource);
                break;
            case ResourceType.Stone:
                resource2 = Mathf.Min(resource2 + resourceAmount, maxResource);
                break;
            case ResourceType.Food:
                resource1 = Mathf.Min(resource1 + resourceAmount, maxResource);
                break;
        }
        UpdateResourceTexts();
    }

    public void AddResourcesPerSecond(ResourceType resource, int resourcesPerSecond)
    {
        switch (resource)
        {
            case ResourceType.Wood:
                resources3PerSecond += resourcesPerSecond;
                Debug.Log("Ressources par seconde pour Wood: " + resources3PerSecond);
                break;
            case ResourceType.Stone:
                resources2PerSecond += resourcesPerSecond;
                Debug.Log("Ressources par seconde pour Stone: " + resources2PerSecond);
                break;
            case ResourceType.Food:
                resources1PerSecond += resourcesPerSecond;
                Debug.Log("Ressources par seconde pour Food: " + resources1PerSecond);
                break;
        }
    }

    void LoadResources()
    {
        resource1 = PlayerPrefs.GetInt("resource1", 0);
        resource2 = PlayerPrefs.GetInt("resource2", 0);
        resource3 = PlayerPrefs.GetInt("resource3", 0);
        resource4 = PlayerPrefs.GetInt("resource4", 0);
    }

    public void SaveResources()
    {
        PlayerPrefs.SetInt("resource1", resource1);
        PlayerPrefs.SetInt("resource2", resource2);
        PlayerPrefs.SetInt("resource3", resource3);
        PlayerPrefs.SetInt("resource4", resource4);
    }

    void SaveLogoutTime()
    {
        PlayerPrefs.SetString("lastLogoutTime", DateTime.Now.ToBinary().ToString());
    }

    void OnSceneChanged(Scene current, Scene next)
    {
        SaveResources();
    }
}
