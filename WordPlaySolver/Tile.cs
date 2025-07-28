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
    
    public char[] GetChars() => string.Concat(Tiles.Select(t => t.Letters)).ToCharArray();
    public string GetWord() => new(GetChars());

    public string GetResult(double value) => $"{GetWord()}\t{value}";
    
    public int GetModifierCount(TileModifierType modifier) => Tiles.Count(t => t.Modifier == modifier);

    public override string ToString()
    {
        return GetWord();
    }

    public static char[] GetCharsFromTiles(Tile[] tiles)
    {
        return string.Concat(tiles.Select(t => t.Letters)).ToCharArray();
    }
}

public class Tile
{
    public string Letters { get; private set; }
    public TileModifierType Modifier { get; set; }
    public int AddedValue { get; set; }
    
    public Tile(string letters, TileModifierType modifier, int addedValue)
    {
        Letters = letters;
        Modifier = modifier;
        AddedValue = addedValue;
    }
}