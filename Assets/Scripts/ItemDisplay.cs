using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    public Image iconImage;

    // M�thode pour mettre � jour l'ic�ne
    public void SetItem(Item item)
    {
        if (iconImage != null)
        {
            iconImage.sprite = item.icon;
        }
    }
}
