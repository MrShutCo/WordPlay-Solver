// // See https://aka.ms/new-console-template for more information
//
// using System.Diagnostics;
// using WordPlaySolver;
//
// var scorer = new Scorer(16);
// var watch = new Stopwatch();
//
// watch.Start();
// scorer.LoadWords("wordsfull.txt", 16);
// watch.Stop();
//
// Console.WriteLine($"Time to read all words and score them: {watch.ElapsedMilliseconds}ms");
//
// watch.Start();
// var tree = new Tree(scorer.Words.ToList(), scorer);
// watch.Stop();
//
// Console.WriteLine($"Time to load all words into tree: {watch.ElapsedMilliseconds}ms");
//
// var bag = "NOOELSDRTUTGBORN";
// var random = new Random();
//
// var state = new State(bag.ToCharArray());
// state.DrawHand(random, 16);
//
// state.PlayableTiles.Tiles[6].Modifier = TileModifierType.Red;
// state.PlayableTiles.Tiles[8].Modifier = TileModifierType.Red;
// state.PlayableTiles.Tiles[11].Modifier = TileModifierType.Red;
//
// // Slot multipliers
// // state.Modifiers.Add(new SlotMultiplier(3, 3));
// // state.Modifiers.Add(new SlotMultiplier(3, 1));
// // state.Modifiers.Add(new GenericAddBonus(8, (h, t) => true));
// // //state.Modifiers.Add(new GenericAddBonus(10, (h, t) => h.Tiles.Count == 6));
// // // If first and last are vowels
// // state.Modifiers.Add(new GenericMultBonus(2, (h, t) => Scorer.IsVowel(h.Tiles.First().Letter) && Scorer.IsVowel(h.Tiles.Last().Letter)));
// // // If first letter = last letter
// // state.Modifiers.Add(new GenericMultBonus(2, (h, t) =>
// // {
// //     var word = h.GetWord();
// //     return word.First() == word.Last();
// // }));
// state.Modifiers.Add(new GenericMultBonus(2, (hand, _) => !hand.GetWord().Contains("E")));
//
// scorer.ApplyUpgrades(state.Modifiers);
//
// watch.Start();
// var best = state.FindBestWordsInHand(tree, scorer, new SearchParameters()
// {
//     BestNResults = 5, 
//     MinLength = 4,
//     MaxLength = 16,
//     Prefix = "",
//     Suffix = "",
//     Contains = "",
//     //MaxWordScore = 10
// });
// watch.Stop();
//
// var words = best.Select(t => (t.hand.GetWord(), t.value)).ToArray();
//
// foreach (var word in words)
// {
//     Console.WriteLine($"{word.Item1} for {word.value} points");
// }
//
// Console.WriteLine($"Time to find best word: {watch.ElapsedMilliseconds}ms");
// //Console.WriteLine($"Best word: {best.hand.GetWord()} with value: {best.value}");
//
// // TODO:
// // - Add tile modifiers
// // - flat bonus
// // - gold tiles
// // - diamond tiles
// // - dotted tiles
// // - emerald tiles
// // - multiletter tiles
// // - Implement the . (hard)
// // - Implement the ! (easier)
// // - Add upgrades
// // [*] Show top N results
// // - Good UI