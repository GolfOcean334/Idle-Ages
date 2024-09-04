using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private PlayerStats playerStats;
    private static string previousScene;


    public void OnPointerClick(PointerEventData eventData)
    {
        // Log pour vérifier que le clic fonctionne
        Debug.Log("Button clicked: " + eventData.pointerCurrentRaycast.gameObject.name);

        // Exécutez des actions en fonction du bouton cliqué
        if (eventData.pointerCurrentRaycast.gameObject.name == "ReturnGameButton")
        {
            ReturnGame();
        }
        else if (eventData.pointerCurrentRaycast.gameObject.name == "ReturnSettingButton")
        {
            ReturnSetting();
        }
        else if (eventData.pointerCurrentRaycast.gameObject.name == "ReturnMapButton")
        {
            ReturnMap();
        }
        else if (eventData.pointerCurrentRaycast.gameObject.name == "ReturnUnitsCreationButton")
        {
            ReturnUnitsCreation();
        }
        else if (eventData.pointerCurrentRaycast.gameObject.name == "ReturnTechnoTreeButton")
        {
            ReturnTechnoTree();
        }
        else if (eventData.pointerCurrentRaycast.gameObject.name == "QuitGameButton")
        {
            QuitGame();
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
