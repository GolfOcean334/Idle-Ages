using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LoadingBarManager : MonoBehaviour
{
    public RectTransform loadingBar;
    public Button startButton;
    public TextMeshProUGUI UnitsT1Text;
    public int UnitsT1 = 0;
    private bool isLoading = false;
    private float loadingTime = 5f; // Temps de chargement en secondes
    private float maxBarWidth = 300f; // Largeur maximale de la barre

    void Start()
    {
        startButton.onClick.AddListener(OnStartButtonClick);
        UpdateUnitsT1Text();
    }

    void OnStartButtonClick()
    {
        if (!isLoading)
        {
            StartCoroutine(LoadOverTime());
        }
    }

    IEnumerator LoadOverTime()
    {
        isLoading = true;
        float elapsedTime = 0f;
        float initialBarWidth = 0f;

        while (elapsedTime < loadingTime)
        {
            elapsedTime += Time.deltaTime;
            float newWidth = Mathf.Lerp(initialBarWidth, maxBarWidth, elapsedTime / loadingTime);
            loadingBar.sizeDelta = new Vector2(newWidth, loadingBar.sizeDelta.y);
            yield return null;
        }

        isLoading = false;
        IncreaseUnitsT1();
    }

    void IncreaseUnitsT1()
    {
        UnitsT1 += 1;
        UpdateUnitsT1Text();
        ResetLoadingBar();
    }

    void UpdateUnitsT1Text()
    {
        UnitsT1Text.text = "UnitsT1 : " + UnitsT1;
    }

    void ResetLoadingBar()
    {
        loadingBar.sizeDelta = new Vector2(0f, loadingBar.sizeDelta.y);
    }
}
