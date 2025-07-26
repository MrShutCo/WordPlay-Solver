namespace WordPlaySolver;

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

    public string GetResult(double value) => $"{GetWord()}\t{value}";
    
    public int GetModifierCount(TileModifierType modifier) => Tiles.Count(t => t.Modifier == modifier);

    public override string ToString()
    {
        return GetWord();
    }
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