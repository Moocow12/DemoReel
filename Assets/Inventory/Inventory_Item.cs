using UnityEngine;

/// <summary>
/// Base item for the Inventory program
/// </summary>
public class Inventory_Item : ScriptableObject
{
    [SerializeField] private int itemID = 0;
    [SerializeField] private int goldValue = 0;
    [SerializeField] private int maxStackSize = 1;
    [SerializeField] private Texture2D iconTexture;

    public int GetItemID()
    {
        return itemID;
    }

    public int GetGoldValue()
    {
        return goldValue;
    }

    public int GetMaxStackSize()
    {
        return maxStackSize;
    }

    public Texture2D GetIconTexture()
    {
        return iconTexture;
    }
}
