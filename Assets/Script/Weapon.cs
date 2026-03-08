using UnityEngine;

[System.Serializable]
public class Weapon
{
    public WeaponData data;

    public int CalculateDamage(Character character)
    {
        if (data == null || character == null)
            return 0;

        AbilityScore ability = GetScalingAbility(character);

        return data.baseDamage + ability.Modifier;
    }

    private AbilityScore GetScalingAbility(Character character)
    {
        switch (data.scalingAbility)
        {
            case AbilityType.Strength:
                return character.Strength;

            case AbilityType.Dexterity:
                return character.Dexterity;

            case AbilityType.Constitution:
                return character.Constitution;

            case AbilityType.Intelligence:
                return character.Intelligence;

            case AbilityType.Wisdom:
                return character.Wisdom;

            case AbilityType.Charisma:
                return character.Charisma;

            default:
                return character.Strength;
        }
    }
}