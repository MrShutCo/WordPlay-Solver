namespace WordPlaySolver;


public class Slot
{

    public int FlatBonus { get; set; }
    public int LetterMultBonus { get; set; }
    
    public Slot(int flatBonus, int letterMultBonus)
    {
        FlatBonus = flatBonus;
        LetterMultBonus = letterMultBonus;
    }
}

public class Scorer
{
    public List<string> Words = new();
    
    public readonly Dictionary<char, int> Scores = new ()
    {
        { 'A', 1 },
        { 'B', 3 },
        { 'C', 3 },
        { 'D', 2 },
        { 'E', 1 },
        { 'F', 4 },
        { 'G', 2 },
        { 'H', 4 },
        { 'I', 1 },
        { 'J', 8 },
        { 'K', 5 },
        { 'L', 1 },
        { 'M', 3 },
        { 'N', 1 },
        { 'O', 1 },
        { 'P', 3 },
        { 'Q', 10 },
        { 'R', 1 },
        { 'S', 1 },
        { 'T', 1 },
        { 'U', 1 },
        { 'V', 4 },
        { 'W', 4 },
        { 'X', 8 },
        { 'Y', 4 },
        { 'Z', 10 },
    };

    public static bool IsVowel(char c)
    {
        return c is 'A' or 'E' or 'I' or 'O' or 'U';
    }

    private List<Slot> slots { get; set; }
    private List<int> _defaultPlus = [0, 0, 0, 0, 5, 5, 5, 10, 10, 15, 15, 20, 20, 20, 25, 25, 25, 30, 40, 50];

    public Scorer(int slotCount)
    {
        slots = new List<Slot>();
        for (int i = 0; i < slotCount; i++)
        {
            slots.Add(new Slot(_defaultPlus[i], 1));
        }
    }

    public void LoadWordsFromFile(string path, int maxWordLength)
    {
        var words =  File.ReadLines(path).ToList();
        foreach (var word in words)
        {
            if (word.Length > maxWordLength) continue;
            Words.Add(word);
        }
        Console.WriteLine($"Loaded {Words.Count} words");
    }

    public void LoadWords(string words, int maxWordLength)
    {
        var list = words.Split("\n").ToList();
        foreach (var word in list)
        {
            if (word.Length > maxWordLength) continue;
            Words.Add(word);
        }
        Console.WriteLine($"Loaded {Words.Count} words");
    }

    public void ApplyUpgrades(List<Modifier> upgrades)
    {
        foreach (var upgrade in upgrades.Where(u => u is SlotMultiplier))
        {
            var mult = (SlotMultiplier)upgrade;
            slots[mult.SlotIndex].LetterMultBonus = mult.Multiplier;
        }
    }
    
    public int GetScore(Hand hand, List<Tile> bag, List<Modifier> modifiers)
    {
        var word = hand.GetWord();
        var baseScore = 0;
        var bonusScore = 0;
        for (var i = 0; i < word.Length; i++)
        {
            baseScore += Scores[word[i]] * slots[i].LetterMultBonus;
            bonusScore += slots[i].FlatBonus;
        }

        foreach (var mod in modifiers)
        {
            if (mod is GenericAddBonus addBonus && addBonus.Conditional(hand, bag)) 
                bonusScore += addBonus.Amount;
        }
        
        var goldCount = hand.GetModifierCount(TileModifierType.Gold);
        if (goldCount > 0)
        {
            baseScore *= goldCount;
        }

        if (hand.Tiles.Last().Modifier == TileModifierType.Red)
        {
            baseScore *= 2;
        }
        
        var sumScore = baseScore + bonusScore;

        foreach (var mod in modifiers)
        {
            if (mod is GenericMultBonus multBonus && multBonus.Conditional(hand, bag)) 
                sumScore *= multBonus.Multiplier;
        }
        
        return sumScore;
    }
}

