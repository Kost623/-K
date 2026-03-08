using UnityEngine;
using UnityEngine.SceneManagement;

public class CloseInventory : MonoBehaviour
{
    public void Close()
    {
        SceneManager.UnloadSceneAsync("InventoryScene");
    }
}
