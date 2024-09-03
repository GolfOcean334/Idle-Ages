using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    private static string previousScene;

    public static int ScreenWidth = Screen.width;
    public static int ScreenHeight = Screen.height;

    private void Start()
    {
        if(ScreenWidth == Screen.width || ScreenHeight == Screen.height)
        {
            ScreenHeight = Screen.width - (640 * 2);
            ScreenWidth = Screen.height - (360 * 2);
            Screen.SetResolution(ScreenWidth, ScreenHeight, true);
        }
    }
    public void ReturnGame()
    {
        playerStats.Save();
        SceneManager.LoadScene(previousScene);
    }

    public void ReturnSetting()
    {
        // Stocker le nom de la scène actuelle avant de charger la scène des paramètres
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
