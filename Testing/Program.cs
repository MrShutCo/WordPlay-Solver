using System.Diagnostics;
using WordPlaySolver;

var scorer = new Scorer(16);
var watch = new Stopwatch();

watch.Start();
scorer.LoadWordsFromFile("wordsfull.txt", 16);
watch.Stop();

Console.WriteLine($"Time to read all words and score them: {watch.ElapsedMilliseconds}ms");

watch.Start();
var tree = new Tree(scorer.Words.ToList());
watch.Stop();


Console.WriteLine($"Time to load all words into tree: {watch.ElapsedMilliseconds}ms");

void TestWords(string[] bag)
{

    var state = new State(bag);
    state.DrawHand(bag.Length);
    
    watch.Start();
    
    state.Modifiers.Add(ModifierList.GetModifierByName("2nd tile is 3x"));
    var second = (GenericBonusAdditional)ModifierList.GetModifierByName("If word begins with _, +20 BP");
    second.Additional = "D";
    state.Modifiers.Add(second);
    state.Modifiers.Add(ModifierList.GetModifierByName("If no E, score 2x"));

    var best = state.FindBestWordsInHand(tree, scorer, new SearchParameters()
    {
        BestNResults = 9, 
        MinLength = 7,
        MaxLength = 10,
        Prefix = "",
        Suffix = "",
        Contains = "",
        MaxWordScore = 55
    });
    watch.Stop();
    var words = best.Select(t => (t.hand.GetWord(), t.value)).ToArray();

    foreach (var word in words) 
    { 
        Console.WriteLine($"{word.Item1} for {word.value} points");
    }
    Console.WriteLine($"Time to find all words in {watch.ElapsedMilliseconds}ms");
}


//TestWords(["Q","U","O","T","ING","ERS","QU","I","W","M","C","N","I","O","S","S"]);
//TestWords(["Q","U","O","T","ING","ERS","QU","*","W","M","C","N","*","O","S","S"]);

//TestWords(["ING", "ERS", "QU", "A", "S","D","*","E","N","C","A","S","D","Q","*", "E"]);

// Very slow in UI
TestWords([
    "Q", "I", "O", "P", 
    "E","R","ING","S",
    "D","O","D","ERS",
    "O","I","P","S"]);