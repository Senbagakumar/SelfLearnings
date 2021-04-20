using Prepration.Microsoft;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace Prepration
{
    public class DesignQuestions
    {

        //https://leetcode.com/discuss/interview-question/1140451/helpful-list-of-leetcode-posts-on-system-design-at-facebook-google-amazon-uber-microsoft


        public static void ExecuteTicTacToe()
        {
            int val = 0;
            var tictactoe = new TicTacToe(3);
            val = tictactoe.Move(0, 0, 1); //1 - first player
            val = tictactoe.Move(0, 2, 2); //2 - second player
            val = tictactoe.Move(2, 2, 1); //1-  first player
            val = tictactoe.Move(1, 1, 2); //2-  second player
            val = tictactoe.Move(2, 0, 1); //1-  first player
            val = tictactoe.Move(1, 0, 2); //2 - second player
            val = tictactoe.Move(2, 1, 1); //1 - first player
            if (val == 1)
                Console.WriteLine("First Player Win");
            else
                if (val != 0)
                Console.WriteLine("Second Player Win");
            else
                Console.WriteLine("draw");
        }

        //Amazon Question
        //Microsoft Question
        class TicTacToe
        {

            int[,] tic;
            int n;
            public TicTacToe(int n)
            {
                this.n = n;
                tic = new int[n, n];

            }

            public int Move(int row, int col, int teamPlayer)
            {
                int val = teamPlayer == 1 ? 1 : -1;
                tic[row, col] += val;

                if (WinConditionForRow(val, row) || WinConditionForColumn(val, col) ||
                   WinConditionForFirstDiagonal(val) || WinConditionForSecondDiagonal(val))
                {
                    return teamPlayer;
                }
                else
                    return 0;
            }

            //Row Check
            private bool WinConditionForRow(int player, int row)
            {
                bool win = true;
                for (int i = 0; i < n; i++)
                {
                    if (tic[row, i] != player)
                    {
                        win = false;
                        break;
                    }
                }
                return win;
            }
            //Column check
            private bool WinConditionForColumn(int player, int column)
            {
                bool win = true;
                for (int i = 0; i < n; i++)
                {
                    if (tic[i, column] != player)
                    {
                        win = false;
                        break;
                    }
                }
                return win;
            }

            private bool WinConditionForFirstDiagonal(int player)
            {
                bool win = true;
                for (int i = 0; i < n; i++)
                {
                    if (tic[i, i] != player)
                    {
                        win = false; break;
                    }
                }
                return win;
            }

            private bool WinConditionForSecondDiagonal(int player)
            {
                bool win = true;
                for (int i = 0; i < n; i++)
                {
                    if (tic[i, n - i - 1] != player)
                    {
                        win = false; break;
                    }
                }
                return win;
            }

        }

        //Micrsoft Question
        class Celebrity
        {
            int[,] cele;

            public Celebrity(int n)
            {
                cele = new int[4, 4] { { 0,0,1,0 },
                                       { 0,0,1,0 },
                                       { 0,0,0,0 },
                                       { 0,0,1,0} };
            }

            public bool Knows(int a, int b)
            {
                return cele[a, b] == 1 ? true : false;
            }

            public int FindCelebrity(int n)
            {
                var st = new Stack<int>();

                for (int i = 0; i < n; i++)
                {
                    st.Push(i);
                }
                while (st.Count > 1)
                {
                    int first = st.Pop();
                    int second = st.Pop();

                    if (Knows(first, second))
                        st.Push(second);
                    else
                        st.Push(first);
                }
                int result = st.Pop();

                for (int i = 0; i < n; i++)
                {
                    if (Knows(result, i) || !Knows(i, result))
                        return -1;
                }
                return result;
            }

        }

        private class ListNode
        {
            public int Key { get; set; }
            public int Value { get; set; }
            public ListNode() { }

            public ListNode(int key, int value)
            {
                Key = key;
                Value = value;
            }
            public ListNode next;
            public ListNode previous;

        }

        //Amazon Question
        //Microsoft Question
        public class LRUCache
        {
            private int Capacity;
            private int TotalItems;
            private Dictionary<int, ListNode> dict = new Dictionary<int, ListNode>();
            ListNode head; ListNode tail;

            public LRUCache(int capacity)
            {
                Capacity = capacity;
                TotalItems = 0;

                head = new ListNode();
                tail = new ListNode();

                head.next = tail;
                tail.previous = head;
            }


            public int Get(int key)
            {
                var node = dict.Keys.Contains(key) ? dict[key] : null;
                if (node != null)
                {
                    MovetoHead(node);
                    return node.Value;
                }
                return -1;
            }

            private void MovetoHead(ListNode node)
            {
                RemoveElement(node);
                AddElementsInHead(node);
            }

            private void AddElementsInHead(ListNode node)
            {
                node.previous = head;
                node.next = head.next;

                head.next.previous = node;
                head.next = node;

            }

            private void RemoveElement(ListNode lnode)
            {
                ListNode saveNext = lnode.next;
                ListNode savePrevious = lnode.previous;

                savePrevious.next = saveNext;
                saveNext.previous = savePrevious;
            }

            public void Put(int key, int value)
            {
                ListNode current = new ListNode(key, value);

                var isExist = dict.Keys.Contains(key) ? dict[key] : null;
                if (isExist != null)
                {
                    isExist.Value = value;
                    MovetoHead(isExist);
                }
                else
                {
                    dict.Add(key, current);
                    AddElementsInHead(current);
                    TotalItems++;
                    if (TotalItems > Capacity)
                    {
                        RemoveLeast();
                    }

                }

            }

            private void RemoveLeast()
            {
                var lastNode = tail.previous;
                RemoveElement(lastNode);
                dict.Remove(lastNode.Key);
                TotalItems--;
            }

            public static void Test()
            {
                int ret = 0;
                var vi = new LRUCache(2);
                vi.Put(1, 1);
                vi.Put(2, 2);
                ret = vi.Get(1);  // return 1;
                vi.Put(3, 3); // evicts 2;
                ret = vi.Get(2);  // return -1;
                vi.Put(4, 4); //evicts 1;
                ret = vi.Get(1); // return -1;
                ret = vi.Get(3);
                ret = vi.Get(4);
            }

        }

        class Pair<U, V>
        {
            public U first;
            public V second;

            public Pair(U first, V second)
            {
                this.first = first;
                this.second = second;
            }
        }


        class Bucket
        {
            private List<Pair<int, int>> bucket;

            public Bucket()
            {
                this.bucket = new List<Pair<int, int>>();
            }

            public int get(int key)
            {
                foreach (Pair<int, int> pair in this.bucket)
                {
                    if (pair.first.Equals(key))
                        return pair.second;
                }
                return -1;
            }

            public void update(int key, int value)
            {
                bool found = false;
                foreach (Pair<int, int> pair in this.bucket)
                {
                    if (pair.first.Equals(key))
                    {
                        pair.second = value;
                        found = true;
                    }
                }
                if (!found)
                    this.bucket.Add(new Pair<int, int>(key, value));
            }

            public void remove(int key)
            {
                foreach (Pair<int, int> pair in this.bucket)
                {
                    if (pair.first.Equals(key))
                    {
                        this.bucket.Remove(pair);
                        break;
                    }
                }
            }
        }

        public class MyHashMap
        {
            private int key_space;
            List<Bucket> hash_table;


            /** Initialize your data structure here. */
            public MyHashMap()
            {
                this.key_space = 2069;
                hash_table = new List<Bucket>();
                for (int i = 0; i < this.key_space; i++)
                    hash_table.Add(new Bucket());
            }

            /** value will always be non-negative. */
            public void put(int key, int value)
            {
                int hash_key = key % this.key_space;
                this.hash_table[hash_key].update(key, value);
            }

            /**
             * Returns the value to which the specified key is mapped, or -1 if this map contains no mapping
             * for the key
             */
            public int get(int key)
            {
                int hash_key = key % this.key_space;
                return this.hash_table[hash_key].get(key);
            }

            /** Removes the mapping of the specified value key if this map contains a mapping for the key */
            public void remove(int key)
            {
                int hash_key = key % this.key_space;
                this.hash_table[hash_key].remove(key);
            }
        }

        //Microsoft Question
        //https://leetcode.com/problems/implement-trie-prefix-tree/
        //https://leetcode.com/problems/implement-trie-prefix-tree/discuss/523313/C
        public class Trie
        {
            private Trie[] Children = new Trie[26];
            private bool EndOfWord = false;

            /** Inserts a word into the trie. */
            public void Insert(string word, int i = 0)
            {
                int code = word[i] - 'a';
                if (Children[code] == null)
                {
                    Children[code] = new Trie();
                }
                if (word.Length - 1 == i)
                    Children[code].EndOfWord = true;
                else
                    Children[code].Insert(word, i + 1);
            }

            /** Returns if the word is in the trie. */
            public bool Search(string word, int i = 0)
            {
                var code = word[i] - 'a';
                if (Children[code] == null) return false;

                if (word.Length - 1 == i) return Children[code].EndOfWord;
                else return Children[code].Search(word, i + 1);
            }

            /** Returns if there is any word in the trie that starts with the given prefix. */
            public bool StartsWith(string prefix, int i = 0)
            {
                var code = prefix[i] - 'a';
                if (Children[code] == null) return false;

                if (prefix.Length - 1 == i) return true;
                else return Children[code].StartsWith(prefix, i + 1);
            }
        }

        //Implement Queue using Stacks
        //https://leetcode.com/problems/implement-queue-using-stacks/
        //https://leetcode.com/problems/implement-queue-using-stacks/discuss/516686/Concise-Java-Using-2-Stacks
        public class MyQueue
        {
            Stack<int> r;
            Stack<int> s;
            public MyQueue()
            {
                r = new Stack<int>();
                s = new Stack<int>();
            }

            public void EnQueue(int inp)
            {
                r.Push(inp);
            }
            public int DeQueue()
            {
                ReverseElements();
                return s.Pop();
            }

            public int Peek()
            {
                ReverseElements();
                return s.Peek();
            }

            public bool IsEmpty()
            {
                return r.Count == 0 && s.Count == 0;
            }

            private void ReverseElements()
            {
                if (s.Count == 0)
                {
                    while (r.Count > 0)
                    {
                        s.Push(r.Pop());
                    }
                }
            }
        }

        // Implement Stack using Queues
        //https://leetcode.com/problems/implement-stack-using-queues/
        //https://leetcode.com/problems/implement-stack-using-queues/discuss/509809/C-solution-using-a-queue-and-indexcount

        public class MyStack
        {
            Queue<int> queue;
            int index = 0;
            public MyStack()
            {
                queue = new Queue<int>();
            }

            public void Push(int value)
            {
                queue.Enqueue(value);
                index++;
            }

            public int Pop()
            {
                int count = 0;
                while (count < index - 1)
                {
                    queue.Enqueue(queue.Dequeue());
                    count++;
                }
                index--;
                return queue.Dequeue();
            }
            public int Top()
            {
                int count = 0;
                while (count < index - 1)
                {
                    queue.Enqueue(queue.Dequeue());
                    count++;
                }
                int val = queue.Dequeue();
                queue.Enqueue(val);
                return val;
            }
            public bool Empty()
            {
                return queue.Count == 0 ? true : false;
            }
        }

        //Amazon Question
        public class MinStack
        {

            private Stack<int[]> stack = new Stack<int[]>();
            public MinStack() { }

            public void Push(int x)
            {
                /* If the stack is empty, then the min value
                 * must just be the first value we add. */
                if (stack.Count == 0)
                {
                    stack.Push(new int[] { x, x });
                    return;
                }

                int currentMin = stack.Peek()[1];
                stack.Push(new int[] { x, Math.Min(x, currentMin) });
            }


            public void Pop()
            {
                stack.Pop();
            }


            public int Top()
            {
                return stack.Peek()[0];
            }


            public int getMin()
            {
                return stack.Peek()[1];
            }
        }

        //Amazon Question
        public class FreqStack
        {
            Dictionary<int, Node> map;
            List<Stack<Node>> list;
            public FreqStack()
            {
                this.map = new Dictionary<int, Node>();
                this.list = new List<Stack<Node>>();
                list.Add(new Stack<Node>());
            }

            public void push(int x)
            {
                if (map.ContainsKey(x))
                {
                    Node node = map[x];
                    node.fre++;
                    if (list.Count < node.fre)
                    {
                        list.Add(new Stack<Node>());
                    }
                    list[node.fre - 1].Push(node);

                }
                else
                {
                    Node node = new Node(x);
                    map.Add(x, node);
                    list[0].Push(node);
                }
            }

            public int pop()
            {
                int index = list.Count - 1;
                Node node = list[index].Pop();
                int res = node.val;
                node.fre--;
                if (list[index].Count == 0)
                    list.RemoveAt(index);
                return res;
            }

            class Node
            {
                public int val;
                public int fre;
                public Node(int val)
                {
                    this.val = val;
                    this.fre = 1;
                }
            }
        }

        //https://leetcode.com/problems/find-median-from-data-stream/
        //Find the median from datastream
        //[2,3,4], the median is 3 ; [2,3], the median is (2 + 3) / 2 = 2.5
        //addNum(1)
        //addNum(2)
        //findMedian() -> 1.5
        //addNum(3)
        //findMedian() -> 2

        public class MedianFinder
        {

            /** initialize your data structure here. */
            //use a sorted List;
            //Find the Median will be easy;
            //if odd count; take the middle number;
            //if even, take the average of the two middle number;
            //the question is the sorted list;
            //during AddNum we need to use binary search to find the location for insertion.
            private List<int> data = new List<int>();


            public void AddNum(int num)
            {
                if (data.Count == 0)
                    data.Add(num);
                else
                {
                    int left = 0;
                    int right = data.Count - 1;

                    while (left <= right)
                    {
                        int mid = (left + right) / 2;
                        if (data[mid] >= num)
                            right = mid - 1;
                        else
                            left = mid + 1;
                    }
                    data.Insert(left, num);
                }
            }

            public double FindMedian()
            {
                if (data.Count % 2 == 1)
                    return (double)data[data.Count / 2];
                else if (data.Count >= 2)
                {
                    double x = (double)data[data.Count / 2];
                    double y = (double)data[(data.Count / 2) - 1];
                    double result = (x + y) / 2;
                    return result;
                }
                else
                    return 0.0;
            }
        }

        //https://leetcode.com/problems/clone-graph/
        //Clone Graph
        //Given a reference of a node in a connected undirected graph.
        //Return a deep copy(clone) of the graph.

        public class CNode
        {
            public int val;
            public IList<CNode> neighbors;

            public CNode()
            {
                val = 0;
                neighbors = new List<CNode>();
            }

            public CNode(int _val)
            {
                val = _val;
                neighbors = new List<CNode>();
            }

            public CNode(int _val, List<CNode> _neighbors)
            {
                val = _val;
                neighbors = _neighbors;
            }
        }

        public CNode CloneGraph(CNode node)
        {
            var visited = new Dictionary<CNode, CNode>();
            if (node == null) return null;
            CNode result = DFSCloneGraph(node, visited);
            return result;
        }

        private CNode DFSCloneGraph(CNode node, Dictionary<CNode, CNode> visited)
        {
            if (visited.ContainsKey(node))
                return visited[node];
            var newNode = new CNode(node.val, new List<CNode>());
            visited.Add(node, newNode);
            foreach (CNode cnode in node.neighbors)
                newNode.neighbors.Add(DFSCloneGraph(cnode, visited));
            return newNode;
        }

        //https://leetcode.com/problems/time-based-key-value-store/
        //Create a timebased key-value store class TimeMap, that supports two operations.
        //1. set(string key, string value, int timestamp)

        //Stores the key and value, along with the given timestamp.
        //2. get(string key, int timestamp)

        //Returns a value such that set(key, value, timestamp_prev) was called previously, with timestamp_prev <= timestamp.
        //If there are multiple such values, it returns the one with the largest timestamp_prev.
        //If there are no values, it returns the empty string ("").

        //Exa mple:
        //Input: inputs = ["TimeMap","set","set","get","get","get","get","get"], inputs = [[],["love","high",10],["love","low",20],["love",5],["love",10],["love",15],["love",20],["love",25]]
        //Output: [null,null,null,"","high","high","low","low"]

        public class TimeMap
        {
            Dictionary<string, List<(int timestamp, string value)>> _data;

            public TimeMap()
            {
                _data = new Dictionary<string, List<(int timestamp, string value)>>();
            }

            public void Set(string key, string value, int timestamp)
            {
                if (!_data.ContainsKey(key))
                {
                    _data[key] = new List<(int timestamp, string value)>();
                }

                _data[key].Add((timestamp, value));
            }

            public string Get(string key, int timestamp)
            {
                if (!_data.ContainsKey(key))
                {
                    return string.Empty;
                }

                var list = _data[key];

                int left = 0;
                int right = list.Count - 1;

                while (left < right)
                {
                    if (right - left == 1)
                    {
                        break;
                    }

                    int mid = left + (right - left) / 2;

                    var midItem = list[mid];
                    if (midItem.timestamp == timestamp)
                    {
                        return midItem.value;
                    }

                    if (midItem.timestamp < timestamp)
                    {
                        left = mid;
                        continue;
                    }

                    right = mid;
                }

                if (list[right].timestamp <= timestamp)
                {
                    return list[right].value;
                }

                if (list[left].timestamp <= timestamp)
                {
                    return list[left].value;
                }

                return string.Empty;
            }
        }


        //Minesweeper
        //https://leetcode.com/problems/minesweeper/

        //Input: 

        //[['E', 'E', 'E', 'E', 'E'],
        //['E', 'E', 'M', 'E', 'E'],
        //['E', 'E', 'E', 'E', 'E'],
        //['E', 'E', 'E', 'E', 'E']]

        //Click : [3,0]

        //Output: 

        //[['B', '1', 'E', '1', 'B'],
        //['B', '1', 'M', '1', 'B'],
        //['B', '1', '1', '1', 'B'],
        //['B', 'B', 'B', 'B', 'B']]

        int[] xdirs = new int[] { 1, 0, -1, 0, -1, -1, 1, 1 };
        int[] ydirs = new int[] { 0, 1, 0, -1, -1, 1, -1, 1 };

        public char[][] updateBoard(char[][] board, int[] click)
        {
            if (board.Length == 0) { return board; }

            if (board[click[0]][click[1]] == 'M')
            {
                board[click[0]][click[1]] = 'X';
                return board;
            }

            Queue<int[]> queue = new Queue<int[]>();
            queue.Enqueue(new int[] { click[0], click[1] });

            while (queue.Count > 0)
            {
                int[] cur = queue.Dequeue();

                if (board[cur[0]][cur[1]] != 'E') { continue; }

                char adjacentMines = getAdjacentMines(cur[0], cur[1], board);

                if (adjacentMines != '0')
                {
                    board[cur[0]][cur[1]] = adjacentMines;
                }
                else
                {
                    board[cur[0]][cur[1]] = 'B';

                    for (int i = 0; i < xdirs.Length; i++)
                    {
                        int newX = cur[0] + xdirs[i];
                        int newY = cur[1] + ydirs[i];

                        if (newX >= 0 && newY >= 0 && newX < board.Length && newY < board[0].Length
                          && board[newX][newY] == 'E')
                        {
                            queue.Enqueue(new int[] { newX, newY });
                        }
                    }
                }
            }

            return board;
        }

        private char getAdjacentMines(int row, int col, char[][] board)
        {
            int mines = 0;

            for (int i = 0; i < xdirs.Length; i++)
            {
                int newX = row + xdirs[i];
                int newY = col + ydirs[i];

                if (newX >= 0 && newY >= 0 && newX < board.Length && newY < board[0].Length
                  && board[newX][newY] == 'M')
                {
                    mines++;
                }
            }

            return (char)(mines + '0');
        }

        //380. Insert Delete GetRandom O(1)
        //https://leetcode.com/problems/insert-delete-getrandom-o1/
        public class RandomizedSet
        {

            /** Initialize your data structure here. */
            private HashSet<int> set;
            public RandomizedSet()
            {
                set = new HashSet<int>();
            }

            /** Inserts a value to the set. Returns true if the set did not already contain the specified element. */
            public bool Insert(int val)
            {
                if (set.Contains(val)) return false;
                set.Add(val);
                return true;
            }

            /** Removes a value from the set. Returns true if the set contained the specified element. */
            public bool Remove(int val)
            {
                if (!set.Contains(val)) return false;
                set.Remove(val);
                return true;
            }

            /** Get a random element from the set. */
            public int GetRandom()
            {
                if (set.Count == 0) return 0;
                var rand = new Random();
                return set.ElementAt(rand.Next(set.Count));
            }
        }

        //https://leetcode.com/problems/read-n-characters-given-read4-ii-call-multiple-times/
        //158. Read N Characters Given Read4 II - Call multiple times
        //File file("abcde"); // File is "abcde", initially file pointer (fp) points to 'a'
        //char[] buf4 = new char[4]; // Create buffer with enough space to store characters
        //read4(buf4); // read4 returns 4. Now buf4 = "abcd", fp points to 'e'
        //read4(buf4); // read4 returns 1. Now buf4 = "e", fp points to end of file
        //read4(buf4); // read4 returns 0. Now buf4 = "", fp points to end of file
        //https://leetcode.com/problems/read-n-characters-given-read4-ii-call-multiple-times/discuss/242427/C-Solution-100

        //https://leetcode.com/problems/paint-house/
        //256. Paint House
        //Input: costs = [[17,2,17],[16,16,5],[14,3,19]]
        //Output: 10
        //Explanation: Paint house 0 into blue, paint house 1 into green, paint house 2 into blue.
        //Minimum cost: 2 + 5 + 3 = 10.
        public int MinCost(int[][] costs)
        {
            int prevR = 0;
            int prevG = 0;
            int prevB = 0;

            int r = 0;
            int g = 0;
            int b = 0;

            for (int i = 0; i < costs.Length; i++)
            {
                if (i == 0)
                {
                    r = costs[i][0];
                    g = costs[i][1];
                    b = costs[i][2];
                }
                else
                {
                    r = Math.Min(prevG, prevB) + costs[i][0];
                    g = Math.Min(prevR, prevB) + costs[i][1];
                    b = Math.Min(prevR, prevG) + costs[i][2];
                }

                prevR = r;
                prevG = g;
                prevB = b;
            }

            return Math.Min(r, Math.Min(g, b));
        }

        //https://leetcode.com/problems/design-hit-counter/
        //362. Design Hit Counter
        //https://leetcode.com/problems/design-hit-counter/discuss/83483/Super-easy-design-O(1)-hit()-O(s)-getHits()-no-fancy-data-structure-is-needed!
        public class HitCounter
        {
            private int[] times;
            private int[] hits;
            /** Initialize your data structure here. */
            public HitCounter()
            {
                times = new int[300];
                hits = new int[300];
            }

            /** Record a hit.
                @param timestamp - The current timestamp (in seconds granularity). */
            public void hit(int timestamp)
            {
                int index = timestamp % 300;
                if (times[index] != timestamp)
                {
                    times[index] = timestamp;
                    hits[index] = 1;
                }
                else
                {
                    hits[index]++;
                }
            }

            /** Return the number of hits in the past 5 minutes.
                @param timestamp - The current timestamp (in seconds granularity). */
            public int getHits(int timestamp)
            {
                int total = 0;
                for (int i = 0; i < 300; i++)
                {
                    if (timestamp - times[i] < 300)
                    {
                        total += hits[i];
                    }
                }
                return total;
            }
        }
        //Average of K numbers in a stream
        //var c1 = new CalculateAverage(2);
        //double avg = c1.Add(1);
        //avg = c1.Add(2); --> 1.5
        //avg = c1.Add(3); --> 2.5
        //avg = c1.Add(4); --> 3.5
        //avg = c1.Add(5); --> 4.5
        public class CalculateAverage
        {
            Queue<int> t = null;
            double sum = 0.0;
            int maxSize = 0;

            public CalculateAverage(int k)
            {
                maxSize = k;
                t = new Queue<int>();
                sum = 0.0;
            }

            public double Add(int num)
            {
                if (t.Count() == maxSize)
                    sum -= t.Dequeue();

                t.Enqueue(num);
                sum += num;
                return sum / maxSize;
            }
        }

        public class HtmlParser
        {
            public List<String> GetUrls(String url) { return new List<string>(); }
        }
        //https://leetcode.com/problems/web-crawler-multithreaded/
        //1242. Web Crawler Multithreaded
        //https://leetcode.com/problems/web-crawler-multithreaded/discuss/554526/Simple-Task-based-code-with-comments-beats-95-on-CPU-uses-TryAdd
        ConcurrentDictionary<string, string> done = new ConcurrentDictionary<string, string>();
        HtmlParser parser;
        string hostname;
        public IList<string> Crawl(string startUrl, HtmlParser htmlParser)
        {
            this.parser = htmlParser;
            Uri myUri = new Uri(startUrl);
            hostname = @"http://" + myUri.Host;
            done.TryAdd(startUrl, startUrl);
            CCrawl(startUrl);
            return done.Keys.ToList();
        }

        private void CCrawl(string url)
        {
            var res = parser.GetUrls(url);
            List<Task> toWait = new List<Task>();

            // This will initiate child tasks. Also, it will only return when all the tasks are done.
            Parallel.ForEach(res, (item) =>
            {
                if (!done.ContainsKey(item))
                {
                    if (item.StartsWith(hostname))
                    {
                        // If TryAdd fails we will be adding duplicates.
                        if (done.TryAdd(item, item))
                            CCrawl(item);
                    }
                }
            });
        }
    }

}
