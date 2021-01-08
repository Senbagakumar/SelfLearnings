using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepration.Amazon.OnlineAssessment
{
    class KeywordSuggestions
    {
        //https://leetcode.com/problems/search-suggestions-system/

        //Given an array of strings products and a string searchWord.We want to design a system that suggests at most three product names from products after each character of 
        //searchWord is typed.Suggested products should have common prefix with the searchWord.If there are more than three products with a common prefix return the three 
        //lexicographically minimums products.

        //Return list of lists of the suggested products after each character of searchWord is typed.



        //Example 1:

        //Input: products = ["mobile", "mouse", "moneypot", "monitor", "mousepad"], searchWord = "mouse"
        //Output: [
        //["mobile","moneypot","monitor"],
        //["mobile","moneypot","monitor"],
        //["mouse","mousepad"],
        //["mouse","mousepad"],
        //["mouse","mousepad"]
        //]
        //Explanation: products sorted lexicographically = ["mobile","moneypot","monitor","mouse","mousepad"]
        //    After typing m and mo all products match and we show user["mobile", "moneypot", "monitor"]
        //After typing mou, mous and mouse the system suggests["mouse", "mousepad"]
        //Example 2:

        //Input: products = ["havana"], searchWord = "havana"
        //Output: [["havana"],["havana"],["havana"],["havana"],["havana"],["havana"]]
        //Example 3:

        //Input: products = ["bags","baggage","banner","box","cloths"], searchWord = "bags"
        //Output: [["baggage","bags","banner"],["baggage","bags","banner"],["baggage","bags"],["bags"]]
        //Example 4:

        //Input: products = ["havana"], searchWord = "tatiana"
        //Output: [[],[],[],[],[],[],[]]


        //https://leetcode.com/problems/search-suggestions-system/discuss/856171/C-Simple-Solution
        //https://leetcode.com/problems/search-suggestions-system/discuss/436119/Very-Easy-using-Linq-on-C

        //first solution

        public IList<IList<string>> SuggestedProducts(string[] products, string searchWord)
        {
            IList<IList<string>> ans = new List<IList<string>>();
            Array.Sort(products);
            Trie trie = new Trie();
            foreach (var w in products)
                trie.Add(w);

            TrieNode node = trie.root;
            foreach (var c in searchWord)
            {
                node = node?.Children == null ? null : node.Children.ContainsKey(c) ? node.Children[c] : null;

                if (node == null)
                    ans.Add(new List<string>());
                else
                    ans.Add(node.Words);
            }

            return ans;
        }


        class TrieNode
        {
            public Dictionary<char, TrieNode> Children = new Dictionary<char, TrieNode>();
            public List<string> Words = new List<string>();
        }

        class Trie
        {
            public TrieNode root = new TrieNode();

            public void Add(string word)
            {
                TrieNode cur = root;
                foreach (var c in word)
                {
                    if (!cur.Children.ContainsKey(c))
                        cur.Children.Add(c, new TrieNode());

                    cur = cur.Children[c];

                    if (cur.Words.Count < 3)
                        cur.Words.Add(word);
                }
            }
        }

        //second Solution
        public static IList<IList<string>> SuggestedProducts2(string[] products, string searchWord)
        {
            var filteredProducts = products.OrderBy(prod => prod).ToList();
            var resList = new List<IList<string>>();
            for (int len = 1; len <= searchWord.Length; len++)
            {
                string str = searchWord.Substring(0, len);
                filteredProducts = filteredProducts.Where(prod => prod.StartsWith(str)).ToList();
                resList.Add(filteredProducts.Take(3).ToList());
            }

            return resList;
        }
    }
}
