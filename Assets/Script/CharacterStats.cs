using UnityEngine;

[System.Serializable]
public class CharacterStats
{
    public AbilityScore Strength = new AbilityScore(AbilityType.Strength, 10);
    public AbilityScore Dexterity = new AbilityScore(AbilityType.Dexterity, 10);
    public AbilityScore Constitution = new AbilityScore(AbilityType.Constitution, 10);
    public AbilityScore Intelligence = new AbilityScore(AbilityType.Intelligence, 10);
    public AbilityScore Wisdom = new AbilityScore(AbilityType.Wisdom, 10);
    public AbilityScore Charisma = new AbilityScore(AbilityType.Charisma, 10);

    public AbilityScore GetAbility(AbilityType type)
    {
        switch (type)
        {
            case AbilityType.Strength: return Strength;
            case AbilityType.Dexterity: return Dexterity;
            case AbilityType.Constitution: return Constitution;
            case AbilityType.Intelligence: return Intelligence;
            case AbilityType.Wisdom: return Wisdom;
            case AbilityType.Charisma: return Charisma;
            default: return null;
        }
    }
}