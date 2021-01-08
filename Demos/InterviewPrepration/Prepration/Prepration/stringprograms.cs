using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.HtmlControls;
using System.Xml;

namespace Prepration
{
    class WordNode
    {
        public int numSteps;
        public string word;
        public WordNode(string word, int numSteps)
        {
            this.word = word;
            this.numSteps = numSteps;
        }
    }

    class stringprograms
    {

        //1. https://leetcode.com/articles/is-subsequence/
        // Subsequence
        // Input: s = "abc", t = "ahbgdc" Output: true, Input: s = "axc", t = "ahbgdc" Output: false
        public static bool IsSubsequence(string sub, string whole)
        {
            int i = 0;
            int j = 0;
            while (j < sub.Length)
            {
                while (i < whole.Length)
                {
                    if (sub[j] == whole[i])
                        j++;
                    i++;
                }
                if (j == sub.Length)
                    return true;
                else
                    return false;
            }
            return false;
        }

        //1. 1 https://leetcode.com/articles/repeated-substring-pattern/
        //Given a non-empty string check if it can be constructed by taking a substring of it and appending multiple copies of the substring together
        //Input: "abab" Output: True Explanation: It's the substring "ab" twice; Input: "aba" Output: False
        public static bool RepeatedSubstringPattern(string s)
        {
            // var v = (s + s); //abababab
            // var v1 = v.Substring(1, 2 * s.Length - 1); //Now let's cut the first and the last characters in the doubled string- EX: bababa
            // bool res = v1.Contains(s);
            // return res;

            int n = s.Length;
            int[] dp = new int[n];
            // Construct partial match table (lookup table).
            // It stores the length of the proper prefix that is also a proper suffix.
            // ex. ababa --> [0, 0, 1, 2, 1]
            // ab --> the length of common prefix / suffix = 0
            // aba --> the length of common prefix / suffix = 1
            // abab --> the length of common prefix / suffix = 2
            // ababa --> the length of common prefix / suffix = 3

            //ababa => 0,0,1,2,
            for (int i = 1; i < n; ++i)
            {
                int j = dp[i - 1];
                while (j > 0 && s[i] != s[j])
                {
                    j = dp[j - 1];
                }
                if (s[i] == s[j])
                {
                    ++j;
                }
                dp[i] = j;
            }

            int l = dp[n - 1];
            // check if it's repeated pattern string
            bool res= l != 0 && (n % (n - l) == 0);
            return res;

        }

        //1.2 https://leetcode.com/problems/find-first-and-last-position-of-element-in-sorted-array/
        //Given an array of integers nums sorted in ascending order, find the starting and ending position of a given target value
        //Input: nums = [5,7,7,8,8,10], target = 8 Output: [3,4]
        //Input: nums = [5,7,7,8,8,10], target = 6 Output: [-1,-1]
        public int[] SearchRange(int[] nums, int target)
        {
            int left = LeftBoundary(nums, target);
            //here perform the boundary check
            if (left >= nums.Length || nums[left] != target)
                return new int[] { -1, -1 };
            int right = LeftBoundary(nums, target + 1);
            return new int[] { left, right - 1 };
        }

        private int LeftBoundary(int[] nums, int target)
        {
            int left = 0;
            //int right = nums.Length - 1;
            int right = nums.Length;
            while (left <= right)
            {
                //int mid = (right - left) / 2 + left;
                int mid = (left + right) / 2;
                if (nums[mid] < target)
                {
                    left = mid + 1;
                }
                else if (nums[mid] > target || nums[mid] == target)
                {
                    right = mid - 1;
                }
            }
            return left;
        }

        

        //1.4 https://leetcode.com/problems/subsets/
        //Given a set of distinct integers, nums, return all possible subsets (the power set).
        //Input: nums = [1,2,3]
        //Output:
        //[
        //  [3],
        //  [1],
        //  [2],
        //  [1,2,3],
        //  [1,3],
        //  [2,3],
        //  [1,2],
        //  []
        //]

        //Input: [1,2,2]
        //Output:
        //[
        //  [2],
        //  [1],
        //  [1,2,2],
        //  [2,2],
        //  [1,2],
        //  []
        //]

