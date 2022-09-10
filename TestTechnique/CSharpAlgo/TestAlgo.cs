using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpAlgo
{
    public static class TestAlgo
    {
        /// <summary>
        /// Une anagramme est un mot obtenu par transposition des lettres d'un autre
        /// mot (ex. Marie - Aimer)
        /// Retrouvez et affichez les anagrammes
        /// </summary>
        public static void TestAnagram()
        {
            var words = new string[] { "bao", "abab", "aob", "meteor", "remote", "thing", "night", "marie", "aimer"};

            var anagrams = new Dictionary<string, List<string>>();

            var processedIndexes = new HashSet<int>();

            for (var i = 0; i < words.Length; i++)
            {
                if (!processedIndexes.Contains(i))
                {
                    anagrams[words[i]] = new List<string>();
                    for (int j = i + 1; j < words.Length; j++)
                    {
                        if (AreAnagram(words[i], words[j]))
                        {
                            anagrams[words[i]].Add(words[j]);
                            processedIndexes.Add(j); /* No need to add current 'i' */
                        }
                    }
                }
            }

            //Console.Write anagrams
        }

        public static bool AreAnagram(string a, string b)
        {
            if (string.IsNullOrWhiteSpace(a) || string.IsNullOrWhiteSpace(b))
                return false;

            if (a.Length != b.Length)
                return false;

            /* Except works because length is already checked ! Otherwise, even 'a' subset of 'b' will work. */
            return a.ToCharArray().Except(b.ToCharArray()).Count() == 0;
        }

        /// <summary>
        /// Regrouper les mots en deux groupes : Palindromes ou Pas palindrome
        /// </summary>
        public static void TestPalindrome()
        {
            var words = new string[] {"madam", "test", "tenet", "okapi", "bob"};

            var palindromes = new List<string>();
            var others = new List<string>();

            foreach (var word in words)
            {
                if (string.IsNullOrWhiteSpace(word))
                {
                    others.Add(word);
                    continue;
                }

                var length = word.Length;

                if (length == 1)
                {
                    others.Add(word);
                    continue;
                }

                int i;
                for (i = 0; i <= length / 2; i++)
                {
                    var j = (length - 1) - i;

                    if (word[i] != word[j])
                    {
                        others.Add(word);
                        i = length;
                    }
                }

                if (i == length)
                {
                    others.Add(word);
                    continue;
                }

                palindromes.Add(word);
            }
        }

        private static bool IsPalandrome(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            var length = input.Length;

            if (length == 1)
                return true;
            
            for (var i = 0; i <= length / 2; i++)
            {
                var j = (length - 1) - i;

                if (input[i] != input[j])
                    return false;
            }

            return true;
        }

        /// <summary>
        /// En utilisant UNIQUEMENT Linq afficher le nombre
        /// d'occurences de chaque charactère dans la phrase
        /// </summary>
        public static void TestLinqCountOccurence()
        {
            var message = "Welcome to citeo my friend";

            var occurences = message
                .ToCharArray()
                .GroupBy(x => x)
                .Select(g => new { Key = g.Key, Count = g.Count() });
        }
    }
}
