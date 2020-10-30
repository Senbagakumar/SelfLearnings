using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Xml;
using static Prepration.Assessment;

namespace Prepration.Amazon
{
	class AmazonInterviewQuestions
	{
		//Microsoft Question
		//1. Merge K Sorted List -- Merge k sorted linked lists and return it as one sorted list. Analyze and describe its complexity
		//Qus: https://leetcode.com/problems/merge-k-sorted-lists/
		//Ans: https://leetcode.com/problems/merge-k-sorted-lists/discuss/564343/Java-Solution-1ms-faster-than-100
		public Node MergeKLists(Node[] lists)
		{
			if (lists == null || lists.Length == 0)
			{
				return null;
			}
			return MergeKLists(lists, 0, lists.Length - 1);
		}

		public Node MergeKLists(Node[] lists, int left, int right)
		{

			int middle = (right + left) / 2;
			if (left < right)
			{
				Node l1 = MergeKLists(lists, left, middle);
				Node l2 = MergeKLists(lists, middle + 1, right);
				Node result = MergeTwoLists(l1, l2);
				return result;
			}
			return lists[left];
		}

		public Node MergeTwoLists(Node l1, Node l2)
		{
			Node dummy = new Node(0);
			Node cur = dummy;
			while (l1 != null || l2 != null)
			{
				if (l1 == null)
				{
					cur.Next = l2;
					l2 = l2.Next;
				}
				if (l2 == null)
				{
					cur.Next = l1;
					l1 = l1.Next;
				}
				if (l1.Value <= l2.Value)
				{
					cur.Next = l1;
					l1 = l1.Next;
				}
				else
				{
					cur.Next = l2;
					l2 = l2.Next;
				}
				cur = cur.Next;
			}
			return dummy.Next;
		}

		//2. Critical Connections in a Network
		//https://leetcode.com/problems/critical-connections-in-a-network/
		//https://leetcode.com/problems/critical-connections-in-a-network/discuss/552431/Java-easy-to-read-solution
		//Added in ArrayPrograms

		//3. Meeting Rooms II
		//https://leetcode.com/problems/meeting-rooms-ii/
		//https://leetcode.com/problems/meeting-rooms-ii/discuss/533279/C-Sorted-List-and-Stack
		public int MinMeetingRooms(int[,] intervals)
		{
			var times = new List<MeetingTime>();

			for (int i = 0; i < intervals.GetLength(0); i++)
			{

				times.Add(new MeetingTime { stime = intervals[i, 0], isStart = true });
				times.Add(new MeetingTime { stime = intervals[i, 1], isStart = false });
			}

			times = times.OrderBy(x => x.stime).ThenBy(x => x.isStart).ToList();

			var minConf = 0;

			var conferenceStack = new Stack<int>();

			for (int i = 0; i < times.Count; i++)
			{

				if (times[i].isStart)
					conferenceStack.Push(times[i].stime);
				else
					conferenceStack.Pop();

				if (conferenceStack.Count > minConf)
					minConf = conferenceStack.Count;
			}

			return minConf;
		}

		//3. Container with Most Water
		//Question: https://leetcode.com/problems/container-with-most-water/
		public int MaxArea(int[] height) //1, 8, 6, 2, 5, 4, 8, 3, 7 }-- Ans : 49
		{
			int maxarea = 0, l = 0, r = height.Length - 1;
			while (l < r)
			{
				maxarea = Math.Max(maxarea, Math.Min(height[l], height[r]) * (r - l));
				if (height[l] < height[r])
					l++;
				else
					r--;
			}
			return maxarea;
		}

		//4. Roman to Integer
		public int RomanToInt(string s)
		{
			Dictionary<string, int> numeralValues = new Dictionary<string, int>
			{
				{"I", 1},
				{"V", 5},
				{"X", 10},
				{"L", 50},
				{"C", 100},
				{"D", 500},
				{"M", 1000}
			 };

			var numeralArray = s.ToCharArray();
			var total = 0;
			for (var index = 0; index < numeralArray.Length; index++)
			{
				if (index == numeralArray.Length - 1)
				{
					total += numeralArray[index];
				}
				else if (numeralArray[index] >= numeralArray[index + 1])
				{
					total += numeralArray[index];
				}
				else
				{
					total -= numeralArray[index];
				}
			}
			return total;
		}

