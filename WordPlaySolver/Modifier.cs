namespace WordPlaySolver;

public class Modifier
{
    public string Name;
    public ModTimingType TimingType;
    public Rarity Rarity;
    public double Amount;
    
    public Modifier(string name, ModTimingType timingType, Rarity rarity, double amount)
    {
        Name = name;
        Rarity = rarity;
        TimingType = timingType;
        Amount = amount;
    }
    
    public virtual bool IsActive(Hand hand, List<Tile> bag) => true;
}

public class SlotMultiplier(string name, int multiplier, List<int> slotIndices, Rarity rarity, Func<Hand, List<Tile>, (bool, List<int>?)>? conditional = null) 
    : Modifier(name, ModTimingType.TileMult, rarity, amount: multiplier)
{
    public List<int> SlotIndices = slotIndices;

    public override bool IsActive(Hand hand, List<Tile> bag)
    {
        var result = conditional?.Invoke(hand, bag);
        if (result?.Item2 != null)
        {
            SlotIndices = result.Value.Item2;
        }
        return result?.Item1 ?? true;
    }
}

public class GenericBonus : Modifier
{
    public ModType ModType { get; set; }
    
    private Func<Hand, List<Tile>, bool> Conditional { get; set; }
    
    public GenericBonus(string name, double amount, ModType modType, Rarity rarity, Func<Hand, List<Tile>, bool> conditional) 
        : base(name, modType == ModType.Add ? ModTimingType.Bonus : ModTimingType.Multiplier, rarity, amount)
    {
        ModType = modType;
        Amount = amount;
        Conditional = conditional;
    }

    public override bool IsActive(Hand hand, List<Tile> bag)
    {
        return Conditional.Invoke(hand, bag);
    }
}

public class GenericBonusAdditional : Modifier
{
    public string Additional;
    public ModType ModType { get; set; }
    private Func<Hand, List<Tile>, string, bool> Conditional { get; set; }
    
    public GenericBonusAdditional(string name, int amount, ModType modType, Rarity rarity, Func<Hand, List<Tile>, string, bool> conditional, string additional) 
        : base(name, modType == ModType.Add ? ModTimingType.Bonus : ModTimingType.Multiplier, rarity, amount)
    {
        Additional = additional;
        Amount = amount;
        ModType = modType;
        Conditional = conditional;
    }

    public override bool IsActive(Hand hand, List<Tile> bag)
    {
        return Conditional.Invoke(hand, bag, Additional);
    }
}

public class YVowelModifier : Modifier
{
    public YVowelModifier() : base("Y is vowel", ModTimingType.None, Rarity.Uncommon, 0)
    {
        // TODO: this is dirty trick very bad no no
        Scorer.IsYVowel = true;
    }
}

public enum ModType
{
    Add,
    Mult
}

public enum Rarity
{
    Common,
    Uncommon,
    Rare,
    Legendary
}

public enum ModTimingType
{
    None,
    TileMult,
    Bonus,
    Multiplier
}