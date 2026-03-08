using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Button inventoryButton;
    public GameObject inventoryPanel;

    public void OpenInventory()
    {
        // Включаємо/виключаємо панель інвентаря
        bool isActive = inventoryPanel.activeSelf;
        inventoryPanel.SetActive(!isActive);
    }
}