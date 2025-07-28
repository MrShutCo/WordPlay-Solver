using System.Diagnostics;
using WordPlaySolver;

var scorer = new Scorer(16);
var watch = new Stopwatch();

watch.Start();
scorer.LoadWordsFromFile("wordsfull.txt", 16);
watch.Stop();

Console.WriteLine($"Time to read all words and score them: {watch.ElapsedMilliseconds}ms");

watch.Start();
var tree = new Tree(scorer.Words.ToList(), scorer);
watch.Stop();


Console.WriteLine($"Time to load all words into tree: {watch.ElapsedMilliseconds}ms");

string[] bag = ["Q","U","O","T","ING","ERS","QU","I","W","M","C","N","I","O","S","S"];
var state = new State(bag);
state.DrawHand(16);
    
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

// TODO:
// - Add tile modifiers
// - flat bonus
// - gold tiles
// - diamond tiles
// - dotted tiles
// - emerald tiles
// - multiletter tiles
// - Implement the . (hard)
// - Implement the ! (easier)
// - Add upgrades
// [*] Show top N results
// - Good UI