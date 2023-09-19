using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Represents a collection of Inventory_Items
/// </summary>
public class Inventory_Controller : MonoBehaviour
{
    private const int MAX_INVENTORY_SLOTS = 20;

    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private TextMeshProUGUI goldLabel;
    [SerializeField] private int rowSize = 5;
    public int gold { get; private set; } = 0;

    private List<Inventory_Slot> inventorySlots = new List<Inventory_Slot>(MAX_INVENTORY_SLOTS);


    void Start()
    {
        for (int i = 0; i < inventorySlots.Capacity; i++)
        {
            Inventory_Slot newSlot = new Inventory_Slot();
            inventorySlots.Add(newSlot);
        }

        GenerateInventory();
    }

    public Inventory_Slot GetSlotAtIndex(int index)
    {
        if (index > -1 && index < inventorySlots.Count)
        {
            return inventorySlots[index];
        }

        return null;
    }

    public void AddGold(int value)
    {
        gold += value;
        UpdateGold();
    }

    public void SubtractGold(int value)
    {
        gold -= value;
        UpdateGold();
    }

    /// <summary>
    /// Attempts to add newItem to the inventory. Will try to add to an existing stack if possible, otherwise will use the first available slot.
    /// </summary>
    /// <returns>True if newItem was added, otherwise returns false</returns>
    public bool AddItem(Inventory_Item newItem)
    {
        int firstAvailable;
        //first, try to add newItem to an existing stack
        for (int i = 0; i < inventorySlots.Count; i ++)
        {
            if (inventorySlots[i].isOccupied)
            {
                if (inventorySlots[i].item.GetItemID() == newItem.GetItemID())
                {
                    if (inventorySlots[i].stackSize < newItem.GetMaxStackSize())
                    {
                        if (inventorySlots[i].TryAddItem(newItem))
                        {
                            return true;
                        }
                    }
                }
            }
        }

        //if no existing or valid stack, try first available slot
        firstAvailable = GetFirstAvailableSlotIndex();
        if (firstAvailable > -1)
        {
            inventorySlots[firstAvailable].TryAddItem(newItem);
            return true;
        }

        return false;
    }

    public Inventory_Item RemoveItemAtIndex(int index)
    {
        if (index > -1 && index < inventorySlots.Count)
        {
            Inventory_Item removedItem = inventorySlots[index].TryRemoveItem();
            return removedItem;
        }

        return null;
    }

    /// <summary>
    /// Will attempt to swap the items at srcIndex and destIndex. Will simply move the item if no item exists at destIndex
    /// </summary>
    /// <returns>True if successful</returns>
    public bool SwapInventorySlots(int srcIndex, int destIndex)
    {
        Inventory_Item srcItem = inventorySlots[srcIndex].item;
        int srcCount = inventorySlots[srcIndex].stackSize;
        Inventory_Item destItem;
        int destCount;

        if (!inventorySlots[destIndex].isOccupied)
        {
            inventorySlots[destIndex].TryAddMultipleItems(srcItem, srcCount);
            inventorySlots[srcIndex].TryRemoveAllItems();

            return true;
        }
        else
        {
            if (inventorySlots[destIndex].TryAddMultipleItems(srcItem, srcCount))
            {
                inventorySlots[srcIndex].TryRemoveAllItems();
            }
            else
            {
                destItem = inventorySlots[destIndex].item;
                destCount = inventorySlots[destIndex].stackSize;

                inventorySlots[destIndex].TryRemoveAllItems();
                inventorySlots[destIndex].TryAddMultipleItems(srcItem, srcCount);

                inventorySlots[srcIndex].TryRemoveAllItems();
                inventorySlots[srcIndex].TryAddMultipleItems(destItem, destCount);
            }

            return true;
        }
    }

    /// <summary>
    /// Returns the index of the first available inventory slot or -1 if no available slots.
    /// </summary>
    private int GetFirstAvailableSlotIndex()
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (!inventorySlots[i].isOccupied)
            {
                return i;
            }
        }

        return -1;
    }

    private void UpdateGold()
    {
        if (goldLabel)
        {
            goldLabel.text = gold.ToString();
        }
    }

    /// <summary>
    /// Generates a graphical representation of the inventory
    /// </summary>
    private void GenerateInventory()
    {
        float slotSize = 105.0f;
        float xStart = slotSize * -2.0f;
        float yStart = slotSize + (slotSize / 2.0f);
        float xOffset = xStart;
        float yOffset = yStart;

        for (int i = 0; i < MAX_INVENTORY_SLOTS; i++)
        {
            GameObject newSlot = Instantiate(slotPrefab, transform.GetChild(0));
            Inventory_SlotRenderer slotRenderer = newSlot.GetComponent<Inventory_SlotRenderer>();
            RawImage iconImage = slotRenderer.iconImage;
            TextMeshProUGUI stackLabel = slotRenderer.stackSizeLabel;

            slotRenderer.SetInventoryIndex(i);

            inventorySlots[i].SetIcon(iconImage);
            inventorySlots[i].SetStackLabel(stackLabel);

            newSlot.GetComponent<RectTransform>().anchoredPosition = new Vector2(xOffset, yOffset);

            if ((i + 1) % rowSize == 0)
            {
                xOffset = xStart;
                yOffset -= slotSize;
            }
            else
            {
                xOffset += slotSize;
            }
        }
    }
}
