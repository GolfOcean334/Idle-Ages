using UnityEngine;
using System.Collections;
using TMPro;

public class ResourcesManager : MonoBehaviour
{
    // R�f�rences aux textes UI pour afficher les ressources
    [SerializeField] private TextMeshProUGUI resource1Text;
    [SerializeField] private TextMeshProUGUI resource2Text;
    [SerializeField] private TextMeshProUGUI resource3Text;
    [SerializeField] private TextMeshProUGUI resource4Text;

    // Compteurs pour les ressources
    public int resource1 = 0;
    public int resource2 = 0;
    public int resource3 = 0;
    public int resource4 = 0;

    // Vitesse de gain des ressources
    private readonly int resourcesPerSecond = 5;

    void Start()
    {
        // D�marrer la coroutine pour g�n�rer des ressources
        StartCoroutine(GenerateResources());
    }

    // Coroutine pour g�n�rer des ressources
    IEnumerator GenerateResources()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            // Ajouter des ressources chaque seconde
            resource1 += resourcesPerSecond;
            resource2 += resourcesPerSecond;
            resource3 += resourcesPerSecond;
            resource4 += resourcesPerSecond;

            // Mettre � jour les textes UI
            UpdateResourceTexts();
        }
    }

    // M�thode pour mettre � jour les textes UI
    void UpdateResourceTexts()
    {
        resource1Text.text = "Wood: " + resource1;
        resource2Text.text = "Stone: " + resource2;
        resource3Text.text = "Food: " + resource3;
        resource4Text.text = "Gemme: " + resource4;
    }
}
