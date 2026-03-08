using UnityEngine;
using UnityEngine.SceneManagement;

public class LocationDiscovery : MonoBehaviour
{
    [Header("Посилання")]
    [SerializeField] private MapMover mapMover;

    [Header("Налаштування")]
    [SerializeField] private KeyCode enterKey = KeyCode.Space;

    void Update()
    {
        if (Input.GetKeyDown(enterKey))
            EnterCurrentLocation();
    }

public void EnterCurrentLocation()
    {
        string locationName = mapMover.GetCurrentNodeName();

        // ✅ Перевірка прогресу — без цього локація не відкриється
        if (!mapMover.IsNodeVisited(locationName))
        {
            Debug.Log($"🔒 Локація «{locationName}» ще не розблокована!");
            return;
        }

        switch (locationName)
        {
            case "Start":
                SceneManager.LoadScene("Start"); // ✅ старт теж має сцену

                break;
            case "Riverside_Keep":
                SceneManager.LoadScene("Scene_RiversideKeep");
                break;
            case "Cruise_island":
                SceneManager.LoadScene("Scene_CruiseIsland");
                break;
            case "Camp_Padites":
                SceneManager.LoadScene("Scene_CampPadites");
                break;
            case "Lake_Oakshire":
                SceneManager.LoadScene("Scene_LakeOakshire");
                break;
            case "Goldshire":
                SceneManager.LoadScene("Scene_Goldshire");
                break;
            case "Northshire":
                SceneManager.LoadScene("Scene_Northshire");
                break;
            case "Stormwing":
                SceneManager.LoadScene("Scene_Stormwing");
                break;
            default:
                Debug.LogWarning($"Невідома локація: {locationName}");
                break;
        }
    }
}