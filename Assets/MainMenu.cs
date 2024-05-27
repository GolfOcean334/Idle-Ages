using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Variable statique pour stocker le nom de la scène précédente
    private static string previousScene;

    public void ReturnGame()
    {
        SaveResources();
        SceneManager.LoadScene(previousScene);
    }

    public void ReturnSetting()
    {
        // Stocker le nom de la scène actuelle avant de charger la scène des paramètres
        previousScene = SceneManager.GetActiveScene().name;
        SaveUnits();
        SaveResources();
        SceneManager.LoadScene("SettingScene");
    }

    public void QuitGame()
    {
        SaveUnits();
        SaveResources();
        Application.Quit();
    }

    // Méthode privée pour sauvegarder les ressources
    private void SaveResources()
    {
        GameObject resourcesManagerObject = GameObject.Find("ResourcesManager");
        if (resourcesManagerObject != null)
        {
            if (resourcesManagerObject.TryGetComponent<ResourcesManager>(out var resourcesManager))
            {
                resourcesManager.SaveResources();
            }
        }
    }

    // Méthode privée pour sauvegarder les Units
    private void SaveUnits()
    {
        GameObject UnitsManagerObject = GameObject.Find("UnitsManager");
        if (UnitsManagerObject != null)
        {
            if (UnitsManagerObject.TryGetComponent<UnitsManager>(out var UnitsManager))
            {
                UnitsManager.SaveUnits();
            }
        }
    }
}
