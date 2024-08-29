using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateIfOtherActive : MonoBehaviour
{
    public GameObject connector; // L'objet dont l'�tat d'activation d�pend d'un autre
    public GameObject recherche; // L'objet de r�f�rence pour v�rifier son �tat

    void Update()
    {
        if (recherche != null && connector == null)
        {
            connector.SetActive(true);
        }
    }
}
