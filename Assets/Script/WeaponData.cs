using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Game/Weapon")]
public class WeaponData : ScriptableObject
{
    public int id;
    public string weaponName;
    public int baseDamage;
    public DamageType damageType;
    public int price;

    public AbilityType scalingAbility; // від якої характеристики рахується бонус
}