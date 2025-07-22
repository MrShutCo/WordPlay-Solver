namespace WordPlaySolver;

/// <summary>
/// Upgrades:
///
/// Slot modifiers: x-th tile scores some mult
/// Swap vowel
/// Add additional tile
/// Length x gives some points
/// +X based on something (number of upgrades used)
/// First tile 5x if vowel
/// Challenges:
///     +4 bonus points if word contains letter pair, -40 otherwise, caps at 40
///     +2 bonus points if word contains letter pair
/// 
/// </summary>
public class Modifier
{
    public string Name;
    
}

public class SlotMultiplier(int multiplier, int slotIndex) : Modifier
{
    public int Multiplier = multiplier;
    public int SlotIndex = slotIndex;
}

public class FlatBonusModifier : Modifier
{
    public int AddedBonus { get; set; }

    public FlatBonusModifier(int bonus)
    {
        AddedBonus = bonus;
    }
}

public class GenericAddBonus : Modifier
{
    public int Amount { get; set; }
    public Func<Hand, List<Tile>, bool> Conditional { get; set; }
    
    public GenericAddBonus(int amount, Func<Hand, List<Tile>, bool> conditional)
    {
        Amount = amount;
        Conditional = conditional;
    }
}

public class GenericMultBonus : Modifier
{
    public int Multiplier { get; set; }
    public Func<Hand, List<Tile>, bool> Conditional { get; set; }
    
    public GenericMultBonus(int multiplier, Func<Hand, List<Tile>, bool> conditional)
    {
        Multiplier = multiplier;
        Conditional = conditional;
    }
}

public class LengthFlatBonusModifier : FlatBonusModifier
{
    public int Length { get; set; }
    public LengthFlatBonusModifier(int bonus, int length) : base(bonus)
    {
        Length = length;
    }
}

public class PairBonusPoints : Modifier
{
    public int CurrentBonus;
    public int AddedBonus;
    public int MaxBonus;
    public EPairType PairType;
    public string Substring;
    
    public PairBonusPoints(int currentBonus, int addedBonus, int maxBonus, EPairType pairType, string substring)
    {
        CurrentBonus = currentBonus;
        AddedBonus = addedBonus;
        MaxBonus = maxBonus;
        PairType = pairType;
        Substring = substring;
    }

    public void LevelUp()
    {
        CurrentBonus += AddedBonus;
        CurrentBonus = Math.Min(CurrentBonus, MaxBonus);
    }

    public enum EPairType
    {
        Contains,
        StartsWith,
        EndsWith,
    }
}