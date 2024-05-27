using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void ReturnGame()
    {
        SceneManager.LoadScene("CreationUnitsScene");
    }

    public void ReturnSetting()
    {
        SceneManager.LoadScene("SettingScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
