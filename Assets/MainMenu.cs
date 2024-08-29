using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    // Variable statique pour stocker le nom de la sc�ne pr�c�dente
    private static string previousScene;

    public void ReturnGame()
    {
        playerStats.Save();
        SceneManager.LoadScene(previousScene);
    }

    public void ReturnSetting()
    {
        // Stocker le nom de la sc�ne actuelle avant de charger la sc�ne des param�tres
        previousScene = SceneManager.GetActiveScene().name;
        playerStats.Save();
        SceneManager.LoadScene("SettingScene");
    }

    public void ReturnMap()
    {
        playerStats.Save();
        SceneManager.LoadScene("MapScene");
    }

    public void ReturnUnitsCreation()
    {
        playerStats.Save();
        SceneManager.LoadScene("CreationUnitsScene");
    }
    
    public void ReturnTechnoTree()
    {
        playerStats.Save();
        SceneManager.LoadScene("ResearchTreeMenu");
    }

    public void QuitGame()
    {
        playerStats.Save();
        Application.Quit();
    }
}
