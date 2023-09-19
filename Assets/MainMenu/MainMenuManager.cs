using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private const string INVENTORY_SCENE = "InventoryScene";

    public void LoadInventoryScene()
    {
        SceneManager.LoadScene(INVENTORY_SCENE);
    }
}
