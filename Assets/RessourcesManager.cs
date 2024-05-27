using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class ResourcesManager : MonoBehaviour
{
    // Références aux textes UI pour afficher les ressources
    [SerializeField] private TextMeshProUGUI resource1Text;
    [SerializeField] private TextMeshProUGUI resource2Text;
    [SerializeField] private TextMeshProUGUI resource3Text;
    [SerializeField] private TextMeshProUGUI resource4Text;

    // Compteurs pour les ressources
    public int resource1 = 0;
    public int resource2 = 0;
    public int resource3 = 0;
    public int resource4 = 0;

    // Vitesse de gain des ressources
    private readonly int resourcesPerSecond = 5;

    void Start()
    {
        // Charger les ressources sauvegardées
        LoadResources();

        // Démarrer la coroutine pour générer des ressources
        StartCoroutine(GenerateResources());

        // S'abonner à l'événement de changement de scène
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    // Méthode appelée quand le jeu est fermé ou la scène est changée
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

    // Coroutine pour générer des ressources
    IEnumerator GenerateResources()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            // Ajouter des ressources chaque seconde
            resource1 += resourcesPerSecond;
            resource2 += resourcesPerSecond;
            resource3 += resourcesPerSecond;
            resource4 += resourcesPerSecond;

            // Mettre à jour les textes UI
            UpdateResourceTexts();
        }
    }

    // Méthode pour mettre à jour les textes UI
    void UpdateResourceTexts()
    {
        resource1Text.text = "Wood: " + resource1;
        resource2Text.text = "Stone: " + resource2;
        resource3Text.text = "Food: " + resource3;
        resource4Text.text = "Gemme: " + resource4;
    }

    // Méthode pour sauvegarder les ressources
    public void SaveResources()
    {
        PlayerPrefs.SetInt("Resource1", resource1);
        PlayerPrefs.SetInt("Resource2", resource2);
        PlayerPrefs.SetInt("Resource3", resource3);
        PlayerPrefs.SetInt("Resource4", resource4);
        PlayerPrefs.Save();
    }

    // Méthode pour charger les ressources
    void LoadResources()
    {
        resource1 = PlayerPrefs.GetInt("Resource1", 0);
        resource2 = PlayerPrefs.GetInt("Resource2", 0);
        resource3 = PlayerPrefs.GetInt("Resource3", 0);
        resource4 = PlayerPrefs.GetInt("Resource4", 0);
        UpdateResourceTexts();
    }

    // Méthode appelée lors du changement de scène
    void OnSceneChanged(Scene current, Scene next)
    {
        SaveResources();
    }
}
