using UnityEngine;

// Клас екземпляра броні в грі
[System.Serializable]
public class Armor
{
    public ArmorData data;

    public string Name => data.armorName;
    public ArmorType Type => data.type;
    public int BonusToAC => data.bonusToAC;

    public Armor(ArmorData data)
    {
        this.data = data;
    }
}

// Клас гравця
public class Player : MonoBehaviour
{
    public string playerName = "Артем";
    public int baseAC = 10;          // Базовий КЗ
    public Armor equippedArmor;      // Надягнута броня

    // Поточний КЗ з бронею
    public int GetCurrentAC()
    {
        if (equippedArmor != null)
            return baseAC + equippedArmor.BonusToAC;
        return baseAC;
    }

    // Надіти броню
    public void EquipArmor(ArmorData armorData)
    {
        equippedArmor = new Armor(armorData);
        Debug.Log(playerName + " надів броню: " + equippedArmor.Name + ", КЗ = " + GetCurrentAC());
    }
}

// Приклад використання в Unity
public class Game : MonoBehaviour
{
    public ArmorData lightArmor;
    public ArmorData mediumArmor;
    public ArmorData heavyArmor;

    void Start()
    {
        Player hero = new Player();
        hero.playerName = "Артем";

        // Беремо броню з інвентаря або вибору гравця
        // Наприклад:
        // hero.EquipArmor(lightArmor);
        // hero.EquipArmor(mediumArmor);
        // hero.EquipArmor(heavyArmor);
        // У грі завжди надягається тільки одна броня
    }
}