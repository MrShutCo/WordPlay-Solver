namespace WordPlaySolver;

public static class ModifierList
{
    public static List<Modifier> Modifiers =
    [
        // Common
        new Modifier("None", ModTimingType.None, Rarity.Common, 0),
        new Modifier("Generic Always True", ModTimingType.None, Rarity.Common, 0),
        new SlotMultiplier("2nd tile is 3x", 3, 1, Rarity.Common),
        new SlotMultiplier("4th tile is 3x", 3, 3, Rarity.Common),
        
        // Uncommon
        new SlotMultiplier("If first tile is vowel, 5x its value", 5, 0, Rarity.Uncommon, ModifierConditionals.FirstTileIsVowel),
        new GenericBonusAdditional("If word begins with _, +20 BP", 20, ModType.Add, Rarity.Uncommon, ModifierConditionals.WordBeginsWith, ""),
        
        // Rare
        // All the mults
        new GenericBonus("If every letter is unique, score 1.5x", 1.5, ModType.Mult, Rarity.Rare, ModifierConditionals.UniqueLetters),
        new GenericBonus("First and last letters are vowel, score 2x", 2, ModType.Mult, Rarity.Rare, ModifierConditionals.FirstLastLettersVowels),
        new GenericBonus("First letter = last, score 2x", 2, ModType.Mult, Rarity.Rare, ModifierConditionals.FirstLetterEqualsLast),
        new GenericBonus("First letter vowel, score 2x", 2, ModType.Mult, Rarity.Rare, ModifierConditionals.FirstLetterIsVowel),
        new GenericBonusAdditional("word begins with _, score 2x", 2, ModType.Mult, Rarity.Rare, ModifierConditionals.WordBeginsWith, ""),
        new GenericBonus("Identical letters in row, score 1.5x", 1.5, ModType.Mult, Rarity.Rare, ModifierConditionals.IdenticalAdjacentLetters),
        new GenericBonus("No adjacent consonants, score 2x", 2, ModType.Mult, Rarity.Rare, ModifierConditionals.NoAdjacentConsonants),
        new GenericBonusAdditional("ends begins with _, score 2x", 2, ModType.Mult, Rarity.Rare, ModifierConditionals.WordEndsWith, ""),
        new GenericBonus("At least 3 unique vowels, score 2x", 2, ModType.Mult, Rarity.Rare, ModifierConditionals.AtLeastThreeUniqueVowels),
        new GenericBonus("If more vowels than consonants, score 2x", 2, ModType.Mult, Rarity.Rare, ModifierConditionals.MoreVowelsThanConsonants),
        new GenericBonus("If no E, score 2x", 2, ModType.Mult, Rarity.Rare, ModifierConditionals.NoE),
        
        // TODO: implement 48-54
        
        // new Modifier("(N/A) 10 bonus points during special rounds", ModTimingType.None, Rarity.Uncommon, 0),
        // new Modifier("(N/A) Add 5 Bonus Points for each unused Upgrade", ModTimingType.None, Rarity.Uncommon, 0),
    ];
    
    public static Modifier GetModifierByName(string name) => Modifiers.First(s => s.Name == name);
}