using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Prepration
{
    public class DesignQuestions
    {  
        
        public static void ExecuteTicTacToe()
        {
            int val= 0;
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
            public bool WinConditionForRow(int player, int row)
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
            public bool WinConditionForColumn(int player, int column)
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

            public bool WinConditionForFirstDiagonal(int player)
            {
                bool win = true;
                for(int i=0; i<n; i++)
                {
                    if(tic[i,i] != player)
                    {
                        win = false; break;
                    }
                }
                return win;
            }

            public bool WinConditionForSecondDiagonal(int player)
            {
                bool win = true;
                for (int i = 0; i < n; i++)
                {
                    if (tic[i, n-i-1] != player)
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

                for(int i=0; i<n; i++)
                {
                    st.Push(i);
                }
                while(st.Count > 1)
                {
                    int first = st.Pop();
                    int second = st.Pop();
                    
                    if (Knows(first, second))
                        st.Push(second);
                    else
                        st.Push(first);
                }
                int result = st.Pop();

                for(int i=0; i<n; i++)
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
                if(node!=null)
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
                ListNode current = new ListNode(key,value);

                var isExist= dict.Keys.Contains(key) ? dict[key] : null;
                if(isExist!=null)
                {
                    isExist.Value = value;
                    MovetoHead(isExist);
                }
                else
                {
                    dict.Add(key, current);
                    AddElementsInHead(current);
                    TotalItems++;
                    if(TotalItems > Capacity)
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
                ret=vi.Get(1);  // return 1;
                vi.Put(3, 3); // evicts 2;
                ret=vi.Get(2);  // return -1;
                vi.Put(4, 4); //evicts 1;
                ret=vi.Get(1); // return -1;
                ret=vi.Get(3);
                ret=vi.Get(4);
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
                foreach(Pair<int, int> pair in this.bucket)
                {
                    if (pair.first.Equals(key))
                        return pair.second;
                }
                return -1;
            }

            public void update(int key, int value)
            {
                bool found = false;
                foreach(Pair<int, int> pair in this.bucket)
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
                foreach(Pair<int, int> pair in this.bucket)
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
                if(s.Count == 0)
                {
                    while(r.Count > 0)
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
                while(count < index-1)
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
                while(count < index-1)
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
    }
}
