//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class SceneController : MonoBehaviour
//{
//    public Inventory inventory;

//    private void OnEnable()
//    {
//        SceneManager.sceneLoaded += OnSceneLoaded;
//    }

//    private void OnDisable()
//    {
//        SceneManager.sceneLoaded -= OnSceneLoaded;
//    }

//    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
//    {
//        inventory.LoadInventory();
//    }

//    public void ChangeScene(string sceneName)
//    {
//        inventory.SaveInventory();
//        SceneManager.LoadScene(sceneName);
//    }

//    public void LoadInventoryScene()
//    {
//        // Chargez la scène d'inventaire
//        UnityEngine.SceneManagement.SceneManager.LoadScene("InventoryScene");

//        // Appelez ResetInventory après le chargement de la scène
//        Inventory.Instance.ResetInventory();
//    }
//}
