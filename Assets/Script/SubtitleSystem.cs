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
    [SerializeField] private Image npcPortrait;      // ✅ НОВИЙ — портрет справа
    [SerializeField] private Image panelBackground;

    [Header("Налаштування")]
    [SerializeField] private float displayTime = 3f;
    [SerializeField] private float typeSpeed = 0.05f;

    [Header("Портрети")]
    [SerializeField] private Sprite heroSprite;
    [SerializeField] private Sprite peasant1Sprite;   // селянин з Road5
    [SerializeField] private Sprite peasant2Sprite;   // селянин з LowerIntersection
    [SerializeField] private Sprite merchantSprite;   // торговець TradingHouse
    [SerializeField] private Sprite banditSprite;     // бандит

    [Header("Кнопки вибору")]
    [SerializeField] private GameObject choicePanel; // батьківський об'єкт з обома кнопками
    [SerializeField] private TextMeshProUGUI choiceText; // ← новий
// Вузли де показуються кнопки
private HashSet<string> choiceNodes = new HashSet<string>()
{
    "Road5",
    "LowerIntersection",
    "TradingHouse"
};

    private Coroutine currentCoroutine;
    private HashSet<string> visitedNodes = new HashSet<string>();

    // Який NPC говорить на кожному вузлі
    private Dictionary<string, Sprite> nodeNPC = new Dictionary<string, Sprite>();

    void Start()
    {
        // Заповнюємо після того як Unity завантажить спрайти
        // (робимо в Start щоб спрайти вже були призначені)
        nodeNPC["Road5"]             = peasant1Sprite;
        nodeNPC["LowerIntersection"] = peasant2Sprite;
        nodeNPC["TradingHouse"]      = merchantSprite;

        if (heroSprite != null)
            heroPortrait.sprite = heroSprite;

        subtitlePanel.SetActive(true);
        HideAll();
    }

    public void HideChoicePanel()
    {
    if (choicePanel != null)
        choicePanel.SetActive(false);
   }
    public void ShowComment(string nodeName)
    {
    string text;

    // Оголошуємо ПЕРЕД if блоком
    bool showChoice = choiceNodes.Contains(nodeName) && !visitedNodes.Contains(nodeName);

    if (choicePanel != null)
    {  
        choicePanel.SetActive(showChoice);
    }
        

    //  Тепер showChoice доступна тут
    if (showChoice && choiceText != null && heroThoughts.ContainsKey(nodeName))
    {
        choiceText.text = heroThoughts[nodeName];  
    }
       

    if (!visitedNodes.Contains(nodeName))
    {
        visitedNodes.Add(nodeName);
        if (!firstVisit.ContainsKey(nodeName)) return;
        text = firstVisit[nodeName];
    }
    else
    {
        if (!repeatVisit.ContainsKey(nodeName)) return;
        text = repeatVisit[nodeName];
    }

    Sprite npc = nodeNPC.ContainsKey(nodeName) ? nodeNPC[nodeName] : null;

    if (currentCoroutine != null)
        StopCoroutine(currentCoroutine);

    currentCoroutine = StartCoroutine(DisplaySubtitle(text, npc));
    }

    IEnumerator DisplaySubtitle(string text, Sprite npc)
    {
        // Показуємо NPC портрет тільки якщо він є
        if (npc != null)
        {
            npcPortrait.sprite = npc;
            npcPortrait.gameObject.SetActive(true);
        }
        else
        {
            npcPortrait.gameObject.SetActive(false);
        }

        ShowAll();
        subtitleText.text = "";

        foreach (char letter in text)
        {
            subtitleText.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }

        yield return new WaitForSeconds(displayTime);
        HideAll();
    }

    void ShowAll()
    {
        SetAlpha(panelBackground, 0.7f);
        subtitleText.gameObject.SetActive(true);
        heroPortrait.gameObject.SetActive(true);
    }

    void HideAll()
    {
        SetAlpha(panelBackground, 0f);
        subtitleText.gameObject.SetActive(false);
        heroPortrait.gameObject.SetActive(false);
        npcPortrait.gameObject.SetActive(false);
    if (choicePanel != null)
        choicePanel.SetActive(false); // ховаємо кнопки разом з панеллю
    }

    void SetAlpha(Image image, float alpha)
    {
        if (image == null) return;
        Color c = image.color;
        c.a = alpha;
        image.color = c;
    }

    // ======= ДІАЛОГИ =======
