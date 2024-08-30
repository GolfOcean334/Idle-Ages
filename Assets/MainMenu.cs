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
        SaveUnits();
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

    public void ReturnMap()
    {
        SaveResources();
        SaveUnits();
        SceneManager.LoadScene("MapScene");
    }

    public void ReturnUnitsCreation()
    {
        SaveResources();
        SaveUnits();
        SceneManager.LoadScene("CreationUnitsScene");
    }
    
    public void ReturnTechnoTree()
    {
        SaveResources();
        SaveUnits();
        SceneManager.LoadScene("ResearchTreeMenu");
    }

    public void ReturnInventory()
    {
        SaveResources();
        SaveUnits();
        SceneManager.LoadScene("InventoryScene");
    }

    public void ReturnForge()
    {
        SaveResources();
        SaveUnits();
        SceneManager.LoadScene("ForgeScene");
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
