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

    public int resource1 = 0;
    public int resource2 = 0;
    public int resource3 = 0;
    public int resource4 = 0;

    private int resources1PerSecond = 5;
    private int resources2PerSecond = 5;
    private int resources3PerSecond = 5;
    private int resources4PerSecond = 5;

    private readonly int maxResource = 500000000;

    void Start()
    {
        LoadResources();
        CalculateOfflineEarnings();
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
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveResources();
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

    void CalculateOfflineEarnings()
    {
        long lastLogoutTime = Convert.ToInt64(PlayerPrefs.GetString("lastLogoutTime", DateTime.Now.ToBinary().ToString()));
        DateTime previousDateTime = DateTime.FromBinary(lastLogoutTime);
        TimeSpan timeElapsed = DateTime.Now - previousDateTime;

        int resourcesEarned1 = Mathf.FloorToInt((float)timeElapsed.TotalSeconds * resources1PerSecond);
        int resourcesEarned2 = Mathf.FloorToInt((float)timeElapsed.TotalSeconds * resources2PerSecond);
        int resourcesEarned3 = Mathf.FloorToInt((float)timeElapsed.TotalSeconds * resources3PerSecond);
        int resourcesEarned4 = Mathf.FloorToInt((float)timeElapsed.TotalSeconds * resources4PerSecond);

        resource1 = Mathf.Min(resource1 + resourcesEarned1, maxResource);
        resource2 = Mathf.Min(resource2 + resourcesEarned2, maxResource);
        resource3 = Mathf.Min(resource3 + resourcesEarned3, maxResource);
        resource4 = Mathf.Min(resource4 + resourcesEarned4, maxResource);
    }

    void OnSceneChanged(Scene current, Scene next)
    {
        SaveResources();
    }

    public void AddPassiveResourceGeneration(ResourceType resourceType, int resourcesPerSecond)
    {
        switch (resourceType)
        {
            case ResourceType.Wood:
                resources3PerSecond += resourcesPerSecond;
                break;
            case ResourceType.Stone:
                resources2PerSecond += resourcesPerSecond;
                break;
            case ResourceType.Food:
                resources1PerSecond += resourcesPerSecond;
                break;
        }
    }
}
