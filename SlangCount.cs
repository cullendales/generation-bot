using System;
using System.Collections.Generic;
using System.Linq;

namespace GenerationBot
{
    public static class SlangCount
    {
        // Track all words per user
        public static Dictionary<ulong, Dictionary<string, int>> UserWordCounts = new();

        // slang that is cool but I dont really use because Im unc
        private static readonly HashSet<string> ZoomerLanguage = new()
        {
            "cooked", "rizz", "aura", "frfr", "cap", "goon", "skibidi", "typeshit", "unc", "based", "bussin", "mid"
        };
        // epic slang + real af
        private static readonly HashSet<string> MillenialLanguage = new()
        {
            "wheeling", "epic", "dope", "basic", "savage", "af", "swole", "fire", "lit", "extra", "brah", "snack"
        };
        
        public static void CountWords(ulong userId, string message)
        {
            // eliminate all that whitespace
            var words = message.ToLower().Split(new char[] { ' ', '.', ',', '!', '?', ';', ':', '\n', '\r' },
                StringSplitOptions.RemoveEmptyEntries);

            if (!UserWordCounts.ContainsKey(userId))
                UserWordCounts[userId] = new Dictionary<string, int>();

            foreach (var word in words)
            {
                if (UserWordCounts[userId].ContainsKey(word))
                    UserWordCounts[userId][word]++;
                else
                    UserWordCounts[userId][word] = 1;
            }
        }
        
        public static int GetCount(ulong userId, string word)
        {
            word = word.ToLower();
            if (UserWordCounts.ContainsKey(userId) && UserWordCounts[userId].ContainsKey(word))
                return UserWordCounts[userId][word];
            return 0;
        }
        
        public static int GetZoomerCount(ulong userId)
        {
            if (!UserWordCounts.ContainsKey(userId))
                return 0;

            return UserWordCounts[userId]
                   .Where(kv => ZoomerLanguage.Contains(kv.Key))
                   .Sum(kv => kv.Value);
        }
        
        public static int GetMillennialCount(ulong userId)
        {
            if (!UserWordCounts.ContainsKey(userId))
                return 0;

            return UserWordCounts[userId]
                   .Where(kv => MillenialLanguage.Contains(kv.Key))
                   .Sum(kv => kv.Value);
        }
        
        public static string GetDominantCategory(ulong userId)
        {
            int zoomer = GetZoomerCount(userId);
            int millennial = GetMillennialCount(userId);

            if (zoomer == 0 && millennial == 0)
                return "You haven't used any slang yet, lil bro";

            if (zoomer > millennial)
                return "Zoomer";
            else if (millennial > zoomer)
                return "Millennial";
            else
                return "idk, you have used an equal amount of slang. You should conform to your gen better.";
        }
    }
}
