using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private int index;
    private Vector3 initialImagePosition;

    [SerializeField] private TextMeshProUGUI itemCountText;
    [SerializeField] private Image itemImage;
    private Button button;
    private InventoryDisplay inventoryDisplay;

    /// <summary>Methode qui initialise l'objet</summary>
    public void Initialize(InventoryDisplay _inventoryDisplay, int _index)
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(OnClick);
        index = _index;
        inventoryDisplay = _inventoryDisplay;
        initialImagePosition = itemImage.transform.position;
    }

    public void OnClick()
    {
        inventoryDisplay.ClickSlot(index);
    }

    public void UpdateDisplay(Item _item)
    {
        if (!_item.Empty)
        {
            itemCountText.text = _item.Count.ToString();
            itemImage.sprite = _item.Data.icon;
            itemImage.color = Color.white;
            return;
        }

        itemCountText.text = "";
        itemImage.sprite = null;
        itemImage.color = new Color(0, 0, 0, 0);
    }

    #region Drag and Drop

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        inventoryDisplay.DragSlot(index);
        initialImagePosition = itemImage.transform.localPosition;
        itemImage.transform.SetParent(inventoryDisplay.transform);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        itemImage.transform.position = eventData.position;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        itemImage.transform.SetParent(transform);
        itemImage.transform.localPosition = initialImagePosition;
    }

    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        inventoryDisplay.DropOnSlot(index);
    }

    #endregion
}