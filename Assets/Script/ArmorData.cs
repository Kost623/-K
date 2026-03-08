using UnityEngine;

// Armor types
public enum ArmorType
{
    Light,
    Medium,
    Heavy
}

// ScriptableObject для броні
[CreateAssetMenu(fileName = "NewArmor", menuName = "Items/Armor")]
public class ArmorData : ScriptableObject
{
    public string armorName;      // Назва броні
    public ArmorType type;        // Тип броні: Light / Medium / Heavy
    public int bonusToAC;         // Бонус до КЗ
}