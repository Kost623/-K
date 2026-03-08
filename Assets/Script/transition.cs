using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenInventory : MonoBehaviour
{
    public void OpenInv()
    {
        SceneManager.LoadScene("InventoryScene", LoadSceneMode.Additive);
    }
}
