using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        private const string INVENTORY_SCENE = "InventoryScene";

        public void LoadInventoryScene()
        {
            SceneManager.LoadScene(INVENTORY_SCENE);
        }
    }
}

