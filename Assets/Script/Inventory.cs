using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Button inventoryButton;   // ������ ��� ��������/�����
    public GameObject inventoryPanel; // ������ ��������� (UI Panel)
    public GameObject cellPrefab;      // ������ ����������
    public Transform gridParent;       // ������ � Grid Layout Group

    public int width = 10;
    public int height = 6;
    private bool[,] grid; // true = �������

    void Start()
    {
        grid = new bool[width, height];

        // ��������� ������ �� ������
        if (inventoryButton != null)
            inventoryButton.onClick.AddListener(ToggleInventory);
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                GameObject cell = Instantiate(cellPrefab, gridParent);
                cell.name = $"Cell_{x}_{y}";
            }
        }
    }

    // ³������/������� ��������
    public void ToggleInventory()
    {
        if (inventoryPanel != null)
        {
            bool isActive = inventoryPanel.activeSelf;
            inventoryPanel.SetActive(!isActive);
            Debug.Log("�������� " + (isActive ? "�������" : "�������"));
        }
    }

    // �������� �� ����� ��������� �������
    public bool CanPlaceItem(int itemWidth, int itemHeight, int x, int y)
    {
        if (x + itemWidth > width || y + itemHeight > height) return false;

        for (int i = 0; i < itemWidth; i++)
        {
            for (int j = 0; j < itemHeight; j++)
            {
                if (grid[x + i, y + j]) return false; // ��� �������
            }
        }
        return true;
    }

    // ��������� ��������
    public void PlaceItem(int itemWidth, int itemHeight, int x, int y)
    {
        if (!CanPlaceItem(itemWidth, itemHeight, x, y))
        {
            Debug.Log("�� ����� ��������� �������!");
            return;
        }

        for (int i = 0; i < itemWidth; i++)
        {
            for (int j = 0; j < itemHeight; j++)
            {
                grid[x + i, y + j] = true;
            }
        }
        Debug.Log("������� ��������!");
    }
}