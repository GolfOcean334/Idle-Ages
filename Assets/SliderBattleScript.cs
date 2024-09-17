using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SliderBattleScript : MonoBehaviour
{
    [SerializeField] private Slider sliderT1;
    [SerializeField] private Slider sliderT2;
    [SerializeField] private Slider sliderT3;

    [SerializeField] private TextMeshProUGUI sliderTextT1;
    [SerializeField] private TextMeshProUGUI sliderTextT2;
    [SerializeField] private TextMeshProUGUI sliderTextT3;

    [SerializeField] private PlayerStats playerStats;

    void Start()
    {
        // Initialiser les sliders avec les valeurs maximales des unités disponibles
        sliderT1.maxValue = playerStats.UnitsT1;
        sliderT2.maxValue = playerStats.UnitsT2;
        sliderT3.maxValue = playerStats.UnitsT3;

        sliderT1.onValueChanged.AddListener((v) => {
            sliderTextT1.text = v.ToString("0");
        });

        sliderT2.onValueChanged.AddListener((v) => {
            sliderTextT2.text = v.ToString("0");
        });

        sliderT3.onValueChanged.AddListener((v) => {
            sliderTextT3.text = v.ToString("0");
        });
    }

    public int GetSelectedUnitsT1()
    {
        return (int)sliderT1.value;
    }

    public int GetSelectedUnitsT2()
    {
        return (int)sliderT2.value;
    }

    public int GetSelectedUnitsT3()
    {
        return (int)sliderT3.value;
    }
}