		//5. 3Sum //[-1, 0, 1, 2, -1, -4]
		//https://leetcode.com/problems/3sum/
		public List<List<int>> ThreeSum(int[] arr, int target)
		{
			//List<List<int>> ans = new List<List<int>>();
			//Array.Sort(arr);
			//for (int i = 0; i < arr.Length; i++)
			//{
			//	int low = i + 1; int high = arr.Length - 1;
			//	int currentTarget = target - arr[i];
			//	while (low < high)
			//	{
			//		int sum = arr[low] + arr[high] + arr[i];
			//		if(sum == target)
			//			ans.Add(new List<int>() { arr[low], arr[high], arr[i] });

			//		if (arr[low] + arr[high] > currentTarget) high--;
			//		else low++;
			//	}
			//}
			//return ans;

			Array.Sort(arr);
			int n = arr.Length;
			List<List<int>> ans = new List<List<int>>();
			for (int i = 0; i < n; i++)
			{
				if (arr[i] > target) break; // Since a[i] <= arr[j] <= arr[k], if a[i] > 0 then sum=arr[i]+arr[j]+arr[k] > 0
				int j = i + 1, k = n - 1;
				while (j < k)
				{
					int sum = arr[i] + arr[j] + arr[k];
					if (sum < target) j++;
					else if (sum > target) k--;
					else
					{
						ans.Add(new List<int>() { arr[i], arr[j], arr[k] });
						// Skip duplicate numbers of j
						while (j + 1 <= k && arr[j] == arr[j + 1]) j++;
						j++;
						k--;
					}
				}
				// Skip duplicate numbers of i
				while (i + 1 < n && arr[i + 1] == arr[i]) i++;
			}
			return ans;
		}

		//6. 3Sum Closest
		public int ThreeSumClosest(int[] nums, int target)
		{
			Array.Sort(nums);
			int result = 0;
			int minDiff = int.MaxValue;
			for (int i = 0; i < nums.Length; i++)
			{
				int j = i + 1; int k = nums.Length - 1;
				//int currentTarget = target - nums[i];
				while (j < k)
				{
					int sum = nums[j] + nums[k] + nums[i];
					int diff = Math.Abs(target - sum);
					if (diff < minDiff)
					{
						minDiff = diff;
						result = sum;
					}
					if (nums[j] + nums[k] + nums[i] > target) k--;
					else j++;
				}
			}
			return result;
		}

