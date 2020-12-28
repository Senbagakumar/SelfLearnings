using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepration.Amazon.OnlineAssessment
{
    class TopKFrequentlyMentionedKeywords
    {
        //Top K Frequently Mentioned Keywords

        //Amazon Assessment -- 1 -- Top K Frequently Mentioned Keywords
        public static void TestMaximumToys()
        {
            int numToys = 6;
            int topToys = 2;
            string[] toys = new string[] { "elmo", "elsa", "legos", "tablet", "warcraft" };
            int numQuotes = 6;
            string[] quotes = new string[] {"Elmo is the hottest toy of the season! Elmo will be on every kid's wishlist!",
                    "The new Elmo dolls are super high quality.",
                    "Expect the Elsa dolls to be very popular this year",
                    "Elsa and Elmo are the toys i'll be buying for my kids",
                    "For parents of older kids, look into buying them a drone.",
                    "Warcraft is slowly rising in popularity ahead of the holiday season."};
            //var result = maximum_toys(numToys, topToys, toys, numQuotes, quotes.ToList());
            var result = GetTopKeywords(numToys, topToys, toys, numQuotes, quotes);
            //expected output: elmo, elsa

            toys = new string[] { "anacell", "cetracular", "betacellular" };
            quotes = new string[] {
                  "Anacell provides the best services in the city",
                  "betacellular has awesome services",
                  "Best services provided by anacell, everyone should use anacell",
            };

            //Output:
            //["anacell", "betacellular"]
            result = GetTopKeywords(numToys, topToys, toys, numQuotes, quotes);

            toys = new string[] { "anacell", "betacellular", "cetracular", "deltacellular", "eurocell" };
            quotes = new string[] {
                  "I love anacell Best services; Best services provided by anacell",
                  "betacellular has great services",
                  "deltacellular provides much better services than betacellular",
                  "cetracular is worse than anacell",
                  "Betacellular is better than deltacellular.",
                };
            //Output:
            //["betacellular", "anacell"]
            result = GetTopKeywords(numToys, topToys, toys, numQuotes, quotes);

            Console.Read();
        }

        private static List<string> GetTopKeywords(int k, int topToys, string[] keywords, int numQuotes, string[] reviews)
        {
            var keywordsDictionary = new Dictionary<string, int>();
            foreach (var key in keywords)
            {
                int count = reviews.Count(x => x.ToLower().Contains(key.ToLower()));
                keywordsDictionary.Add(key, count);
            }
            return keywordsDictionary.OrderByDescending(x => x.Value).Select(x => x.Key).Take(topToys).ToList();
        }
    }
}
