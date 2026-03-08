using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class SubtitleSystem : MonoBehaviour
{
    [Header("UI елементи")]
    [SerializeField] private GameObject subtitlePanel;
    [SerializeField] private TextMeshProUGUI subtitleText;
    [SerializeField] private Image heroPortrait;
    [SerializeField] private Image panelBackground; // ← додай це поле і перетягни Image з SubtitlePanel

    [Header("Налаштування")]
    [SerializeField] private float displayTime = 3f;
    [SerializeField] private float typeSpeed = 0.05f;
    [SerializeField] private Sprite defaultPortrait;

    private Coroutine currentCoroutine;

    private Dictionary<string, string> nodeComments = new Dictionary<string, string>()
    {
        { "n1",  "Починаємо... Треба бути обережним." },
        { "n2",  "Це місце виглядає знайомо." },
        { "n3",  "Тут щось нечисте." },
        { "n4",  "Дорога стає небезпечнішою." },
        { "n5",  "Зверху видно далі." },
        { "n6",  "Чую якийсь шум..." },
        { "n7",  "Майже там." },
        { "n8",  "Ось воно. Кінець шляху." },
    };

    void Start()
    {
        if (defaultPortrait != null)
            heroPortrait.sprite = defaultPortrait;

        subtitlePanel.SetActive(true);
        HideAll(); // ← ховаємо все на старті
    }

    public void ShowComment(string nodeName)
    {
        if (!nodeComments.ContainsKey(nodeName)) return;

        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);

        currentCoroutine = StartCoroutine(DisplaySubtitle(nodeComments[nodeName]));
    }

    IEnumerator DisplaySubtitle(string text)
    {
        ShowAll(); // ← показуємо все разом
        subtitleText.text = "";

        foreach (char letter in text)
        {
            subtitleText.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }

        yield return new WaitForSeconds(displayTime);

        HideAll(); // ← ховаємо все разом
    }

    // ✅ Показує панель + текст + портрет
    void ShowAll()
    {
        SetAlpha(panelBackground, 0.7f); // напівпрозорий фон
        subtitleText.gameObject.SetActive(true);
        heroPortrait.gameObject.SetActive(true);
    }

    // ✅ Ховає панель (прозора) + текст + портрет
    void HideAll()
    {
        SetAlpha(panelBackground, 0f); // повністю прозорий фон
        subtitleText.gameObject.SetActive(false);
        heroPortrait.gameObject.SetActive(false);
    }

    // Змінює прозорість Image
    void SetAlpha(Image image, float alpha)
    {
        if (image == null) return;
        Color c = image.color;
        c.a = alpha;
        image.color = c;
    }
}