// Думки героя для кожного вузла при першому та повторному відвідуванні
    private Dictionary<string, string> firstVisit = new Dictionary<string, string>()
    {
        { "Start",             "Ось і село... Нарешті. Гаманець майже порожній — треба терміново знайти роботу." },
        { "RoadLock",          "Хтось поставив тут огорожу. Цікаво навіщо..." },
        { "RoadCourt",         "Будівля суду. Значить тут є якийсь порядок." },
        { "Crossroads",        "Перехрестя. Звідси можна піти в декілька сторін. Куди спочатку?" },
        { "Lock",              "Замок на ланцюгу. Явно щось ховають за тими воротами." },
        { "Street",            "Головна вулиця села. Люди ходять, торгують. Звичайне життя." },
        { "Road1",             "Дорога веде кудись далі. Треба бути уважним." },
        { "Road2",             "Чую скрип вітряка. Село явно живе своїм життям." },
        { "Road3",             "Тут тихіше. Менше людей. Щось мені не подобається ця тиша." },
        { "Road4",             "Довга дорога. Ноги вже втомились. Але зупинятись не можна." },
        { "Road5",             "Старий селянин хапає мене за рукав.\n— Добродію! Благаю вас! Бандит захопив мій дім! Виженіть його, прошу!" },
        { "Road6",             "Край села. Далі вже відкрита місцевість. Треба бути обережним." },
        { "EndGlobalMap",      "Далі дороги немає. Потрібно повернутись назад." },
        { "NrafCastle",        "Замок Нраф. Масивні стіни, темні вежі. Хто тут живе?" },
        { "Windmill",          "Вітряк повільно крутиться. Хтось ще обслуговує його." },
        { "Windmill1",         "Ще один вітряк. Це село явно не бідне." },
        { "TradingHouse",      "Господар підбігає до мене.\n— Мандрівнику! Доставте листа до Stormwind терміново! Добре заплачу!" },
        { "Transition",        "Якесь незвичне місце... Відчуваю що тут щось сталось раніше." },
        { "RoadBelow",         "Нижня частина села. Тут бідніші будинки." },
        { "RoadBelow1",        "Вузька стежка між будинками. Темнувато тут." },
        { "RoadBelow2",        "Забута частина села. Давно тут не прибирали." },
        { "RoadBelow3",        "Майже вийшов на інший шлях. Треба не заблукати." },
        { "RoadBelow4",        "Звідси видно верхню дорогу. Майже обійшов усе село." },
        { "LowerIntersection", "Переляканий селянин виглядає з-за рогу.\n— Пане! Тут бандит! Він погрожує всім! Розберіться з ним!" },
        { "Manor",             "Старий маєток. Колись тут жила заможна родина. Що з ними сталось?" },
    };
// Думки героя для кожного вузла при повторному відвідуванні
    private Dictionary<string, string> repeatVisit = new Dictionary<string, string>()
    {
        { "Start",             "Знову тут. Треба продовжувати справи." },
        { "RoadLock",          "Огорожа стоїть на місці. Нічого не змінилось." },
        { "RoadCourt",         "Суд все ще працює." },
        { "Crossroads",        "Знову на перехресті. Цього разу знаю куди йти." },
        { "Lock",              "Замок досі закритий." },
        { "Street",            "Вулиця жива як і раніше." },
        { "Road1",             "Знайома дорога." },
        { "Road2",             "Вітряк крутиться як завжди." },
        { "Road3",             "Вже не так лячно як вперше." },
        { "Road4",             "Знову ця довга дорога..." },
        { "Road5",             "Старий селянин дивиться на мене з надією.\n— Ну як? Ви вже розібрались з тим бандитом?" },
        { "Road6",             "Знову на краю села." },
        { "EndGlobalMap",      "Знову глухий кут." },
        { "NrafCastle",        "Замок Нраф. Все така ж похмура будівля." },
        { "Windmill",          "Вітряк не зупиняється ні вдень ні вночі." },
        { "Windmill1",         "Другий вітряк. Вже звик до їх скрипу." },
        { "TradingHouse",      "Господар поглядає на мене.\n— Ну як? Лист вже в дорозі до Stormwind?" },
        { "Transition",        "Це місце досі відчувається якось дивно." },
        { "RoadBelow",         "Нижня частина села. Все так само бідно." },
        { "RoadBelow1",        "Та сама темна стежка." },
        { "RoadBelow2",        "Ця частина виглядає ще більш занедбаною." },
        { "RoadBelow3",        "Вже знаю цей шлях." },
        { "RoadBelow4",        "Вже вдруге тут." },
        { "LowerIntersection", "Селянин виглядає з-за рогу.\n— Пане! Той бандит досі тут! Коли ви вже розберетесь?!" },
        { "Manor",             "Маєток стоїть тихо. Таємниця цієї родини мене не відпускає." },
    };
    // Думки героя для кожного вузла
private Dictionary<string, string> heroThoughts = new Dictionary<string, string>()
{
    { "Road5",
      "Старий виглядає щиро переляканим...\n" +
      "Якщо я допоможу — це займе час і може бути небезпечно.\n" +
      "Але якщо не допомогти — що буде з цим чоловіком?\n\n" +
      "Чи варто ризикувати?" },

    { "LowerIntersection",
      "Бандит на дорозі... Це небезпечно.\n" +
      "Селянин явно боїться і не може сам впоратись.\n" +
      "Якщо я пройду повз — він продовжить тероризувати село.\n\n" +
      "Може варто зупинити це зараз?" },

    { "TradingHouse",
      "Лист до Stormwind... Це далека дорога.\n" +
      "Але торговець обіцяє добру винагороду.\n" +
      "А мені якраз потрібні гроші.\n\n" +
      "Ризик чи вигода — ось питання." },
};
}