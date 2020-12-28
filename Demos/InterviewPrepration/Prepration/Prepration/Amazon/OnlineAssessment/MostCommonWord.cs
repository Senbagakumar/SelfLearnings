using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepration.Amazon.OnlineAssessment
{
    class MostCommonWord
    {
		//10. Most Common Word
		//Input: Paragraph = "Bob hit a ball, the hit BALL flew far after it was hit." 
		//banned = ["hit"] Output: "ball"
		//Explanation: 
		//"hit" occurs 3 times, but it is a banned word. "ball" occurs twice(and no other word does), so it is the most frequent non-banned word in the paragraph.
		//Note that words in the paragraph are not case sensitive,
		//that punctuation is ignored (even if adjacent to words, such as "ball,"), 
		//and that "hit" isn't the answer even though it occurs more because it is banned.

		//https://leetcode.com/problems/most-common-word/
		public string Most_Common_Word(string paragraph, string[] banned)
		{
			//paragraph += ".";

			HashSet<string> banset = new HashSet<string>();
			foreach (string w in banned)
				banset.Add(w);

			var count = new Dictionary<string, int>();

			string ans = "";
			int ansfreq = 0;

			//string[] words = paragraph.Split(' ');
			//      foreach(var word in words)
			//{
			//	var finalword = word.ToLower();
			//	if (!banset.Contains(finalword))
			//	{
			//		if (count.ContainsKey(finalword))
			//			count[finalword]++;
			//		else
			//			count.Add(finalword, 1);

			//		if (count[finalword] > ansfreq)
			//		{
			//			ans = finalword;
			//			ansfreq = count[finalword];
			//		}
			//	}
			//}

			//StringBuilder word = new StringBuilder();
			string word = string.Empty;
			foreach (char c in paragraph)
			{
				if (Char.IsLetter(c))
				{
					word += c;
					//word.Append(c.ToString().ToLower());
				}
				else if (word.Length > 0)
				{
					word = word.ToLower();
					//String finalword = word.ToString();
					if (!banset.Contains(word))
					{
						if (count.ContainsKey(word))
							count[word]++;
						else
							count.Add(word, 1);

						if (count[word] > ansfreq)
						{
							ans = word;
							ansfreq = count[word];
						}
					}
					word = string.Empty;
					//word = new StringBuilder();
				}
			}
			return ans;
		}
	}
}
