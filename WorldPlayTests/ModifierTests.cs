using WordPlaySolver;

namespace WorldPlayTests;

public class ModifierTests
{
    [Fact]
    public void Test_IdenticalAdjacentLetters()
    {
        Assert.True(ModifierConditionals.IdenticalAdjacentLetters(GetHand(["F", "O", "O"]), []));
        Assert.False(ModifierConditionals.IdenticalAdjacentLetters(GetHand(["A", "P", "G", "A"]), []));
        Assert.True(ModifierConditionals.IdenticalAdjacentLetters(GetHand(["I", "ING", "O"]), []));
        Assert.True(ModifierConditionals.IdenticalAdjacentLetters(GetHand(["I", "ERS", "S"]), []));
        Assert.False(ModifierConditionals.IdenticalAdjacentLetters(GetHand(["S", "ERS", "I"]), []));
    }

    [Fact]
    public void Test_FirstTileIsVowel()
    {
        Assert.Equal((true, null), ModifierConditionals.FirstTileIsVowel(GetHand(["I", "N", "G"]), []));
        Assert.Equal((false, null), ModifierConditionals.FirstTileIsVowel(GetHand(["ING", "I", "G"]), []));
        Assert.Equal((false, null), ModifierConditionals.FirstTileIsVowel(GetHand(["J", "I", "I"]), []));
    }

    [Fact]
    public void Test_HasFiveTiles()
    {
        var actual = ModifierConditionals.HasFiveTiles(GetHand(["P", "O", "G", "G", "E"]), []);
        Assert.Equal(true, actual.Item1);
        Assert.Equal([0,4], actual.Item2);

        actual = ModifierConditionals.HasFiveTiles(GetHand(["P", "O", "ING", "G", "E"]), []);
        Assert.Equal(true, actual.Item1);
        Assert.Equal([0,4], actual.Item2);
        
        Assert.Equal((false, null), ModifierConditionals.HasFiveTiles(GetHand(["P", "O", "ING"]), []));
        Assert.Equal((false, null), ModifierConditionals.HasFiveTiles(GetHand(["P", "O", "G"]), []));
    }

    [Fact]
    public void Test_WordBeginsWith()
    {
        Assert.True(ModifierConditionals.WordBeginsWith(GetHand(["S", "ERS", "I"]), [], "S"));
        Assert.False(ModifierConditionals.WordBeginsWith(GetHand(["S", "ERS", "I"]), [], "I"));
        Assert.True(ModifierConditionals.WordBeginsWith(GetHand(["ING", "ERS", "I"]), [], "I"));
    }
    
    [Fact]
    public void Test_WordEndsWith()
    {
        Assert.True(ModifierConditionals.WordEndsWith(GetHand(["S", "ERS", "I"]), [], "I"));
        Assert.False(ModifierConditionals.WordEndsWith(GetHand(["S", "ERS", "I"]), [], "S"));
        Assert.True(ModifierConditionals.WordEndsWith(GetHand(["ING", "ERS", "ERS"]), [], "S"));
    }

    [Fact]
    public void Test_WordHasNTiles()
    {
        Assert.True(ModifierConditionals.WordHasNTiles(GetHand(["F", "O", "U", "R"]), [], "4"));
        Assert.False(ModifierConditionals.WordHasNTiles(GetHand(["F", "O", "U", "R"]), [], "5"));
        Assert.True(ModifierConditionals.WordHasNTiles(GetHand(["F", "ING", "U", "R"]), [], "4"));
        Assert.False(ModifierConditionals.WordHasNTiles(GetHand(["F", "ING", "U", "R"]), [], "6"));
    }

    [Fact]
    public void Test_FirstLetterEqualsLast()
    {
        Assert.True(ModifierConditionals.FirstLetterEqualsLast(GetHand(["F", "O", "U", "F"]), []));
        Assert.False(ModifierConditionals.FirstLetterEqualsLast(GetHand(["F", "O", "U", "R"]), []));
        Assert.True(ModifierConditionals.FirstLetterEqualsLast(GetHand(["ING", "O", "U", "I"]), []));
        Assert.True(ModifierConditionals.FirstLetterEqualsLast(GetHand(["S", "O", "U", "ERS"]), []));
        Assert.False(ModifierConditionals.FirstLetterEqualsLast(GetHand(["ERS", "O", "U", "ERS"]), []));
    }