		//6. Implement strStr()
		public int SubStringPattern(string input, string substring)
		{
			//HiHowHowareyou
			//Howa
			int index = -1;
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
					index = i - substring.Length;
					break;
				}
			}
			return index;
		}

		//7. Compare Version Number //If version1 > version2 return 1; if version1 < version2 return -1;otherwise return 0
		//Input: version1 = "0.1", version2 = "1.1" output: -1
		public int CompareVersion(String version1, string version2)
		{
			string[] nums1 = version1.Split('.');
			string[] nums2 = version2.Split('.');
			int n1 = nums1.Length, n2 = nums2.Length;

			// compare versions
			int i1, i2;
			for (int i = 0; i < Math.Max(n1, n2); ++i)
			{
				i1 = i < n1 ? int.Parse(nums1[i]) : 0;
				i2 = i < n2 ? int.Parse(nums2[i]) : 0;
				if (i1 != i2)
				{
					return i1 > i2 ? 1 : -1;
				}
			}
			// the versions are equal
			return 0;
		}

		//8. Product of Array Except Self
		//return an array output such that output[i] is equal to the product of all the elements of nums except nums[i].
		//Input:  [1,2,3,4] Output: [24,12,8,6]
		public int[] ProductExceptSelf(int[] nums)
		{

			// The length of the input array
			int length = nums.Length;

			// The left and right arrays as described in the algorithm
			int[] L = new int[length];
			int[] R = new int[length];

			// Final answer array to be returned
			int[] answer = new int[length];

			// L[i] contains the product of all the elements to the left
			// Note: for the element at index '0', there are no elements to the left,
			// so L[0] would be 1
			L[0] = 1;
			for (int i = 1; i < length; i++)
			{

				// L[i - 1] already contains the product of elements to the left of 'i - 1'
				// Simply multiplying it with nums[i - 1] would give the product of all
				// elements to the left of index 'i'
				L[i] = nums[i - 1] * L[i - 1];
			}

			// R[i] contains the product of all the elements to the right
			// Note: for the element at index 'length - 1', there are no elements to the right,
			// so the R[length - 1] would be 1
			R[length - 1] = 1;
			for (int i = length - 2; i >= 0; i--)
			{

				// R[i + 1] already contains the product of elements to the right of 'i + 1'
				// Simply multiplying it with nums[i + 1] would give the product of all
				// elements to the right of index 'i'
				R[i] = nums[i + 1] * R[i + 1];
			}

			// Constructing the answer array
			for (int i = 0; i < length; i++)
			{
				// For the first element, R[i] would be product except self
				// For the last element of the array, product except self would be L[i]
				// Else, multiple product of all elements to the left and to the right
				answer[i] = L[i] * R[i];
			}

			return answer;
		}

		//9. First Unique Character in a String
		//Given a string, find the first non-repeating character in it and return it's index. If it doesn't exist, return -1
		//s = "leetcode" return 0
		public int FirstUniqueChar(string s)
		{
			Dictionary<char, int> dict = new Dictionary<char, int>();
			int i = 0;
			while (i < s.Length)
			{
				if (dict.ContainsKey(s[i]))
				{
					dict[s[i]]++;
				}
				else
				{
					dict.Add(s[i], 1);
				}
				i++;
			}
			for (int j = 0; j < dict.Count; j++)
			{
				if (dict[s[j]] == 1)
					return j;
			}
			return -1;

		}

		//10. MostCommonWord
		//Input: Paragraph = "Bob hit a ball, the hit BALL flew far after it was hit." 
		//banned = ["hit"] Output: "ball"
		//Explanation: 
		//"hit" occurs 3 times, but it is a banned word. "ball" occurs twice(and no other word does), so it is the most frequent non-banned word in the paragraph.
		//Note that words in the paragraph are not case sensitive,
		//that punctuation is ignored (even if adjacent to words, such as "ball,"), 
		//and that "hit" isn't the answer even though it occurs more because it is banned.
		public string MostCommonWord(string paragraph, string[] banned)
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

		//Microsoft
		//https://leetcode.com/problems/reverse-nodes-in-k-group/
		//11. Reverse K nodes in a Group
		//Given this linked list: 1->2->3->4->5
		//For k = 2, you should return: 2->1->4->3->5
		//For k = 3, you should return: 3->2->1->4->5
		private Node reverseLinkedList(Node head, int k)
		{

			// Reverse k nodes of the given linked list.
			// This function assumes that the list contains 
			// atleast k nodes.
			Node new_head = null;
			Node ptr = head;

			while (k > 0)
			{

				// Keep track of the next node to process in the
				// original list
				Node next_node = ptr.Next;

				// Insert the node pointed to by "ptr"
				// at the beginning of the reversed list
				ptr.Next = new_head;
				new_head = ptr;

				// Move on to the next node
				ptr = next_node;

				// Decrement the count of nodes to be reversed by 1
				k--;
			}


			// Return the head of the reversed list
			return new_head;

		}

		public Node reverseKGroup(Node head, int k)
		{

			Node ptr = head;
			Node ktail = null;

			// Head of the final, moified linked list
			Node new_head = null;

			// Keep going until there are nodes in the list
			while (ptr != null)
			{

				int count = 0;

				// Start counting nodes from the head
				ptr = head;

				// Find the head of the next k nodes
				while (count < k && ptr != null)
				{
					ptr = ptr.Next;
					count += 1;
				}

				// If we counted k nodes, reverse them
				if (count == k)
				{

					// Reverse k nodes and get the new head
					Node revHead = this.reverseLinkedList(head, k);

					// new_head is the head of the final linked list
					if (new_head == null)
						new_head = revHead;

					// ktail is the tail of the previous block of 
					// reversed k nodes
					if (ktail != null)
						ktail.Next = revHead;

					ktail = head;
					head = ptr;
				}
			}

			// attach the final, possibly un-reversed portion
			if (ktail != null)
				ktail.Next = head;

			return new_head == null ? head : new_head;
		}

		public Node reverseKGrouprecur(Node head, int k)
		{

			int count = 0;
			Node ptr = head;

			// First, see if there are atleast k nodes
			// left in the linked list.
			while (count < k && ptr != null)
			{
				ptr = ptr.Next;
				count++;
			}


			// If we have k nodes, then we reverse them
			if (count == k)
			{

				// Reverse the first k nodes of the list and
				// get the reversed list's head.
				Node reversedHead = this.reverseLinkedList(head, k);

				// Now recurse on the remaining linked list. Since
				// our recursion returns the head of the overall processed
				// list, we use that and the "original" head of the "k" nodes
				// to re-wire the connections.
				head.Next = this.reverseKGrouprecur(ptr, k);
				return reversedHead;
			}

			return head;
		}

		//12. Course Schedule
		//Input: numCourses = 2, prerequisites = [[1,0]]
		//Output: true
		//Explanation: There are a total of 2 courses to take.
		//To take course 1 you should have finished course 0. So it is possible.
		public bool CanFinish(int numCourses, int[,] prerequisites)
		{

			// course -> list of next courses [1,0]
			Dictionary<int, List<int>> courseDict = new Dictionary<int, List<int>>();

			// build the graph first
			for (int i=0; i<prerequisites.GetLength(0); i++)
			{
				// relation[0] depends on relation[1]
				if (courseDict.ContainsKey(prerequisites[i,1]))
				{
					courseDict[prerequisites[i,1]].Add(prerequisites[i,0]);
				}
				else
				{
					List<int> nextCourses = new List<int>();
					nextCourses.Add(prerequisites[i, 0]);
					courseDict.Add(prerequisites[i, 1], nextCourses);
				}
			}

			bool[] path = new bool[numCourses];

			for (int currCourse = 0; currCourse < numCourses; ++currCourse)
			{
				if (this.isCyclic(currCourse, courseDict, path))
				{
					return false;
				}
			}

			return true;
		}


		/*
		 * backtracking method to check that no cycle would be formed starting from currCourse
		 */
		protected bool isCyclic(
			int currCourse,
			Dictionary<int, List<int>> courseDict,
			bool[] path)
		{

			if (path[currCourse])
			{
				// come across a previously visited node, i.e. detect the cycle
				return true;
			}

			// no following courses, no loop.
			if (!courseDict.ContainsKey(currCourse))
				return false;

			// before backtracking, mark the node in the path
			path[currCourse] = true;

			// backtracking
			bool ret = false;
			foreach(int nextCourse in courseDict[currCourse])
			{
				ret = this.isCyclic(nextCourse, courseDict, path);
				if (ret)
					break;
			}
			// after backtracking, remove the node from the path
			path[currCourse] = false;
			return ret;
		}


		//13. Cut of Trees for Golf Event		

		private void TreeHelper(Coordinate cellFrom, Coordinate cellTo, int[,] forest, out int dist)
		{
			int[] xdirs = new int[] { 1, 0, -1, 0 };
			int[] ydirs = new int[] { 0, 1, 0, -1 };

			int n = forest.GetLength(0);
			int m = forest.GetLength(1);
			bool[,] visited = new bool[n, m];
			Queue<Coordinate> bfs = new Queue<Coordinate>();
			bfs.Enqueue(cellFrom);
			visited[cellFrom.x, cellFrom.y] = true;

			dist = 0;

			while (bfs.Count > 0)
			{
				int count = bfs.Count;
				for (int i = 0; i < count; i++)
				{
					var node = bfs.Dequeue();

					if (node.x == cellTo.x && node.y == cellTo.y)
					{
						return;
					}

					for(int dir=0; dir<xdirs.Length; dir++)
					{
						int newX = node.x + xdirs[dir];
						int newY = node.y + ydirs[dir];

						if (newX >= 0 && newX < n && newY >= 0 && newY < m && forest[newX, newY] != 0 && !visited[newX, newY])
						{
							visited[newX, newY] = true;
							bfs.Enqueue(new Coordinate(newX, newY));
						}
					}
				}
				dist++;
			}
			dist =-1;
		}

		public int CutOffTree(int[,] forest)
		{
			int n = forest.GetLength(0);
			if (n == 0)
			{
				return 0;
			}

			int m = forest.GetLength(1);


			List<Tuple<int,int,int>> trees = new List<Tuple<int, int, int>>();
			for (int i = 0; i < n; i++)
			{
				for (int j = 0; j < m; j++)
				{
					if (forest[i,j] != 0)
					{
						trees.Add(new Tuple<int, int, int>(i, j, forest[i,j]));
					}
				}
			}

			trees.Sort((d1, d2) => d1.Item3.CompareTo(d2.Item3));
			int res = 0;
			var currCell = new Coordinate(0,0);

			for (int i = 0; i < trees.Count; i++)
			{
				TreeHelper(currCell, new Coordinate(trees[i].Item1, trees[i].Item2), forest, out int dst);
				if (dst == -1)
				{
					return -1;
				}

				res += dst;
				currCell = new Coordinate(trees[i].Item1, trees[i].Item2);
			}

			return res;
		}

		//14. Flood Fill
		public int[,] FloodFill(int[,] image, int sr, int sc, int newColor)
		{
			int color = image[sr,sc];
			if (color != newColor) dfs(image, sr, sc, color, newColor);
			return image;
		}
		private void dfs(int[,] image, int r, int c, int color, int newColor)
		{
			if (image[r,c] == color)
			{
				image[r,c] = newColor;
				if (r >= 1) dfs(image, r - 1, c, color, newColor);
				if (c >= 1) dfs(image, r, c - 1, color, newColor);
				if (r + 1 < image.GetLength(0)) dfs(image, r + 1, c, color, newColor);
				if (c + 1 < image.GetLength(1)) dfs(image, r, c + 1, color, newColor);
			}
		}

		//https://leetcode.com/problems/merge-intervals/
		//15. Merge Intervals  //Inp:[[1,3],[2,6],[8,10],[15,18]] //Out: [[1,6],[8,10],[15,18]]
		public List<MeetingTime> MergeInterval(int[,] interval)
		{
			var lmtime = new List<MeetingTime>();
			var result = new List<MeetingTime>();
			for (int i=0; i<interval.GetLength(0); i++)
			{
				lmtime.Add(new MeetingTime() { stime = interval[i, 0], etime = interval[i, 1] });
			}
			lmtime = lmtime.OrderBy(q => q.stime).ToList();

			int k = 0;
			MeetingTime f, s;
			while(k<lmtime.Count)
			{
				f = lmtime[k];
				if (k + 1 < lmtime.Count)
					s = lmtime[k + 1];
				else
					s = lmtime[k];

				if (f.etime >= s.stime)
				{
					result.Add(new MeetingTime() { stime = f.stime, etime = s.etime });
					k = k + 2;
				}
				else
				{
					result.Add(new MeetingTime() { stime = f.stime, etime = f.etime });
					k = k + 1;
				}
			}
			return result;
		}

		//16. TopFrequent
		//Input: nums = [1,1,1,2,2,3], k = 2 	Output: [1,2]
		public IList<int> TopKFrequent(int[] nums, int k)
		{
			Dictionary<int, int> top = new Dictionary<int, int>();
			IList<int> res = new List<int>();
			for (int i = 0; i < nums.Length; i++)
			{
				if (top.ContainsKey(nums[i]))
					top[nums[i]]++;
				else
					top.Add(nums[i], 1);
			}
			for (int j = 0; j < top.Count; j++)
			{
				if (top[nums[j]] >= k)
					res.Add(nums[j]);
			}
			return res;
		}

		//Microsoft Question
		//https://leetcode.com/problems/maximum-subarray/
		//17. MaxSubArray or Maximum Subarray
		//Given an integer array nums, find the contiguous subarray (containing at least one number) which has the largest sum and return its sum
		//Input: [-2,1,-3,4,-1,2,1,-5,4], Output: 6
		//Explanation: [4,-1,2,1] has the largest sum = 6.
		public int MaxSubArray(int[] nums)
		{
			int n = nums.Length;
			int currSum = nums[0];
			int maxSum = nums[0];

			for (int i = 1; i < n; ++i)
			{
				currSum = Math.Max(nums[i], currSum + nums[i]);
				maxSum = Math.Max(maxSum, currSum);
			}
			return maxSum;
		}

		//https://leetcode.com/problems/maximum-product-subarray/
		//Maximum Product Subarray
		//Given an integer array nums, find the contiguous subarray within an array (containing at least one number) which has the largest product.
		//Input: [2,3,-2,4] Output: 6 Explanation: [2,3] has the largest product 6.
		//Input: [-2,0,-1] Output: 0 Explanation: The result cannot be 2, because[-2, -1] is not a subarray.

		public int MaxProductArray(int[] nums)
		{
			int n = nums.Length;
			int currSum = nums[0];
			int maxSum = nums[0];

			for (int i = 1; i < n; ++i)
			{
				currSum = Math.Max(nums[i], currSum * nums[i]);
				maxSum = Math.Max(maxSum, currSum);
			}
			return maxSum;
		}


		//https://leetcode.com/problems/word-break/
		//18. WordBreak //s = "leetcode", wordDict = ["leet", "code"]
		public bool WordBreak(string s, List<string> wordDict)
		{
			int N = s.Length;
			bool[] dp = new bool[N + 1];
			dp[0] = true;

			for (int i = 1; i <= N; i++)
			{
				for (int j = 0; j < i; j++)
				{
					var word = s.Substring(j, i - j);
					if (dp[j] & wordDict.Contains(word))
					{
						dp[i] = true;
						break;
					}
				}
			}
			return dp[N];
		}

		//19. Coin Change
		//Input: coins = [1, 2, 5], amount = 11  Output: 3 Explanation: 11 = 5 + 5 + 1
		public int CoinChange(int[] coins, int amount)
		{
			int max = amount + 1;
			int[] dp = new int[amount + 1];
			dp=dp.Select(v => v = max).ToArray<int>();
			dp[0] = 0;
			for (int i = 1; i <= amount; i++)
			{
				for (int j = 0; j < coins.Length; j++)
				{
					if (coins[j] <= i)
					{
						dp[i] = Math.Min(dp[i], dp[i - coins[j]] + 1);
					}
				}
			}
			return dp[amount] > amount ? -1 : dp[amount];
		}

		//20. Second Highest Salary
		//SELECT DISTINCT
		//		Salary AS SecondHighestSalary
		//	FROM

		//	Employee
		//ORDER BY Salary DESC
		//LIMIT 1 OFFSET 1

		

		//https://leetcode.com/discuss/general-discussion/640977/stone-game-iii




	}

	public class MeetingTime
	{
		public int stime { get; set; }
		public bool isStart { get; set; }
		public int etime { get; set; }
	}
}
