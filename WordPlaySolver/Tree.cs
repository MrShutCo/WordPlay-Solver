namespace WordPlaySolver;

public class Tree
{
    private Node[] _initialLetters;

    public Tree(List<string> words, Scorer scorer)
    {
        _initialLetters = new Node[26];
        List<char> letters = ['A', 'B', 'C', 'D', 'E', 'F', 'G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'];
        for (int i = 0; i < 26; i++)
        {
            _initialLetters[i] = new Node(letters[i]);
        }

        foreach (var word in words)
        {
            AddWord(word, scorer);
        }
    }

    public bool ExistsThatBeginsWith(string prefix)
    {
        if (prefix == "") return true;
        var initialIndex = CharToIndex(prefix[0]);
        if (initialIndex < 0 || initialIndex >= _initialLetters.Length) throw new IndexOutOfRangeException();
        
        var curr = _initialLetters[initialIndex];
        for (int i = 1; i < prefix.Length; i++)
        {
            var next = curr.NextLetters[CharToIndex(prefix[i])];
            if (next == null) return false;
            curr = next;
        }

        return true;
    }

    public bool ExistsThatBeginsWith(Tile[] tiles)
    {
        if (tiles.Length == 0) return true;
        var initialIndex = CharToIndex(tiles[0].Letter);
        if (initialIndex < 0 || initialIndex >= _initialLetters.Length) throw new IndexOutOfRangeException();
        
        var curr = _initialLetters[initialIndex];
        for (int i = 1; i < tiles.Length; i++)
        {
            var next = curr.NextLetters[CharToIndex(tiles[i].Letter)];
            if (next == null) return false;
            curr = next;
        }

        return true;
    }
    
    public bool TryGetWord(string word)
    {
        var initialIndex = CharToIndex(word[0]);
        if (initialIndex < 0 || initialIndex >= _initialLetters.Length) throw new IndexOutOfRangeException();
        
        var curr = _initialLetters[initialIndex];
        for (int i = 1; i < word.Length; i++)
        {
            var next = curr.NextLetters[CharToIndex(word[i])];
            if (next == null) return false;
            curr = next;
        }

        //value = curr.Score;
        return curr.IsTerminator;
    }

    int CharToIndex(char ch)
    {
        return ch - 'A';
    }
    
    void AddWord(string word, Scorer scorer)
    {
        var curr = _initialLetters[CharToIndex(word[0])];
        for (int i = 1; i < word.Length; i++)
        {
            curr.Count++;
            var nextIdx = CharToIndex(word[i]);
            curr.NextLetters[nextIdx] ??= new Node(word[i]);
            curr = curr.NextLetters[nextIdx];
        }
        curr.IsTerminator = true;
        //curr.Score = scorer.GetScore(word);
    }
}

public class Node
{
    public char Letter { get; set; }
    public int Count { get; set; }
    //public int Score { get; set; }
    public bool IsTerminator { get; set; }
    public Node?[] NextLetters { get; set; }

    public Node(char letter)
    {
        Letter = letter;
        NextLetters = new Node[26];
        IsTerminator = false;
        Count = 1;
        //Score = 0;
    }

    public Node()
    {
        IsTerminator = true;
        NextLetters = new Node[26];
    }
}