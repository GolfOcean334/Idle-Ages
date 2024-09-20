using UnityEngine;

public class MonoBehaviourSingleton : MonoBehaviour
{
    private static MonoBehaviourSingleton instance;
    public static MonoBehaviourSingleton Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject singletonObject = new GameObject("MonoBehaviourSingleton");
                instance = singletonObject.AddComponent<MonoBehaviourSingleton>();
                DontDestroyOnLoad(singletonObject);
            }
            return instance;
        }
    }
}
