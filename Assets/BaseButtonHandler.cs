using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaseButtonHandler : MonoBehaviour
{
    public int power;
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    private static GameObject currentInfoPanel;
    private static TextMeshProUGUI currentInfoText;
    private static BaseButtonHandler currentBase;

    void Start()
    {
        if (currentInfoPanel == null)
        {
            currentInfoPanel = infoPanel;
            currentInfoText = infoText;
        }

        // Assigner la méthode OnClick à l'événement du bouton
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void Initialize(int power, GameObject infoPanel, TextMeshProUGUI infoText)
    {
        this.power = power;
        this.infoPanel = infoPanel;
        this.infoText = infoText;
    }

    void OnClick()
    {
        if (currentBase == this)
        {
            // Hide the panel if the same base is clicked again
            currentInfoPanel.SetActive(false);
            currentBase = null;
        }
        else
        {
            // Show the panel with the power information
            currentInfoPanel.SetActive(true);
            currentInfoText.text = "Puissance de la base : " + power;
            currentBase = this;
        }
    }
}
