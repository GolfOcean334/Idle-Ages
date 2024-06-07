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

    private readonly int resourcesPerSecond = 5;
    private readonly int maxResource = 500;

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
            resource1 = Mathf.Min(resource1 + resourcesPerSecond, maxResource);
            resource2 = Mathf.Min(resource2 + resourcesPerSecond, maxResource);
            resource3 = Mathf.Min(resource3 + resourcesPerSecond, maxResource);
            resource4 = Mathf.Min(resource4 + resourcesPerSecond, maxResource);
        }
    }

    void UpdateResourceTexts()
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

    public void SaveResources()
    {
        PlayerPrefs.SetInt("Resource1", resource1);
        PlayerPrefs.SetInt("Resource2", resource2);
        PlayerPrefs.SetInt("Resource3", resource3);
        PlayerPrefs.SetInt("Resource4", resource4);

        PlayerPrefs.SetString("LastSaveTime", DateTime.Now.ToBinary().ToString());
        PlayerPrefs.Save();
    }

    void LoadResources()
    {
        resource1 = PlayerPrefs.GetInt("Resource1", 0);
        resource2 = PlayerPrefs.GetInt("Resource2", 0);
        resource3 = PlayerPrefs.GetInt("Resource3", 0);
        resource4 = PlayerPrefs.GetInt("Resource4", 0);
    }

    void CalculateOfflineEarnings()
    {
        string lastSaveTimeString = PlayerPrefs.GetString("LastSaveTime", DateTime.Now.ToBinary().ToString());
        DateTime lastSaveTime = DateTime.FromBinary(Convert.ToInt64(lastSaveTimeString));
        TimeSpan timeSinceLastSave = DateTime.Now - lastSaveTime;

        int secondsSinceLastSave = (int)timeSinceLastSave.TotalSeconds;
        int earnedResources = secondsSinceLastSave * resourcesPerSecond;

        resource1 = Mathf.Min(resource1 + earnedResources, maxResource);
        resource2 = Mathf.Min(resource2 + earnedResources, maxResource);
        resource3 = Mathf.Min(resource3 + earnedResources, maxResource);
        resource4 = Mathf.Min(resource4 + earnedResources, maxResource);
    }

    void OnSceneChanged(Scene current, Scene next)
    {
        SaveResources();
    }
}
