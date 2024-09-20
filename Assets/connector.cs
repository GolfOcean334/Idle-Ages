using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateIfOtherActive : MonoBehaviour
{
    public GameObject connector; // L'objet dont l'état d'activation dépend d'un autre
    public GameObject recherche; // L'objet de référence pour vérifier son état

    void Update()
    {
        if (recherche != null && connector == null)
        {
            connector.SetActive(true);
        }
    }
}
