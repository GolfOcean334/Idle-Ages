using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    private static string previousScene;

    private void Update()
    {
        playerStats.Save();
    }
    public void ReturnGame()
    {
        SceneManager.LoadScene(previousScene);
    }

    public void ReturnSetting()
    {
        // Stocker le nom de la scène actuelle avant de charger la scène des paramètres
        previousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("SettingScene");
    }

    public void ReturnMap()
    {
        SceneManager.LoadScene("MapScene");
    }

    public void ReturnUnitsCreation()
    {
        SceneManager.LoadScene("CreationUnitsScene");
    }
    
    public void ReturnTechnoTree()
    {
        SceneManager.LoadScene("ResearchTreeMenu");
    }

    public void ReturnInventory()
    {

        SceneManager.LoadScene("InventoryScene");
    }

    public void ReturnForge()
    {

        SceneManager.LoadScene("ForgeScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
