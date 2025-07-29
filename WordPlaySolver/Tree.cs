namespace WordPlaySolver;

public class Tree
{
    private readonly Node _root;

    public Tree(List<string> words)
    {
        _root = new Node();
        //List<char> letters = ['A', 'B', 'C', 'D', 'E', 'F', 'G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'];
        
        foreach (var word in words)
        {
            AddWord(word);
        }
    }

    public bool ExistsThatBeginsWith(Tile[] tiles)
    {
        // if (tiles.Length == 0) return true;
        var letters = new string(Hand.GetCharsFromTiles(tiles));
        // var initialIndex = CharToIndex(letters[0]);
        // if (initialIndex < 0 || initialIndex >= _initialLetters.Length) throw new IndexOutOfRangeException();
        
        //var curr = _initialLetters[initialIndex];
        // for (int i = 1; i < letters.Length; i++)
        // {
        //     var found = curr.NextLetters.TryGetValue(letters[i], out var next);
        //     if (!found) return false;
        //     curr = next;
        // }
        Span<char> starBuffer = stackalloc char[16];
        var matches = Matches(_root, letters, 0, false, starBuffer, 0);
        int numMatched = 0;
        foreach (var tile in tiles)
        {
            if (tile.IsSpecial)
            {
                tile.StarMatched = starBuffer[numMatched].ToString();
                numMatched++;
            }
        }

        return matches;
    }
    
    public bool TryGetWord(string word)
    {
        // var initialIndex = CharToIndex(word[0]);
        // if (initialIndex < 0 || initialIndex >= _initialLetters.Length) throw new IndexOutOfRangeException();
        
        // var curr = _initialLetters[initialIndex];
        // for (int i = 1; i < word.Length; i++)
        // {
        //     curr.NextLetters.TryGetValue(word[i], out var next);
        //     if (next == null) return false;
        //     curr = next;
        // }
        //
        // //value = curr.Score;
        // return curr.IsTerminator;
        Span<char> starBuffer = stackalloc char[16];
        return Matches(_root, word, 0, true, starBuffer, 0);
    }

    private bool Matches(Node node, string word, int index, bool checkTerminating, Span<char> buffer, int bufferIndex)
    {
        while (index < word.Length)
        {
            var curr = word[index];
            if (curr == '*')
            {
                foreach (var next in node.NextLetters)
                {
                    buffer[bufferIndex] = next.Key;
                    if (Matches(next.Value, word, index + 1, checkTerminating, buffer, bufferIndex+1))
                    {
                        return true;
                    }
                }
            
                return false;
            }

            node.NextLetters.TryGetValue(word[index], out var nextNode);
            if (nextNode == null) return false;
            node = nextNode;
            index += 1;
        }

        return !checkTerminating || node.IsTerminator;
    }

    public static int CharToIndex(char ch)
    {
        return ch - 'A';
    }
    
    void AddWord(string word)
    {
        var curr = _root;
        for (int i = 0; i < word.Length; i++)
        {
            if (!curr.NextLetters.ContainsKey(word[i]))
            {
                curr.NextLetters[word[i]] = new Node(word[i]);
            }
            
            curr = curr.NextLetters[word[i]];
        }
        curr.IsTerminator = true;
    }
}

public class Node
{
    public char Letter { get; set; }
    public int Count { get; set; }
    public bool IsTerminator { get; set; }
    public Dictionary<char, Node> NextLetters { get; set; }

    public Node(char letter)
    {
        Letter = letter;
        NextLetters = new();
        IsTerminator = false;
        Count = 1;
        //Score = 0;
    }

    public Node()
    {
        IsTerminator = true;
        NextLetters = new ();
    }
}