    [Fact]
    public void Test_FirstLastLettersVowels()
    {
        Assert.True(ModifierConditionals.FirstLastLettersVowels(GetHand(["A", "B", "A"]), []));
        Assert.True(ModifierConditionals.FirstLastLettersVowels(GetHand(["E", "B", "O"]), []));
        Assert.True(ModifierConditionals.FirstLastLettersVowels(GetHand(["ING", "B", "QU"]), []));
        Assert.False(ModifierConditionals.FirstLastLettersVowels(GetHand(["B", "B", "O"]), []));
        Assert.False(ModifierConditionals.FirstLastLettersVowels(GetHand(["B", "B", "S"]), []));
    }

    [Fact]
    public void Test_UniqueLetters()
    {
        Assert.True(ModifierConditionals.UniqueLetters(GetHand(["A", "B", "C"]), []));
        Assert.False(ModifierConditionals.UniqueLetters(GetHand(["A", "B", "A"]), []));
        Assert.False(ModifierConditionals.UniqueLetters(GetHand(["A", "ING", "I"]), []));
        Assert.False(ModifierConditionals.UniqueLetters(GetHand(["ERS", "U", "QU"]), []));
        Assert.True(ModifierConditionals.UniqueLetters(GetHand(["ERS", "ING", "QU"]), []));
    }

    [Fact]
    public void Test_FirstLetterIsVowel()
    {
        Assert.True(ModifierConditionals.FirstLetterIsVowel(GetHand(["A", "B", "C"]), []));
        Assert.True(ModifierConditionals.FirstLetterIsVowel(GetHand(["ING", "B", "C"]), []));
        Assert.False(ModifierConditionals.FirstLetterIsVowel(GetHand(["C", "A", "A"]), []));
        Assert.False(ModifierConditionals.FirstLetterIsVowel(GetHand(["QU", "A", "A"]), []));
    }

    [Fact]
    public void Test_NoAdjacentConsonants()
    {
        Assert.True(ModifierConditionals.NoAdjacentConsonants(GetHand(["A", "I", "E"]), []));
        Assert.False(ModifierConditionals.NoAdjacentConsonants(GetHand(["A", "B", "C"]), []));
        Assert.True(ModifierConditionals.NoAdjacentConsonants(GetHand(["C", "A", "A", "C"]), []));
        Assert.False(ModifierConditionals.NoAdjacentConsonants(GetHand(["A", "C", "C", "A", "I"]), []));
    }

    [Fact]
    public void Test_AtLeastThreeUniqueVowels()
    {
        Assert.False(ModifierConditionals.AtLeastThreeUniqueVowels(GetHand(["A", "B", "C"]), []));
        Assert.False(ModifierConditionals.AtLeastThreeUniqueVowels(GetHand(["A", "B", "E"]), []));
        Assert.False(ModifierConditionals.AtLeastThreeUniqueVowels(GetHand(["A", "E", "E", "B"]), []));
        Assert.True(ModifierConditionals.AtLeastThreeUniqueVowels(GetHand(["A", "I", "E"]), []));
        Assert.True(ModifierConditionals.AtLeastThreeUniqueVowels(GetHand(["O", "U", "V", "C", "F", "E", "E"]), []));
    }

    [Fact]
    public void Test_MoreVowelsThanConsonants()
    {
        Assert.False(ModifierConditionals.MoreVowelsThanConsonants(GetHand(["F", "F", "F"]), []));
        Assert.True(ModifierConditionals.MoreVowelsThanConsonants(GetHand(["E", "A", "I"]), []));
        Assert.False(ModifierConditionals.MoreVowelsThanConsonants(GetHand(["B", "F", "A", "I"]), []));
        Assert.True(ModifierConditionals.MoreVowelsThanConsonants(GetHand(["B", "F", "A", "I", "O"]), []));
    }
    
    [Fact]
    public void Test_NoE()
    {
        Assert.True(ModifierConditionals.NoE(GetHand(["F", "F", "F"]), []));
        Assert.False(ModifierConditionals.NoE(GetHand(["F", "E", "F"]), []));
        Assert.False(ModifierConditionals.NoE(GetHand(["F", "ERS", "F"]), []));
    }

    public static Tile[] GetTiles(List<string> tiles)
    {
        return tiles.Select(t => new Tile(t, TileModifierType.Basic, 0)).ToArray();
    }

    public static Hand GetHand(List<string> tiles)
    {
        return new Hand(GetTiles(tiles).ToList());
    }
}