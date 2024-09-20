using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    public Image iconImage;

    // Méthode pour mettre à jour l'icône
    public void SetItem(Item item)
    {
        if (iconImage != null)
        {
            iconImage.sprite = item.icon;
        }
    }
}
