using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory_SlotRenderer : MonoBehaviour
{
    public Image frame;
    public RawImage iconImage;
    public TextMeshProUGUI stackSizeLabel;
    public int inventoryIndex { get; private set; } = -1;

    [SerializeField] private Color highlightColor;

    public void SetInventoryIndex(int index)
    {
        inventoryIndex = index;
    }

    public void OnMouseEnter()
    {
        frame.color = highlightColor;
    }

    public void OnMouseExit()
    {
        frame.color = Color.white;
    }
}
