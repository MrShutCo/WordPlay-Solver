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

    private List<Slot> slots { get; set; }
    private List<int> _defaultPlus = [0, 0, 0, 0, 5, 5, 5, 10, 10, 15];

    public Scorer(int slotCount)
    {
        slots = new List<Slot>();
        for (int i = 0; i < slotCount; i++)
        {
            slots.Add(new Slot(_defaultPlus[i], 1));
        }
    }

    public void LoadWords(string path, int maxWordLength)
    {
        var words =  File.ReadLines(path).ToList();
        foreach (var word in words)
        {
            if (word.Length > maxWordLength) continue;
            Words.Add(word);
        }
        Console.WriteLine($"Loaded {Words.Count} words");
    }
    
    public int GetScore(Hand hand)
    {
        var word = hand.GetWord();
        var score = 0;
        for (int i = 0; i < word.Length; i++)
        {
            score += (Scores[word[i]] + slots[i].FlatBonus) * slots[i].LetterMultBonus;
        }

        var goldCount = hand.GetModifierCount(TileModifierType.Gold);
        if (goldCount > 0)
        {
            score *= goldCount;
        }
        
        return score;
    }
}

