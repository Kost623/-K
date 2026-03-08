using UnityEngine;
using UnityEngine.UI;
public class HpBar : MonoBehaviour
{
    Image healthBar;
    public float maxHP=100f;
    public float HP;
    void Start()
    {
        healthBar = GetComponent<Image>();
        HP=maxHP;
    }

    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp01(HP / maxHP);
    }
}