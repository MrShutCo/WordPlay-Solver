namespace WordPlaySolver;

public class Tree
{
    private Node[] _initialLetters;

    public Tree(List<string> words)
    {
        _initialLetters = new Node[26];
        List<char> letters = ['A', 'B', 'C', 'D', 'E', 'F', 'G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'];
        for (int i = 0; i < 26; i++)
        {
            _initialLetters[i] = new Node(letters[i]);
        }

        foreach (var word in words)
        {
            AddWord(word);
        }
    }

    public bool ExistsThatBeginsWith(Tile[] tiles)
    {
        if (tiles.Length == 0) return true;
        var letters = new string(Hand.GetCharsFromTiles(tiles));
        var initialIndex = CharToIndex(letters[0]);
        if (initialIndex < 0 || initialIndex >= _initialLetters.Length) throw new IndexOutOfRangeException();
        
        //var curr = _initialLetters[initialIndex];
        // for (int i = 1; i < letters.Length; i++)
        // {
        //     var found = curr.NextLetters.TryGetValue(letters[i], out var next);
        //     if (!found) return false;
        //     curr = next;
        // }
        return Matches(_initialLetters[initialIndex], letters, 1, false);
    }
    
    public bool TryGetWord(string word)
    {
        var initialIndex = CharToIndex(word[0]);
        if (initialIndex < 0 || initialIndex >= _initialLetters.Length) throw new IndexOutOfRangeException();
        
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
        return Matches(_initialLetters[initialIndex], word, 1, true);
    }

    private bool Matches(Node node, string word, int index, bool checkTerminating)
    {
        while (index < word.Length)
        {
            // var curr = word[index];
            // if (curr == '*')
            // {
            //     foreach (var next in node.NextLetters)
            //     {
            //         if (Matches(next.Value, word, index + 1))
            //         {
            //             return true;
            //         }
            //     }
            //
            //     return false;
            // }

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
        var curr = _initialLetters[CharToIndex(word[0])];
        for (int i = 1; i < word.Length; i++)
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