using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory_ShopButton : MonoBehaviour
{
    [SerializeField] private RawImage itemImage;
    [SerializeField] private TextMeshProUGUI itemNameLabel;
    [SerializeField] private TextMeshProUGUI itemCostLabel;
    [SerializeField] private Inventory_Item item;

    public void SetItem(Inventory_Item newItem)
    {
        item = newItem;
        itemImage.texture = item.GetIconTexture();
        itemNameLabel.text = item.name;
        itemCostLabel.text = item.GetGoldValue().ToString();
    }

    public Inventory_Item GetItem()
    {
        return item;
    }
}
