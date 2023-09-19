using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory_Slot
{
    public bool isOccupied { get; private set; } = false;
    public int stackSize { get; private set; } = 0;
    public Inventory_Item item { get; private set; } = null;

    private RawImage icon;
    private TextMeshProUGUI stackLabel;

    public void SetIcon(RawImage rawImage)
    {
        icon = rawImage;
    }

    public void SetStackLabel(TextMeshProUGUI label)
    {
        stackLabel = label;
    }

    /// <summary>
    /// Attempts to add an item to this slot. 
    /// </summary>
    /// <returns>False if newItem does not match current item, or if stack size is at max. True otherwise</returns>
    public bool TryAddItem(Inventory_Item newItem)
    {
        if (isOccupied)
        {
            if (item.GetItemID() == newItem.GetItemID())
            {
                if (stackSize < item.GetMaxStackSize())
                {
                    stackSize++;
                    UpdateStackLabel();
                    return true;
                }
            }
        }
        else
        {
            item = newItem;
            isOccupied = true;
            stackSize++;
            UpdateStackLabel();
            UpdateIcon();
            return true;
        }

        return false;
    }

    /// <summary>
    /// Attempts to add multiple items to this slot.
    /// </summary>
    /// <returns>False if newItem does not match current item or if the count would exceed maximum stack size. True otherwise</returns>
    public bool TryAddMultipleItems(Inventory_Item newItem, int count)
    {
        if (isOccupied)
        {
            if (item.GetItemID() == newItem.GetItemID())
            {
                if (stackSize + count < item.GetMaxStackSize())
                {
                    stackSize += count;
                    UpdateStackLabel();
                    return true;
                }
            }
        }
        else
        {
            if (count <= newItem.GetMaxStackSize())
            {
                item = newItem;
                isOccupied = true;
                stackSize = count;
                UpdateStackLabel();
                UpdateIcon();
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Attempts to remove an item from this slot.
    /// </summary>
    /// <returns>False if there is no item. True otherwise</returns>
    public Inventory_Item TryRemoveItem()
    {
        if (isOccupied)
        {
            if (stackSize > 1)
            {
                stackSize--;
                UpdateStackLabel();
                return item;
            }
            else
            {
                Inventory_Item retVal = item;
                stackSize = 0;
                isOccupied = false;
                item = null;

                UpdateStackLabel();
                UpdateIcon();

                return retVal;
            }
        }

        return null;
    }


    /// <summary>
    /// Attempts to remove all items from this slot.
    /// </summary>
    public bool TryRemoveAllItems()
    {
        if (isOccupied)
        {
            stackSize = 0;
            isOccupied = false;
            item = null;

            UpdateStackLabel();
            UpdateIcon();

            return true;
        }

        return false;
    }

    private void UpdateStackLabel()
    {
        if (stackLabel)
        {
            if (stackSize > 1)
            {
                stackLabel.gameObject.SetActive(true);
                stackLabel.text = stackSize.ToString();
            }
            else
            {
                stackLabel.gameObject.SetActive(false);
            }
        }
    }

    private void UpdateIcon()
    {
        if (icon)
        {
            if (isOccupied)
            {
                icon.gameObject.SetActive(true);
                icon.texture = item.GetIconTexture();
            }
            else
            {
                icon.gameObject.SetActive(false);
            }
        }
    }
}
