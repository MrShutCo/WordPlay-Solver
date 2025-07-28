namespace WordPlaySolver;

public class Modifier
{
    public string Name;
    public ModTimingType TimingType;
    public double Amount;
    
    public Modifier(string name, ModTimingType timingType, double amount)
    {
        Name = name;
        TimingType = timingType;
        Amount = amount;
    }
    
    public virtual bool IsActive(Hand hand, List<Tile> bag) => true;
}

public class SlotMultiplier(string name, int multiplier, int slotIndex, Func<Hand, List<Tile>, bool>? conditional = null) 
    : Modifier(name, ModTimingType.TileMult, amount: multiplier)
{
    public readonly int SlotIndex = slotIndex;
    public readonly Func<Hand, List<Tile>, bool> Conditional = conditional;

    public override bool IsActive(Hand hand, List<Tile> bag)
    {
        return Conditional?.Invoke(hand, bag) ?? true;
    }
}

public class GenericBonus : Modifier
{
    public ModType ModType { get; set; }
    
    private Func<Hand, List<Tile>, bool> Conditional { get; set; }
    
    public GenericBonus(string name, double amount, ModType modType, Func<Hand, List<Tile>, bool> conditional) 
        : base(name, modType == ModType.Add ? ModTimingType.Bonus : ModTimingType.Multiplier, amount)
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
    
    public GenericBonusAdditional(string name, int amount, ModType modType, Func<Hand, List<Tile>, string, bool> conditional, string additional) 
        : base(name, modType == ModType.Add ? ModTimingType.Bonus : ModTimingType.Multiplier, amount)
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

public enum ModType
{
    Add,
    Mult
}

public enum ModTimingType
{
    None,
    TileMult,
    Bonus,
    Multiplier
}