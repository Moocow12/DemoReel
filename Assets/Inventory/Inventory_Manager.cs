using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Driver for the Inventory program
/// </summary>
public class Inventory_Manager : MonoBehaviour
{
    private const string MAIN_MENU_SCENE = "MainMenu";

    [SerializeField] private Inventory_ShopButton shopButtonPrefab;
    [SerializeField] private Inventory_Controller inventory;
    [SerializeField] private RectTransform dragIcon;
    [SerializeField] private List<Inventory_Item> shopItems = new List<Inventory_Item>();
    private List<Inventory_ShopButton> shopButtons = new List<Inventory_ShopButton>();
    private Inventory_SlotRenderer hoveredSlot = null;
    private Inventory_SlotRenderer selectedSlot = null;
    private bool dragEnabled = false;
    private Vector2 mouseDownPos = Vector2.zero;

    void Start()
    {
        GenerateShop();

        inventory.AddGold(200);
    }

    private void Update()
    {
        ProcessCursor();
        ProcessMouseClicks();
        ProcessMouseMovement();
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(MAIN_MENU_SCENE);
    }

    private void ProcessCursor()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        hoveredSlot = null;

        //if we're hovering over an Inventory_SlotRenderer, it becomes our hoveredSlot
        foreach (RaycastResult result in results)
        {
            if (result.gameObject.GetComponent<Inventory_SlotRenderer>())
            {
                hoveredSlot = result.gameObject.GetComponent<Inventory_SlotRenderer>();
            }
        }
    }

    private void ProcessMouseClicks()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ProcessLeftMouseDown();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            ProcessLeftMouseUp();
        }
    }

    private void ProcessLeftMouseDown()
    {
        if (!selectedSlot && hoveredSlot)
        {
            if (inventory.GetSlotAtIndex(hoveredSlot.inventoryIndex).isOccupied)
            {
                //begin drag
                selectedSlot = hoveredSlot;
                dragEnabled = true;
                mouseDownPos = new Vector2(Input.mousePosition.x - (Screen.width / 2.0f), Input.mousePosition.y - (Screen.height / 2.0f));
            }
        }
    }

    private void ProcessLeftMouseUp()
    {
        if (selectedSlot && hoveredSlot)
        {
            if (selectedSlot == hoveredSlot)
            {
                //register this as a click
                OnInventorySlotLeftClick(selectedSlot.inventoryIndex);
            }
            else
            {
                //register this as the end of a drag
                inventory.SwapInventorySlots(selectedSlot.inventoryIndex, hoveredSlot.inventoryIndex);
            }
        }
        selectedSlot = null;
        dragEnabled = false;
    }

    private void ProcessMouseMovement()
    {
        if (selectedSlot && dragEnabled)
        {
            Vector2 dragPos = new Vector2(Input.mousePosition.x - (Display.displays[0].renderingWidth / 2.0f), Input.mousePosition.y - (Display.displays[0].renderingHeight / 2.0f));
            float dragMagnitude = (mouseDownPos - dragPos).magnitude;
            if (dragMagnitude > 20.0f)
            {
                //make dragIcon follow the cursor
                dragIcon.gameObject.SetActive(true);
                dragIcon.GetComponent<RawImage>().texture = selectedSlot.iconImage.texture;
                dragIcon.anchoredPosition = dragPos;
            }
        }
        else
        {
            dragIcon.gameObject.SetActive(false);
        }
    }

    private void OnInventorySlotLeftClick(int index)
    {
        //sell item to shop
        if (inventory.GetSlotAtIndex(index).isOccupied)
        {
            Inventory_Item removedItem = inventory.RemoveItemAtIndex(index);
            if (removedItem)
            {
                inventory.AddGold(removedItem.GetGoldValue());
            }
        }
    }

    private void OnShopButtonClick(Inventory_Item item)
    {
        //buy item from shop
        if (inventory.gold >= item.GetGoldValue())
        {
            if (inventory.AddItem(item))
            {
                inventory.SubtractGold(item.GetGoldValue());
            }
        }
    }

    private void GenerateShop()
    {
        float yOffset = 200.0f;
        for (int i = 0; i < shopItems.Count; i++)
        {
            Inventory_ShopButton newButton = Instantiate(shopButtonPrefab, transform.GetChild(0));
            newButton.SetItem(shopItems[i]);
            newButton.GetComponent<Button>().onClick.AddListener(delegate { OnShopButtonClick(newButton.GetItem()); });
            newButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, yOffset);
            shopButtons.Add(newButton);
            yOffset -= 100.0f;
        }
    }
}
