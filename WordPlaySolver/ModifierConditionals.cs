namespace WordPlaySolver;

public static class ModifierConditionals
{
    public static bool IdenticalLetters(Hand hand, List<Tile> bag)
    {
        var word = hand.GetWord();
        var lastSeen = word[0];
        for (int i = 1; i < word.Length; i++)
        {
            if (lastSeen == word[i]) return true;
        }

        return false;
    }

    public static bool FirstTileIsVowel(Hand hand, List<Tile> bag) => Scorer.IsVowel(hand.Tiles.First());

    public static bool WordBeginsWith(Hand hand, List<Tile> bag, string suffix) => hand.GetWord().StartsWith(suffix);
}