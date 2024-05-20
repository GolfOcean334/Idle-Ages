using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneNameToLoad;
    void Start()
    {
        
    }

    public void changeScene()
    {
        SceneManager.LoadScene(sceneNameToLoad);
    }
}
