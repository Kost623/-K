using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Character Identity")]
    [Tooltip("Enter Name and Surname separated by space")]
    public string FullName;

    [Header("Ability Scores (6 - 24)")]
    public AbilityScore Strength = new AbilityScore(AbilityType.Strength, 10);
    public AbilityScore Dexterity = new AbilityScore(AbilityType.Dexterity, 10);
    public AbilityScore Constitution = new AbilityScore(AbilityType.Constitution, 10);
    public AbilityScore Intelligence = new AbilityScore(AbilityType.Intelligence, 10);
    public AbilityScore Wisdom = new AbilityScore(AbilityType.Wisdom, 10);
    public AbilityScore Charisma = new AbilityScore(AbilityType.Charisma, 10);

    [Header("Combat Stats")]
    public int MaxHP;
    public int CurrentHP;

    public int ArmorClass
    {
        get
        {
            return 10 + Dexterity.Modifier;
        }
    }

    private void Awake()
    {
        ClampAbilities();
        CalculateHP();
        CurrentHP = MaxHP;
    }

    private void ClampAbilities()
    {
        Strength.Value = Mathf.Clamp(Strength.Value, 6, 24);
        Dexterity.Value = Mathf.Clamp(Dexterity.Value, 6, 24);
        Constitution.Value = Mathf.Clamp(Constitution.Value, 6, 24);
        Intelligence.Value = Mathf.Clamp(Intelligence.Value, 6, 24);
        Wisdom.Value = Mathf.Clamp(Wisdom.Value, 6, 24);
        Charisma.Value = Mathf.Clamp(Charisma.Value, 6, 24);
    }

    private void CalculateHP()
    {
        // Base HP = 10 + Constitution modifier (как в D&D логике)
        MaxHP = 10 + Constitution.Modifier;

        if (MaxHP < 1)
            MaxHP = 1;
    }

    public void TakeDamage(int damage)
    {
        CurrentHP -= damage;

        if (CurrentHP < 0)
            CurrentHP = 0;
    }

    public void Heal(int amount)
    {
        CurrentHP += amount;

        if (CurrentHP > MaxHP)
            CurrentHP = MaxHP;
    }

    public bool IsAlive()
    {
        return CurrentHP > 0;
    }
}