using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitsManager : MonoBehaviour
{
    public int UnitsT1 = 0;
    public TextMeshProUGUI UnitsT1Text;

    void Start()
    {
        UpdateUnitsT1Text();
    }

    public void IncreaseUnitsT1()
    {
        UnitsT1 += 1;
        UpdateUnitsT1Text();
    }

    void UpdateUnitsT1Text()
    {
        UnitsT1Text.text = "UnitsT1 : " + UnitsT1;
    }
}