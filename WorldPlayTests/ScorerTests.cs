using WordPlaySolver;

namespace WorldPlayTests;

public class ScorerTests
{
    private Scorer _scorer;
    
    public ScorerTests()
    {
        _scorer = new Scorer(16);
        _scorer.LoadWordsFromFile("wordsfull.txt", 16);
    }

    [Fact]
    public void Test_SimpleWordScores()
    {
        Assert.Equal(5, _scorer.GetScore(ModifierTests.GetHand(["C", "A", "T"]), [], []));
        Assert.Equal(19, _scorer.GetScore(ModifierTests.GetHand(["QU", "A", "ERS"]), [], []));
        Assert.Equal(26, _scorer.GetScore(ModifierTests.GetHand(["Q", "U", "O", "T", "ING"]), [], []));
        Assert.Equal(56, _scorer.GetScore(ModifierTests.GetHand(["A", "B", "C", "D", "E", "F", "G", "H", "I"]), [], []));
        Assert.Equal(31, _scorer.GetScore(ModifierTests.GetHand(["Z", "Y", "X", "*", "W"]), [], []));
    }

    [Fact]
    public void Test_WordScoreWithUpgrades()
    {
        
    }
}