        public List<List<int>> Subsets(int[] nums)
        {
            List<List<int>> list = new List<List<int>>();
            Array.Sort(nums);
            backtrack(list, new List<int>(), nums, 0);
            return list;
        }

        private void backtrack(List<List<int>> list, List<int> tempList, int[] nums, int start)
        {
            list.Add(new List<int>(tempList));
            for (int i = start; i < nums.Length; i++)
            {
                if (i > start && nums[i] == nums[i - 1]) continue; // skip duplicates if you have in the input
                tempList.Add(nums[i]);
                backtrack(list, tempList, nums, i + 1);
                tempList.RemoveAt(tempList.Count - 1);
            }
        }

        //https://leetcode.com/problems/permutations/
        //Given a collection of distinct integers, return all possible permutations.
        //Input: [1,2,3]
        // Output:
        //[
        //  [1,2,3],
        //  [1,3,2],
        //  [2,1,3],
        //  [2,3,1],
        //  [3,1,2],
        //  [3,2,1]
        //]

        //Input: [1,1,2]
        //Output:
        //[
        //  [1,1,2],
        //  [1,2,1],
        //  [2,1,1]
        //]

        public List<List<int>> permute(int[] nums)
        {
            List<List<int>> list = new List<List<int>>();
            // Arrays.sort(nums); // not necessary
            pbacktrack(list, new List<int>(), nums);
            return list;
        }

        private void pbacktrack(List<List<int>> list, List<int> tempList, int[] nums)
        {
            if (tempList.Count == nums.Length)
            {
                list.Add(new List<int>(tempList));
            }
            else
            {
                for (int i = 0; i < nums.Length; i++)
                {
                    if (tempList.Contains(nums[i])) continue; // element already exists, skip
                    tempList.Add(nums[i]);
                    pbacktrack(list, tempList, nums);
                    tempList.RemoveAt(tempList.Count - 1);
                }
            }
        }

        public List<List<int>> permuteUnique(int[] nums)
        {
            List<List<int>> list = new List<List<int>>();
            Array.Sort(nums);
            backtrack(list, new List<int>(), nums, new bool[nums.Length]);
            return list;
        }

        private void backtrack(List<List<int>> list, List<int> tempList, int[] nums, bool[] used)
        {
            if (tempList.Count == nums.Length)
            {
                list.Add(new List<int>(tempList));
            }
            else
            {
                for (int i = 0; i < nums.Length; i++)
                {
                    if (used[i] || i > 0 && nums[i] == nums[i - 1] && !used[i - 1]) continue;
                    used[i] = true;
                    tempList.Add(nums[i]);
                    backtrack(list, tempList, nums, used);
                    used[i] = false;
                    tempList.RemoveAt(tempList.Count - 1);
                }
            }
        }

        //https://leetcode.com/problems/combination-sum/
        //Given a set of candidate numbers (candidates) (without duplicates) and a target number (target), find all unique combinations in candidates where the candidate numbers sums to target.
        //The same repeated number may be chosen from candidates unlimited number of times.
        //Input: candidates = [2,3,6,7], target = 7,
        //A solution set is:
        //[
        //  [7],
        //  [2,2,3]
        //]

        //can't reuse same element
        //Input: candidates = [2,3,5], target = 8,
        //A solution set is:
        //[
        //  [2,2,2,2],
        //  [2,3,3],
        //  [3,5]
        //]

        public List<List<int>> combinationSum(int[] nums, int target)
        {
            List<List<int>> list = new List<List<int>>();
            Array.Sort(nums);
            backtrack(list, new List<int>(), nums, target, 0);
            return list;
        }

        private void backtrack(List<List<int>> list, List<int> tempList, int[] nums, int remain, int start)
        {
            if (remain < 0) return;
            else if (remain == 0) list.Add(new List<int>(tempList));
            else
            {
                for (int i = start; i < nums.Length; i++)
                {
                    if (i > start && nums[i] == nums[i - 1]) continue; // skip duplicates: can't reuse same element
                    tempList.Add(nums[i]);
                    backtrack(list, tempList, nums, remain - nums[i], i); // not i + 1 because we can reuse same elements
                    tempList.RemoveAt(tempList.Count - 1);
                }
            }
        }

        //https://leetcode.com/problems/palindrome-partitioning/
        //Given a string s, partition s such that every substring of the partition is a palindrome.
        //Return all possible palindrome partitioning of s.

