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

    var best = state.FindBestWordsInHand(tree, scorer, new SearchParameters()
    {
        BestNResults = 5, 
        MinLength = 4,
        MaxLength = 16,
        Prefix = "",
        Suffix = "",
        Contains = "",
    });
    watch.Stop();
    var words = best.Select(t => (t.hand.GetWord(), t.value)).ToArray();

    foreach (var word in words) 
    { 
        Console.WriteLine($"{word.Item1} for {word.value} points");
    }
    Console.WriteLine($"Time to find all words in {watch.ElapsedMilliseconds}ms");
}


//TestWords(["Q","U","O","T","ING","ERS","QU","I","W","M","C","N","I","O","S","S"];);

