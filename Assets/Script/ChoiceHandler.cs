using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoiceHandler : MonoBehaviour
{
    [SerializeField] private SubtitleSystem subtitleSystem;
    [SerializeField] private GoStartDialog mapMover;

    // Викликається кнопкою "Я згідний"
    public void OnAccept()
    {
        string nodeName = mapMover.GetCurrentNodeName();

        switch (nodeName)
        {
            case "Road5":
                SceneManager.LoadScene("BanditHouse");
                break;

            case "LowerIntersection":
                SceneManager.LoadScene("BanditCrossroads");
                break;

            case "TradingHouse":
                // Тут пізніше зробимо квест з листом
                Debug.Log("Квест з листом прийнято!");
                break;
        }
    }

    // Викликається кнопкою "Не згідний"
    public void OnDecline()
    {
        // Просто ховаємо панель вибору
        subtitleSystem.HideChoicePanel();
    }
}