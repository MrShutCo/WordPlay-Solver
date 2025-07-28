namespace WordPlaySolver;

public class State
{
    private Hand PlayableTiles { get; set; }
    
    public List<Modifier> Modifiers { get; private set; }
    
    private readonly Tile[] _allLetters;
    
    public State(char[] allLetters)
    {
        _allLetters = new Tile[allLetters.Length];
        for (int i = 0; i < allLetters.Length; i++)
        {
            _allLetters[i] = new Tile(allLetters[i].ToString(), TileModifierType.Basic, 0);
        }
        Modifiers = new List<Modifier>();
    }

    public State(string[] allLetters)
    {
        _allLetters = new Tile[allLetters.Length];
        for (int i = 0; i < allLetters.Length; i++)
        {
            _allLetters[i] = new Tile(allLetters[i], TileModifierType.Basic, 0);
        }
        Modifiers = new List<Modifier>();
    }

    public void DrawHand(int handSize)
    {
        PlayableTiles = new Hand(_allLetters.Take(handSize).ToList());
        Console.WriteLine($"Hand drawn: '{PlayableTiles.GetWord()}'");
    }

    public IEnumerable<(Hand hand, double value)> FindBestWordsInHand(Tree wordTree, Scorer scorer, SearchParameters parameters)
    {
        var tracker = new EliteTracker<Hand>(parameters.BestNResults, true);
        var permutations = GenerateAllPermutations(PlayableTiles.Tiles.ToArray(), parameters.MaxLength, wordTree);
        
        foreach (var tiles in permutations)
        {
            var hand = new Hand(tiles.ToList());
            
            var score = scorer.GetScore(hand, [], Modifiers);
            var word = hand.GetWord();

            if (tiles.Length < parameters.MinLength) continue;
            if (parameters.Prefix != "" && !word.StartsWith(parameters.Prefix)) continue;
            if (parameters.Suffix != "" && !word.EndsWith(parameters.Suffix)) continue;
            if (parameters.Contains != "" && !word.Contains(parameters.Contains)) continue;
            
            if (parameters.MaxWordScore != null && score > parameters.MaxWordScore) continue;
            
            if (wordTree.TryGetWord(word))
            {
                tracker.TryUpdateElite(hand, score, false);
            }
        }

        return tracker.GetElitesInOrder();
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

public class SearchParameters
{
    public int MinLength { get; set; }
    public int MaxLength { get; set; }
    public int BestNResults { get; set; }
    
    public string Suffix { get; set; }
    public string Prefix { get; set; }
    public string Contains { get; set; }
    public int? MaxWordScore { get; set; }
}