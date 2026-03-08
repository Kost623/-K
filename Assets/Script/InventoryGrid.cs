using UnityEngine;
using UnityEngine.UI;
public class InventoryGrid : MonoBehaviour
{
    public Image slotPrefab;
    public int width = 5;
    public int height = 4;

    void Start()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Instantiate(slotPrefab);
            }
        }
    }
}
