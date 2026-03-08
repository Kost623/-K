using UnityEngine;

[System.Serializable]
public class AbilityScore
{
    public AbilityType Type;
    public int Value;

    public int Modifier
    {
        get
        {
            return Mathf.FloorToInt((Value - 10) / 2f);
        }
    }

    public AbilityScore(AbilityType type, int value)
    {
        Type = type;
        Value = value;
    }
}