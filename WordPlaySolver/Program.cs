// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using WordPlaySolver;

var scorer = new Scorer(10);
var watch = new Stopwatch();

watch.Start();
scorer.LoadWords("Collins Scrabble Words (2019).txt", 10);
watch.Stop();

Console.WriteLine($"Time to read all words and score them: {watch.ElapsedMilliseconds}ms");

watch.Start();
var tree = new Tree(scorer.Words.ToList(), scorer);
watch.Stop();

Console.WriteLine($"Time to load all words into tree: {watch.ElapsedMilliseconds}ms");

Console.WriteLine(tree.ExistsThatBeginsWith("PP"));

var bag = "PFEIITTEMESIITZA";
var random = new Random();
    
var state = new State(bag.ToCharArray());
state.DrawHand(random, 16);

watch.Start();
var best = state.FindBestWordInHand(tree, scorer);
watch.Stop();

Console.WriteLine($"Time to find best word: {watch.ElapsedMilliseconds}ms");
Console.WriteLine($"Best word: {best.hand.GetWord()} with value: {best.value}");

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
// - Show top N results
// - Good UI