namespace WordPlaySolver;

public static class ModifierConditionals
{
    public static bool IdenticalAdjacentLetters(Hand hand, List<Tile> bag)
    {
        var word = hand.GetWord();
        var lastSeen = word[0];
        for (int i = 1; i < word.Length; i++)
        {
            if (lastSeen == word[i]) return true;
            lastSeen = word[i];
        }

        return false;
    }

    public static (bool, List<int>?) FirstTileIsVowel(Hand hand, List<Tile> bag) => (Scorer.IsVowel(hand.Tiles.First()), null);

    public static (bool, List<int>?) HasFiveTiles(Hand hand, List<Tile> bag)
    {
        if (hand.Tiles.Count == 5) return (true, [0, 4]);
        return (false, null);
    }

    public static (bool, List<int>?) AdjacentTilesShareSameLetter(Hand hand, List<Tile> bag)
    {
        return (false, null);
    }

    public static bool WordBeginsWith(Hand hand, List<Tile> bag, string prefix) => hand.GetWord().StartsWith(prefix);
    
    public static bool WordEndsWith(Hand hand, List<Tile> bag, string suffix) => hand.GetWord().EndsWith(suffix);
    
    public static bool WordHasNTiles(Hand hand, List<Tile> bag, string suffix)
    {
        var valid = int.TryParse(suffix, out var result);
        if (!valid) return false;
        return hand.Tiles.Count == result;
    }

    public static bool FirstLetterEqualsLast(Hand hand, List<Tile> bag)
    {
        var word = hand.GetWord();
        return word.First() == word.Last();
    }

    public static bool FirstLastLettersVowels(Hand hand, List<Tile> _)
    {
        var word = hand.GetWord();
        return Scorer.IsVowel(word.First()) && Scorer.IsVowel(word.Last());
    }

    public static bool UniqueLetters(Hand hand, List<Tile> _)
    {
        var word = hand.GetWord();
        int seenBitMap = 0x0;
        for (int i = 0; i < word.Length; i++)
        {
            int index = Tree.CharToIndex(word[i]);
            if ((seenBitMap & (0x1 << index)) > 0)
            {
                return false;
            }
            seenBitMap |= (0x1 << index);
        }

        return true;
    }

    public static bool FirstLetterIsVowel(Hand hand, List<Tile> bag) => Scorer.IsVowel(hand.Tiles[0].Letters[0]);
    
    public static bool NoAdjacentConsonants(Hand hand, List<Tile> bag)
    {
        var word = hand.GetWord();
        bool lastIsConsonant = !Scorer.IsVowel(word[0]);
        for (int i = 1; i < word.Length; i++)
        {
            if (lastIsConsonant && !Scorer.IsVowel(word[i]) ) return false;
            lastIsConsonant = !Scorer.IsVowel(word[i]);
        }

        return true;
    }

    public static bool AtLeastThreeUniqueVowels(Hand hand, List<Tile> bag)
    {
        int seenVowel = 0;
        var word = hand.GetWord();
        foreach (var ch in word)
        {
            if (ch == 'A') seenVowel |= 0x1;
            if (ch == 'E') seenVowel |= 0x2;
            if (ch == 'I') seenVowel |= 0x4;
            if (ch == 'O') seenVowel |= 0x8;
            if (ch == 'U') seenVowel |= 0x10;
            // TODO: Y might be valid here
        }

        int seenVowelCount = 0;
        for (int i = 0; i < 5; i++)
        {
            if ((seenVowel & 0x1) == 1) seenVowelCount++;
            seenVowel >>= 1;
        }

        return seenVowelCount >= 3;
    }

    public static bool MoreVowelsThanConsonants(Hand hand, List<Tile> bag)
    {
        var word = hand.GetWord();
        int vowelCount = word.Count(w => Scorer.IsVowel(w));
        return vowelCount > (word.Length - vowelCount);
    }

    public static bool NoE(Hand hand, List<Tile> bag)
    {
        return hand.GetWord().All(ch => ch != 'E');
    }
}