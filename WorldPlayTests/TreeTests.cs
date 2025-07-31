using WordPlaySolver;

namespace WorldPlayTests;

public class TreeTests
{
    private Scorer _scorer;
    private Tree _tree;
    
    public TreeTests()
    {
        _scorer = new Scorer(16);
        _scorer.LoadWordsFromFile("wordsfull.txt", 16);
        _tree = new Tree(_scorer.Words);
    }

    [Fact]
    public void Test_TryGetWord()
    {
        Assert.True(_tree.TryGetWord("HELLO"));
        Assert.True(_tree.TryGetWord("HELL"));
        Assert.False(_tree.TryGetWord("XCIO"));
        Assert.False(_tree.TryGetWord("HELLOP"));
    }

    [Fact]
    public void Test_ExistsThatBeginsWith()
    {
        Assert.True(_tree.ExistsThatBeginsWith(ModifierTests.GetTiles(["T", "H"])));
        Assert.True(_tree.ExistsThatBeginsWith(ModifierTests.GetTiles(["T", "H", "E", "Y"])));
        Assert.False(_tree.ExistsThatBeginsWith(ModifierTests.GetTiles(["P", "Z", "Y"])));
        Assert.True(_tree.ExistsThatBeginsWith(ModifierTests.GetTiles(["C", "R", "Y", "ING"])));
        Assert.True(_tree.ExistsThatBeginsWith(ModifierTests.GetTiles(["ERS", "T"])));
        Assert.False(_tree.ExistsThatBeginsWith(ModifierTests.GetTiles(["QU", "U"])));
    }

    [Fact]
    public void Test_ExistsThatBeginsWith_Star()
    {
        var tiles = ModifierTests.GetTiles(["T", "*", "E"]);
        Assert.True(_tree.ExistsThatBeginsWith(tiles));
        Assert.Equal("A", tiles[1].StarMatched);
        
        tiles = ModifierTests.GetTiles(["C", "R", "I", "N", "*", "E"]);
        Assert.True(_tree.ExistsThatBeginsWith(tiles));
        Assert.Equal("G", tiles[4].StarMatched);
        
        tiles = ModifierTests.GetTiles(["Z", "E", "A", "S", "*"]);
        Assert.False(_tree.ExistsThatBeginsWith(tiles));
        Assert.Null(tiles[3].StarMatched);
        
        tiles = ModifierTests.GetTiles(["T", "*", "*", "ING"]);
        Assert.True(_tree.ExistsThatBeginsWith(tiles));
        Assert.Equal("A", tiles[1].StarMatched);
        Assert.Equal("K", tiles[2].StarMatched);
    }
    
}