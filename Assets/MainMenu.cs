using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Variable statique pour stocker le nom de la sc�ne pr�c�dente
    private static string previousScene;

    public void ReturnGame()
    {
        SaveResources();
        SceneManager.LoadScene(previousScene);
    }

    public void ReturnSetting()
    {
        // Stocker le nom de la sc�ne actuelle avant de charger la sc�ne des param�tres
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

    // M�thode priv�e pour sauvegarder les ressources
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

    // M�thode priv�e pour sauvegarder les Units
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