        //Input: "aab"
        //Output:
        //[
        //  ["aa","b"],
        //  ["a","a","b"]
        //]

        public List<List<string>> partition(string s)
        {
            List<List<string>> list = new List<List<string>>();
            backtrack(list, new List<string>(), s, 0);
            return list;
        }

        private void backtrack(List<List<string>> list, List<string> tempList, string s, int start)
        {
            if (start == s.Length)
                list.Add(new List<string>(tempList));
            else
            {
                for (int i = start; i < s.Length; i++)
                {
                    if (isPalindrome(s, start, i))
                    {
                        tempList.Add(s.Substring(start, i + 1-start));
                        backtrack(list, tempList, s, i + 1);
                        tempList.RemoveAt(tempList.Count - 1);
                    }
                }
            }
        }

        private bool isPalindrome(String s, int low, int high)
        {
            while (low < high)
                if (s[low++] != s[high--]) return false;
            return true;
        }

        //https://www.programcreek.com/2012/12/leetcode-evaluate-reverse-polish-notation/
        //2. ["2", "1", "+", "3", "*"] -> ((2 + 1) * 3) -> 9

        public static int CalculateValues()
        {
            string[] inputs = new string[] { "2", "1", "+", "3", "*" };
            string operators = "+/*-";
            int result = 0;
            var stack = new Stack<string>();
            foreach (var inp in inputs)
            {
                if (!operators.Contains(inp))
                    stack.Push(inp);
                else
                {
                    int first = int.Parse(stack.Pop());
                    int second = int.Parse(stack.Pop());

                    switch (inp)
                    {
                        case "+":
                            stack.Push((first + second).ToString());
                            break;
                        case "-":
                            stack.Push(Math.Abs(first - second).ToString());
                            break;
                        case "*":
                            stack.Push((first * second).ToString());
                            break;
                        case "/":
                            stack.Push((first / second).ToString());
                            break;
                    }
                }
            }
            result = Convert.ToInt16(stack.Pop());
            return result;

        }

        //3. MiniParser  Given s = "[123,[456,[789]]]"
        //https://www.programcreek.com/2014/08/leetcode-mini-parser-java/


        //4. Isomorphic Strings "egg" and "add" are isomorphic, "foo" and "bar" are not
        public static bool IsIsomorphic(string word1, string word2)
        {
            if (word1.Length != word2.Length) return false;
            var dict1 = new Dictionary<char, char>();
            var dict2 = new Dictionary<char, char>();

            for (int i = 0; i < word1.Length; i++)
            {
                char c1 = word1[i];
                char c2 = word2[i];
                if (dict1.Keys.Contains(c1))
                {
                   
                    if (c1 != dict2[c2])
                        return false;
                }
                else
                {
                    dict1.Add(c1, c2); 
                    if (dict2.Keys.Contains(c2))
                    {
                        return false;
                    }
                    else
                    {
                        dict2.Add(c2, c1);
                    }
                }
            }
            return true;

        }

        //Amazon Question
        //6. WordLadder 
        // start = "hit" end = "cog" dict = ["hot","dot","dog","lot","log"] output =  "hit" -> "hot" -> "dot" -> "dog" -> "cog"
        public static int NoOfStepsForWordLadder(string beginWord, string endWord, List<string> noOfWord)
        {
            Queue<WordNode> words = new Queue<WordNode>();
            words.Enqueue(new WordNode(beginWord, 1));
            noOfWord.Add(endWord);

            while (words.Count > 0)
            {
                WordNode word = words.Dequeue();

                var currentWord = word.word;

                if (currentWord.Equals(endWord))
                    return word.numSteps;

                char[] chars = currentWord.ToCharArray();
                for (int i = 0; i < chars.Length; i++)
                {
                    for (char cv = 'a'; cv <= 'z'; cv++)
                    {
                        var temp = chars[i];
                        if (chars[i] != cv)
                            chars[i] = cv;

                        string newWord = new string(chars);
                        if (noOfWord.Contains(newWord))
                        {
                            words.Enqueue(new WordNode(newWord, word.numSteps + 1));
                            noOfWord.Remove(newWord);
                        }
                        chars[i] = temp;

                    }
                }
            }
            return 0;

        }

