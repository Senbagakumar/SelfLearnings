using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        class TicTacToe
        {
            int[,] tic;
            int n;
            public TicTacToe(int n)
            {
                this.n = n;
                tic = new int[3, 3];
            }

            public int Move(int row, int col, int teamPlayer)
            {
                int val = teamPlayer == 1 ? 1 : -1;
                tic[row, col] += val;

                if (WinConditionForRow(teamPlayer, row) || WinConditionForColumn(teamPlayer, col) ||
                   WinConditionForFirstDiagonal(teamPlayer) || WinConditionForSecondDiagonal(teamPlayer))
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

        class Celebrity
        {
            int[,] cele;

            public Celebrity(int n)
            {
                cele = new int[4, 4] { { 0,0,1,0 }, { 0,0,1,0 }, { 0,0,0,0}, { 0,0,1,0} };
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
                while(st.Count > 0)
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
                    if (i != result && Knows(result, i) || !Knows(i, result))
                        return -1;
                }
                return result;
            }

        }

        class LRU
        {
            public int Key { get; set; }
            public int Value { get; set; }
            public bool IsRecent { get; set; }
        }

        public class LRUCache
        {
            private int _Capacity;
            private LRU[] lru;
            private int i = 1;

            public LRUCache()
            {

            }
            public LRUCache(int capacity)
            {
                _Capacity = capacity;
                lru = new LRU[capacity];
            }

            public int Get(int key)
            {
                var l = lru.Where(t => t.Key == key).FirstOrDefault();
                if (l != null)
                {
                    l.IsRecent = true;
                    Console.WriteLine($"return={l.Value}");
                    return l.Value;
                }
                else
                {
                    Console.WriteLine($"return -1");
                    return -1;
                }
            }

            public void Put(int key, int value)
            {
                var temp= new LRU() { Key = key, Value = value };
                if (_Capacity < i)
                {
                    var l = lru.Where(t => t.IsRecent == false).FirstOrDefault();
                    Console.WriteLine($"Evicts key={l.Key} value={l.Value}");
                    l.Key = temp.Key; l.Value = temp.Value; 
                }
                else
                {
                    lru[i - 1] = temp;
                    i++;
                }
            }

            public static void Test()
            {
                var vi = new LRUCache(2);
                vi.Put(1, 1);
                vi.Put(2, 2);
                vi.Get(1);  // return 1;
                vi.Put(3, 3); // evicts 2;
                vi.Get(2);  // return -1;
                vi.Put(4, 4); //evicts 1;
                vi.Get(1); // return -1;
                vi.Get(3);
                vi.Get(4);
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
    }
}
