# Word Play Solver

Solver for the game "Word Play", use if you want to remove the challenge from the game and focus solely on getting the highest score. Written in C# with blazor and compiled using AOT. Apologies in advance for the somewhat large site (50MB I believe, not 100% sure)

Disclaimer: Not affiliated with the creator GMTK or the game, this is just an experiment

## Features

- 4x4 grid of tiles that can be easily set
- Supports input ING, ERS, QU (by using `,` `.` and `/` resp)
- Outputs the N highest word and their scores based on settings and modifiers
- All tiles are scored based on in-game scores, including length bonuses
- Can control min/max word length, useful for particular challenges
- Control max score, useful when you just need a few points to get to the next stage
- Can force any prefix/suffix/substring in the best results
- Handful of modifiers are implemented, including most score multipliers and a few others
- Selecting "Generic Always True" modifier gives you control over the additional / multiplication bonuses, useful for any passive bonuses that give flat amounts

## Future Developments

- [ ] Support for the additional 4 tile slots
- [ ] Implementation of more modifiers (some of them are quite niche and require special workflows)
- [ ] Support for wild cards `*`
- [ ] Support for `!`
- [ ] Support for `+` (unsure about this one, runtime may increase significantly)
- [ ] Support for different tile types
  - Biggest issue here is how to efficiently set them in UI. I was thinking of reserving digits 1-8 for setting the tile types but very welcome to suggestions about this
- [ ] Upgradeable tiles (this may be too painful to set properly)
- [ ] Better UI. Graphic design is **not** my passion :( (looking for help / suggestions for this one!)
- [ ] Better code documentation and cleanup
- [ ] Memory / performance improvements
  - Likely some improvements in the recursive enumeration for less memory usage or better pruning techniques
  - Less memory usage overall. It shouldn't be too much that it crashes browser, but the Trie structure seems to be about 250MB or so

I think that some features will forever remain unreachable, such as modelling some upgrades like swapping a tile with a consonant just due to the increased runtime (it would take 16*6=96x longer to compute unless theres some smarter way to do this).

Not all modifiers will be added to the solver, since some of them affect non-score things or just give passive +X bonuses and just add bloat.

## Some Technicals
Theres about 190k words all stored in a Trie, which supports O(L) lookup for valid words where L is the length of the word. The words should match the current 1.07 version of the game.

Enumeration of all permutations of the tiles is doing using an IEnumerable so as to prevent the loading of all words in memory at once. Significant pruning is done as well, where as we build longer and longer strings, if there is no word with the given prefix, we terminate the search early.

The data structure used for storing the N best results just stores them in a list and removes the worst one if the new word is better than it, but this could probably be reimplemented as a maxheap if we wanted to increase the number of results returned without tanking runtime.

Since its all C# compiled to WASM using AOT, there will be some performance differences between the Test project and the UI.

## Building Locally

TBD

## Contributing

Very open to suggestions and contributions!

Also shoutout to viablebunnybudd for writing https://steamcommunity.com/sharedfiles/filedetails/?id=3532673231 which painstakingly lists every modifier, upgrade, and gift. The modifiers would certainly take way longer without this guide