        //7. Find the kth largest element in an unsorted array
        //given [3,2,1,5,6,4] and k = 2, return 5
        public static int FindKthLargestElement(int[] array, int k)
        {
            //Solution 1:
            //Array.Sort(array);
            //int ret = array[array.Length - k];
            //return ret;

            //Solution 2:
            //ManualSort
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = i; j < array.Length; j++)
                {
                    if (array[i] > array[j])
                    {
                        var temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }

                }
            }
            return array[array.Length - k];
        }

        //8. MakeTheNumbersMatch
        public static void MakeTheNumbersMatch(int a, int b, int x, int y)
        {
            while (a != x && b != y)
            {
                if (a > x)
                {
                    a--;
                }
                else
                {
                    a++;
                }

                if (b > y)
                {
                    b--;
                }
                else
                {
                    b++;
                }
            }
        }

        //9. Find the duplicates from Given String
        //1. Use dictionary.
        public static void FindDuplicatesUseDictionary(string inputString)
        {
            var wordCount = new Dictionary<char, int>();
            string s = string.Empty;
            foreach (var c in inputString)
            {
                if (c == ' ') continue;
                if (wordCount.Keys.Contains(c))
                {
                    wordCount[c]++;
                }
                else
                {
                    s += c;
                    wordCount.Add(c, 1);
                }
            }
            //var v= wordCount.OrderBy(q => q.Value);
            Console.WriteLine($"The Unique InputString is => {s}");
            // Console.WriteLine($"The maximum duplicate word is {wordCount.Last().Key}");
        }

        //2. Use string 
        public static void FindDuplicatesUseIndex(string inputString)
        {
            string s = string.Empty;
            foreach (char inp in inputString)
            {
                if (s.IndexOf(inp) < 0)
                {
                    s += inp;
                }
            }
        }

        //Amazon Qustion
        //10. SubstringPattern
        public static bool SubStringPattern(string input, string substring)
        {
            //HiHowHowareyou
            //Howa
            bool isMatched = false;
            int i = 0, j = 0, k = 0;
            while (i < input.Length)
            {
                while (j < substring.Length)
                {
                    if (i == input.Length)
                        break;

                    if (input[i] == substring[j])
                    {
                        i++; j++;
                    }
                    else
                    {
                        j = 0;
                        k++;
                        i = k;
                    }
                }

                if (j == substring.Length)
                {
                    isMatched = true;
                    break;
                }
            }
            return isMatched;

        }

        //https://www.geeksforgeeks.org/naive-algorithm-for-pattern-searching/
        public static void SubStringMatch()
        {
            string input = "abxabcabcaby", pattern = "abcaby";
            //string input = "HiHowHowareyouT", pattern = "Howa";
            //string input= "THIS IS A TEST TEXT",pattern= "TEXT";
            int ip = input.Length;
            int pl = pattern.Length;

            for(int i=0; i<=ip-pl; i++)
            {
                int j;
                for(j=0; j<pl; j++)
                {
                    if (input[i + j] != pattern[j])
                        break;
                }

                if(j==pl)
                {
                    Console.WriteLine("found the pattern=" +i);
                }
            }
        }

        
        // 20. Length Encoding InputString="wwwwaaadexxxxxxywww" output="w4a3d1e1x6y1w3"
        public static string LengthEncoding(string input)
        {
            int length = input.Length;
            string result = string.Empty;
            for (int i = 0; i < length; i++)
            {
                int count = 1;
                while (i < length - 1 && input[i] == input[i + 1])
                {
                    i++;
                    count++;
                }
                result += input[i] + count.ToString();
            }
            return result;
        }

        //https://leetcode.com/problems/string-compression/
        //Input: chars = ["a","a","b","b","c","c","c"]
        //Output: Return 6, and the first 6 characters of the input array should be: ["a","2","b","2","c","3"]
        //Explanation: The groups are "aa", "bb", and "ccc". This compresses to "a2b2c3".
        public static int StringCompression(string[] input)
        {
            int length = input.Length;
            string result = string.Empty;
            for (int i = 0; i < length; i++)
            {
                int count = 1;
                while (i < length - 1 && input[i] == input[i + 1])
                {
                    i++;
                    count++;
                }
                if (count > 1)
                    result += input[i] + count.ToString();
                else
                    result += input[i];
            }
            return result.Length;
        }

        //https://www.geeksforgeeks.org/anagram-substring-search-search-permutations/

        // 21. Remove duplicates
        //https://www.geeksforgeeks.org/remove-duplicates-from-a-given-string/
        public static string RemoveDuplicates(string input)
        {
            //int index = 0;
            //char[] ins = input.ToCharArray();
            ////wwwwaaadexxxxxxywww --> wadexy
            //for (int i = 0; i < ins.Length; i++)
            //{
            //    int j;
            //    for (j = 0; j < i; j++)
            //    {
            //        if (ins[i] == ins[j])
            //        {
            //            break;
            //        }
            //    }
            //    if (i == j)
            //    {
            //        ins[index++] = ins[i];
            //    }
            //}
            //var res = new char[index];
            //Array.Copy(ins, res, index);
            //var result = new string(res);
            //return result;
            char[] array = input.ToCharArray();
            int j = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if(array[i] != array[j])
                    j++;
                array[j] = array[i];
            }
            var res = new char[j];
            Array.Copy(array, res, j);
            var result = new string(res);
            return result;
        }

        //https://www.geeksforgeeks.org/anagram-substring-search-search-permutations/


        //21. Get Length of Unique Substring 
        //Input: "pwwkew" Output: 3
        //longest substring without repeating characters
        //https://leetcode.com/explore/interview/card/amazon/76/array-and-strings/2961/
        //https://leetcode.com/problems/longest-substring-without-repeating-characters/
        public static int GetMaxSubstring(string input)
        {
            //string input = "abcabababcd";
            int[] ar = new int[128];
            int j = 0, ans = 0;
            string result = string.Empty;
            for (int i = 0; i < input.Length; i++)
            {
                j = Math.Max(j, ar[input[i]]);
                if (i - j + 1 > ans)
                {
                    ans = i - j + 1;
                    result = input.Substring(j, ans);
                }
                ar[input[i]] = (i + 1);
            }
            return ans;

        }


        public string MinimumWindowSubstring(string inputString, string substring)
        {
            //inputString: abdzfgabz substring: az result: 3

            int j = 0;
            string windowString = string.Empty;
            int length = 0;
            for(int i=0; i<inputString.Length; i++)
            {
                string gs = inputString.Substring(j, i - j + 1);
                string isPresent = IsSubstring(gs, substring);
                if (isPresent == "NothingPresent")
                    j++;
                if (isPresent == "BothPresent")
                {
                    length = i - j + 1;
                    j = i;
                    windowString = gs;
                }
            }
            return windowString;

        }

        private string IsSubstring(string gs, string substring)
        {
            string defaultValue = "NothingPresent";
            HashSet<char> ch = new HashSet<char>();
            foreach (char sub in substring)
                ch.Add(sub);

            foreach(char g in gs)
            {
                if (ch.Contains(g))
                {
                    defaultValue = "PartialPresent";
                    ch.Remove(g);
                }
            }
            if (ch.Count == 0)
                defaultValue = "BothPresent";

            return defaultValue;
        }


        

        //739. Daily Temperatures
        //https://leetcode.com/problems/daily-temperatures/
        //For example, given the list of temperatures T = [73, 74, 75, 71, 69, 72, 76, 73], your output should be [1, 1, 4, 2, 1, 1, 0, 0].
        public int[] DailyTemperatures(int[] T)
        {
            Stack<int> stack = new Stack<int>();
            int[] result = new int[T.Length];

            for (int i = 0; i < T.Length; i++)
            {
                while (stack.Count > 0 && T[stack.Peek()] < T[i])
                {
                    int index = stack.Pop();
                    result[index] = i - index;
                }
                stack.Push(i);
            }

            return result;
        }


        //https://leetcode.com/problems/reverse-substrings-between-each-pair-of-parentheses/
        //Input: s = "(u(love)i)" Output: "iloveu"
        //Input: s = "(abcd)"     Output: "dcba"
        //Input: s = "(ed(et(oc))el)" Output: "leetcode"
        //Input: s = "a(bcdefghijkl(mno)p)q"  Output: "apmnolkjihgfedcbq"
        public static string reverseInParentheses(string inputString)
        {
            if (inputString == null || inputString.Length <= 1) return inputString;

            var stack = new Stack<string>();
            string str = "";
            foreach (var c in inputString)
            {
                if (c <= 'z' && c >= 'a')
                {
                    str += c;
                }
                else if (c == '(')
                {
                    stack.Push(str);
                    str = "";
                }
                else
                {
                    var p = stack.Pop();
                    var arr = str.ToCharArray();
                    Array.Reverse(arr);
                    var newstr = new string(arr); // reverse current
                    str = p + newstr;
                }
            }

            return str;
        }


        //https://leetcode.com/problems/remove-all-adjacent-duplicates-in-string/
        //1047. Remove All Adjacent Duplicates In String
        //Input: "abbaca" Output: "ca"
        public String removeDuplicates(String str)
        {
            Stack<char> stack = new Stack<char>();
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i <= str.Length - 1; i++)
            {
                if (stack.Count == 0 || stack.Peek() != str[i])
                {
                    stack.Push(str[i]);
                }
                else
                    stack.Pop();
            }
            while (stack.Count > 0)
            {
                sb.Append(stack.Pop());
            }
            char[] s = sb.ToString().ToCharArray();
            Array.Reverse(s);
            return new string(s);
        }

        //https://leetcode.com/problems/decode-string/
        //394. Decode String
        //k[encoded_string], where the encoded_string inside the square brackets is being repeated exactly k times.
        //Example 1: Input: s = "3[a]2[bc]" Output: "aaabcbc"
        //Example 2: Input: s = "3[a2[c]]" Output: "accaccacc"
        //Example 3: Input: s = "2[abc]3[cd]ef" Output: "abcabccdcdcdef"
        //Example 4: Input: s = "abc3[cd]xyz" Output: "abccdcdcdxyz"
        //another solution link: https://leetcode.com/problems/decode-string/discuss/613799/C-faster-than-90.16-less-than-10.00-Mem-O(n)
        public static string decodeString(string s)
        {
            Stack<string> val = new Stack<string>();
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] != ']')
                {
                    val.Push(char.ToString(s[i]));
                }
                else
                {
                    string str = string.Empty;
                    while (val.Count > 0 && !val.Peek().Equals("["))
                    {
                        str = val.Pop() + str;
                    }
                    if (val.Peek().Equals("["))
                    {
                        val.Pop();
                    }
                    string num = string.Empty;
                    while (val.Count > 0 && char.IsDigit(val.Peek()[0]))
                    {
                        num = val.Pop() + num;
                    }
                    if (!string.IsNullOrEmpty(num))
                    {
                        int cnt = int.Parse(num);
                        StringBuilder sb = new StringBuilder();
                        while (cnt > 0)
                        {
                            sb.Append(str);
                            cnt--;
                        }
                        str = sb.ToString();
                    }
                    val.Push(str);
                }
            }
            string str1 = string.Empty;
            while (val.Count > 0)
            {
                str1 = val.Pop() + str1;
            }
            return str1;
        }

        //https://leetcode.com/problems/restore-ip-addresses/
        //93. Restore IP Addresses
        //Input: s = "25525511135" Output: ["255.255.11.135","255.255.111.35"]
        //https://leetcode.com/problems/restore-ip-addresses/discuss/414678/Simple-C-solution-using-backtracking
        public IList<string> RestoreIpAddresses(string s)
        {
            List<string> result = new List<string>();

            StringBuilder ip = new StringBuilder();

            Traverse(result, ip, 0, 0, s);
            return result;
        }

        private void Traverse(List<string> result, StringBuilder ip, int idx, int start, String s)
        {
            if (idx == 4)
            {
                if (start == s.Length)
                {
                    ip.Length--;
                    result.Add(ip.ToString());
                }
                return;
            }

            int len = 0;
            int num = 0;

            for (int i = start; i < Math.Min(s.Length, start + 3); i++)
            {
                if (i > start && num == 0) { return; }

                num *= 10;
                num += (s[i] - '0');

                if (num > 255) { return; }

                len = ip.Length;
                ip.Append(num).Append(".");
                Traverse(result, ip, idx + 1, i + 1, s);
                ip.Length = len;
            }
        }
    }
}
