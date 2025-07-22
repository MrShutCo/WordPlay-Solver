namespace WordPlaySolver;

public class State
{
    private Tile[] _allLetters;
    public Hand Hand { get; private set; }
    
    public State(char[] allLetters)
    {
        _allLetters = new Tile[allLetters.Length];
        for (int i = 0; i < allLetters.Length; i++)
        {
            _allLetters[i] = new Tile(allLetters[i], TileModifierType.Basic, 0);
        }
    }

    public void DrawHand(Random random, int handSize)
    {
        random.Shuffle(_allLetters);
        Hand = new Hand(_allLetters.Take(handSize).ToList());
        //_allLetters = _allLetters.Skip(handSize).ToArray();
        Console.WriteLine($"Hand drawn: '{Hand.GetWord()}'");
    }

    public (Hand hand, int value) FindBestWordInHand(Tree wordTree, Scorer scorer)
    {
        var permutations = GenerateAllPermutations(Hand.Tiles.ToArray(), 10, wordTree);
        Hand bestHand = null;
        var bestScore = 0;
        foreach (var tiles in permutations)
        {
            var hand = new Hand(tiles.ToList());
            var score = scorer.GetScore(hand);
            var word = hand.GetWord();
            // TODO: get the best possible letters based on modifiers
            if (wordTree.TryGetWord(word) && score > bestScore)
            {
                bestScore = score;
                bestHand = hand;
            }
        }
        return (bestHand, bestScore);
    }
    

    
    static IEnumerable<Tile[]> GenerateAllPermutations(Tile[] chars, int maxLength, Tree tree)
    {
        var used = new bool[chars.Length];
        var current = new Tile[maxLength]; // reuse fixed array
        return Permute(chars, used, current, 0, maxLength, tree);
    }

    static IEnumerable<Tile[]> Permute(Tile[] chars, bool[] used, Tile[] current, int depth, int maxLength, Tree tree)
    {
        // Break early because no word even starts with this
        if (chars.Length > 0 && !tree.ExistsThatBeginsWith(current[..depth])) yield break;
        
        if (depth > 0)
        {
            yield return current[..depth];
        }

        if (depth == maxLength)
            yield break;

        for (int i = 0; i < chars.Length; i++)
        {
            if (used[i]) continue;

            used[i] = true;
            current[depth] = chars[i];

            foreach (var perm in Permute(chars, used, current, depth + 1, maxLength, tree))
            {
                yield return perm;
            }

            used[i] = false;
        }
    }
}

public enum TileModifierType
{
    Basic,
    Gold,
    Emerald,
    Red,
    Diamond
}

public class Hand
{
    public List<Tile> Tiles { get; private set; }

    public Hand(List<Tile> tiles)
    {
        Tiles = tiles;
    }
    
    public char[] GetChars() => Tiles.Select(c => c.Letter).ToArray();
    public string GetWord() => new(GetChars());
    
    public int GetModifierCount(TileModifierType modifier) => Tiles.Count(t => t.Modifier == modifier);
}

public class Tile
{
    public char Letter { get; private set; }
    public TileModifierType Modifier { get; set; }
    public int AddedValue { get; set; }
    
    public Tile(char letter, TileModifierType modifier, int addedValue)
    {
        Letter = letter;
        Modifier = modifier;
        AddedValue = addedValue;
    }
}