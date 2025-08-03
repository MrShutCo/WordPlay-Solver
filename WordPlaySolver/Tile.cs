using System.Text;

namespace WordPlaySolver;

public enum TileModifierType
{
    Basic,
    Golden,
    Diamond,
    Emerald,
    Dot,
    Glass,
    Mirror,
    Potion
}

public class Hand : ICloneable
{
    public List<Tile> Tiles { get; private set; }
    
    public Hand(List<Tile> tiles)
    {
        Tiles = tiles;
    }
    
    public string GetWord() {
        StringBuilder s = new StringBuilder();
        foreach (var tile in Tiles)
        {
            var text = tile.IsSpecial ? tile.StarMatched : tile.Letters;
            s.Append(text);
        }
        return s.ToString();
    }
    
    public char[] GetChars() => GetWord().ToCharArray();

    public string GetResult(double value) => $"{GetWord()}\t{value}";
    
    public int GetModifierCount(TileModifierType modifier) => Tiles.Count(t => t.Modifier == modifier);

    public override string ToString()
    {
        return GetWord();
    }

    public object Clone()
    {
        List<Tile> cloned = new List<Tile>(Tiles.Count);
        for (int i = 0; i < Tiles.Count; i++)
        {
            cloned.Add(new Tile(Tiles[i]));
        }

        return new Hand(cloned);
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
    public bool IsSpecial { get; }
    
    public string? StarMatched { get; set; }
    
    public Tile(string letters, TileModifierType modifier, int addedValue)
    {
        if (letters == "*") IsSpecial = true;
        Letters = letters;
        Modifier = modifier;
        AddedValue = addedValue;
    }

    public Tile(Tile tile)
    {
        Letters = tile.Letters;
        Modifier = tile.Modifier;
        AddedValue = tile.AddedValue;
        IsSpecial = tile.IsSpecial;
        StarMatched = tile.StarMatched;
    }
}