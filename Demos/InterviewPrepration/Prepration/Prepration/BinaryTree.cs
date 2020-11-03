using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepration
{
    public class BinaryTree
    {

        //1. Balanced Tree  https://leetcode.com/problems/balanced-binary-tree/discuss/384299/Java-Recursive-Solution

        public bool BTBalanced(Node rootNode)
        {
            if (rootNode == null) return true;
            int lh = BTHeight(rootNode.Left);
            int rh = BTHeight(rootNode.Right);

            if (Math.Abs(lh - rh) <= 1 && BTBalanced(rootNode.Left) && BTBalanced(rootNode.Right))
                return true;
            else
                return false;
        }

        //https://leetcode.com/problems/maximum-depth-of-binary-tree/
        //this below method calculate the Height/Depth
        public int BTHeight(Node rootNode)
        {
            if (rootNode == null) return 0;
            int left = BTHeight(rootNode.Left);
            int right = BTHeight(rootNode.Right);
            return Math.Max(left, right) + 1;
        }


        //2. Merge of Two Binary Trees
        // QL : https://leetcode.com/problems/merge-two-binary-trees/
        // AL : https://leetcode.com/problems/merge-two-binary-trees/discuss/374958/Simple-C-solution-in-5-lines
        public Node MergeBinaryTree(Node first, Node second)
        {
            if (first != null && second != null)
            {
                return new Node(first.Data + second.Data)
                {
                    Left = MergeBinaryTree(first.Left, second.Left),
                    Right = MergeBinaryTree(first.Right, second.Right)
                };
            }
            return first ?? second;
        }

        //Amazon Question
        //3. Diameter of Binary Tree
        //https://leetcode.com/problems/diameter-of-binary-tree/
        //https://leetcode.com/problems/diameter-of-binary-tree/discuss/375854/C-Recursive-solution
        public int DiameterOfBinaryTree(Node node)
        {
            int res = 0;
            res = DiameterRecursive(node, res);
            return res == 0 ? res : res - 1;
        }

        public int DiameterRecursive(Node node, int res)
        {
            if (node == null) return 0;
            int left = BTHeight(node.Left);
            int right = BTHeight(node.Right);
           // res = Math.Max(res, left + right);
            int resLeft = DiameterRecursive(node.Left, res);
            int resRight = DiameterRecursive(node.Right, res);
            //res = Math.Max(res, resLeft);
            //res = Math.Max(res, resRight);
            res = Math.Max(left + right + 1, Math.Max(resLeft, resRight));
            return res;
        }

        //Microsoft Question
        //4. Construct the binary tree
        public static Node ConstructBinaryTree(int[] inorder, int[] postorder, bool preorder=false)
        {

            var map = new Dictionary<int, int>();
            for (int i = 0; i < inorder.Length; i++)
                map.Add(inorder[i], i);

            var node = Construct(postorder, 0, postorder.Length - 1, 0, map, preorder);
            return node;
        }

        public static Node Construct(int[] p, int s, int e, int s_, Dictionary<int, int> inorder, bool preorder=false)
        {
            if (s > e) return null;
            
            var root = 0;
            if (preorder)
               root = p[s];
            else
               root = p[e];
            var index = inorder[root];
            var node = new Node(root);
            var len = index - s_;

            if (!preorder)
            {
                node.Left = Construct(p, s, s + len - 1, s_, inorder, preorder);
                node.Right = Construct(p, s + len, e - 1, index + 1, inorder, preorder);
            }
            else
            {
                node.Left = Construct(p, s + 1, s + len, s_, inorder, preorder);
                node.Right = Construct(p, s + len + 1, e, index+1, inorder, preorder);
            }

            return node;
        }

        //Amazon Question
        //Microsoft Question
        //5. Print the elements by level
        public static void BFSTPrintByLevel(Node input)
        {
            Queue<Node> node = new Queue<Node>();
            node.Enqueue(input);
            node.Enqueue(null);

            while (true)
            {
                Node currnt = node.Dequeue();
                if (currnt != null)
                {
                    Console.Write(currnt.Data);

                    if (currnt.Left != null)
                    {
                        node.Enqueue(currnt.Left);
                    }
                    if (currnt.Right != null)
                    {
                        node.Enqueue(currnt.Right);
                    }
                }
                else
                {
                    Console.WriteLine();

                    if (node.Count > 0)
                    {
                        node.Enqueue(null);

                    }
                    else
                        break;
                }

            }
        }

        //Amazon Question
        //Microsoft Question
        //6. Print the elements by Zig-Zage
        public static void BFSTPrintByZigZagLevel(Node input)
        {
            Queue<Node> node = new Queue<Node>();
            node.Enqueue(input);
            //node.Enqueue(null);
            List<List<int>> result = new List<List<int>>();
            int count = 0;
            while (true)
            {
                List<int> tmp = new List<int>();
                var len = node.Count;
                for (int i = 0; i < len; i++)
                {
                    Node currnt = node.Dequeue();

                    if (currnt.Left != null)
                    {
                        node.Enqueue(currnt.Left);
                    }
                    if (currnt.Right != null)
                    {
                        node.Enqueue(currnt.Right);
                    }

                    tmp.Add(currnt.Data);
                }

                if (count % 2 != 0)
                {
                    int[] arr = tmp.ToArray();
                    Array.Reverse(arr);
                    tmp = arr.ToList();
                }

                result.Add(tmp);

                count++;

                if (node.Count == 0)
                    break;
            }
        }

        public static void BFSTPrintByZigZag(Node input)
        {
            var st1 = new Stack<Node>();
            var st2 = new Stack<Node>();

            st1.Push(input);

            while(true)
            {
                while(st1.Count > 0)
                {
                    var current = st1.Pop();
                    if (current.Left != null) st2.Push(current.Left);
                    if (current.Right != null) st2.Push(current.Right);
                    Console.Write(current.Data);
                }
                while(st2.Count > 0)
                {
                    var current = st2.Pop();
                    if (current.Right != null) st1.Push(current.Right);
                    if (current.Left != null) st1.Push(current.Left);
                    Console.Write(current.Data);
                }

                if (st1.Count == 0 && st2.Count == 0)
                    break;
            }
        }

        //7. UniValued Binary Tree
        public static bool UniValueTree(Node input, int value)
        {
            if (input == null) return true;
            return input.Data == value && UniValueTree(input.Left, value) && UniValueTree(input.Right, value);
        }

        //8. Maximum Binary tree
        /*
            The root is the maximum number in the array.
            The left subtree is the maximum tree constructed from left part subarray divided by the maximum number.
            The right subtree is the maximum tree constructed from right part subarray divided by the maximum number.
        */

        //https://leetcode.com/problems/maximum-binary-tree/
        public static Node ConstructMaximumTree(int[] inputs, int start, int end)
        {
            if (start > end) return null;

            int maxNum = int.MinValue;
            int maxIndex = int.MinValue;

            for (int i = start; i < end; i++)
            {
                if (inputs[i] > maxNum)
                {
                    maxNum = inputs[i];
                    maxIndex = i;
                }
            }

            var root = new Node(maxNum);
            root.Left = ConstructMaximumTree(inputs, start, maxIndex - 1);
            root.Right = ConstructMaximumTree(inputs, maxIndex + 1, end);
            return root;

        }

        //9. Maximum Width of Binarty tree
        //https://leetcode.com/problems/maximum-width-of-binary-tree/discuss/246673/C-BFS-with-Queue
        public static int MaximumWidthOfBinaryTree(Node node)
        {
            if (node == null) return 0;
            var q = new Queue<Node>();
            q.Enqueue(node);
            node.Value = 1;
            var max = 1;

            while (q.Count != 0)
            {
                int count = q.Count;
                max = Math.Max(max, q.LastOrDefault().Value-q.Peek().Value + 1);
                for (int i = 0; i < count; i++)
                {
                    var current = q.Dequeue();
                    if (current.Left != null)
                    {
                        current.Left.Value = current.Value * i;
                        q.Enqueue(current.Left);
                    }

                    if (current.Right != null)
                    {
                        current.Right.Value = current.Value * i + 1;
                        q.Enqueue(current.Right);
                    }

                }
            }
            return max;
        }

        //Amazon Question
        //10. Binary Search Tree to Greater Sum Tree
        //https://leetcode.com/problems/binary-search-tree-to-greater-sum-tree/discuss/381336/Simple-Java-recursion-solution-100-or-100
        //https://leetcode.com/problems/binary-search-tree-to-greater-sum-tree/

        public static Node GreaterSumTree(Node node)
        {
            SumTree(node);
            return node;
        }

        static int count = 0;
        public static void SumTree(Node node)
        {
            if (node == null) return;
            SumTree(node.Right);
            count += node.Data;
            node.Value = count;
            SumTree(node.Left);
        }

        //11. Invert Tree
        //https://leetcode.com/problems/invert-binary-tree/
        public static Node InvertTree(Node node)
        {
            if (node == null) return null;

            Queue<Node> qnode = new Queue<Node>();
            qnode.Enqueue(node);

            while (qnode.Count > 0)
            {
                Node current = qnode.Dequeue();

                var temp = current.Left;
                current.Left = current.Right;
                current.Right = temp;

                if (current.Left != null)
                    qnode.Enqueue(current.Left);
                if (current.Right != null)
                    qnode.Enqueue(current.Right);
            }
            return node;
        }

        //12. Find the sum by level
        public int BFSTraversal(Node input)
        {
            string sv = string.Empty;
            if (input == null) return 0;
            Queue<Node> node = new Queue<Node>();
            node.Enqueue(new Node(input.Data));
            node.Enqueue(null);

            int tempValue = 0;
            int previousValue = 0;


            while (true)
            {
                Node currnt = node.Dequeue();
                if (currnt != null)
                {
                    tempValue += currnt.Data;

                    if (currnt.Left != null)
                    {
                        node.Enqueue(currnt.Left);
                    }
                    else if (currnt.Right != null)
                    {
                        node.Enqueue(currnt.Right);
                    }
                }
                else
                {
                    if (previousValue < tempValue)
                    {
                        previousValue = tempValue;
                    }
                    tempValue = 0;

                    if (node.Count > 0)
                    {
                        node.Enqueue(null);

                    }
                    else
                        break;
                }

            }

            return previousValue;
        }

        //13. PostOrder traversal
        public static void PostOrderTraversal(Node node)
        {
            var st1 = new Stack<Node>();
            var st2 = new Stack<Node>();
            st1.Push(node);
            while (st1.Count > 0)
            {
                var current = st1.Pop();
                st2.Push(current);

                if (current.Left != null)
                    st1.Push(current.Left);
                if (current.Right != null)
                    st1.Push(current.Right);
            }
            while (st2.Count > 0)
            {
                Console.WriteLine(st2.Pop());
            }
        }

        //14. PreOrder traversal
        public static void PreOrderTraversal(Node node)
        {
            var root = new Stack<Node>();
            root.Push(node);
            while (root.Count > 0)
            {
                var current = root.Pop();
                Console.WriteLine(current.Data);
                if (current.Right != null)
                    root.Push(current.Right);
                if (current.Left != null)
                    root.Push(current.Left);
            }
        }

        //Microsoft Question
        //15. InOrder traversal
        public static void InOrderTraversal(Node node)
        {
            var st = new Stack<Node>();
            while (node!=null || st.Count > 0)
            {
                if (node != null)
                {
                    st.Push(node);
                    node = node.Left;
                }
                else
                {
                    var pnode = st.Pop();
                    Console.WriteLine(pnode.Data);
                    node = pnode.Right;
                }
            }
        }

        //15.1 
        //https://leetcode.com/problems/kth-smallest-element-in-a-bst/
        public int KthSmallest(Node root, int k)
        {
            Stack<Node> stack = new Stack<Node>();
            while (root != null || stack.Count > 0)
            {
                while (root != null)
                {
                    stack.Push(root);
                    root = root.Left;
                }
                root = stack.Pop();
                if (--k == 0) break;
                root = root.Right;
            }
            return root.Data;
        
    }
        //15.2  https://leetcode.com/problems/recover-binary-search-tree/
        // aNSWER : https://leetcode.com/problems/recover-binary-search-tree/discuss/466643/2ms-Java-solution-(96.5)-with-explanation



        //15.3  https://leetcode.com/problems/find-mode-in-binary-search-tree/
        //https://leetcode.com/problems/find-mode-in-binary-search-tree/discuss/455422/C-recursive-O(n)-time-O(1)-space


        //15.4 https://leetcode.com/problems/subtree-of-another-tree/
        //https://leetcode.com/problems/subtree-of-another-tree/discuss/474425/Java-Naive-and-Optimized-Preorder-Traversal-Solutions
        public static bool isSubtree(Node s, Node t) // here s is the whole tree, t is a subtree
        {
            if (isEqualTree(s, t)) return true;
            if (s == null) return false;
            return (isSubtree(s.Left, t) || isSubtree(s.Right, t));
        }

        private static bool isEqualTree(Node s, Node t)
        {
            if (s == null || t == null) return t == s;
            return s.Value == t.Value && isEqualTree(s.Left, t.Left) && isEqualTree(s.Right, t.Right);
        }

        //16. RootToLeaf=Sum  Path Sum 1
        public static bool RootToLeafSum(Node node, int target, List<int> path)
        {
            if (node == null) return false;
            if (node.Left == null && node.Right == null)
            {
                if (node.Data == target)
                {
                    path.Add(node.Data);
                    return true;
                }
                else
                    return false;
            }
            if (RootToLeafSum(node.Left, target - node.Data, path))
            {
                path.Add(node.Data);
                return true;
            }
            if (RootToLeafSum(node.Right, target - node.Data, path))
            {
                path.Add(node.Data);
                return true;
            }
            return false;
        }

        //Path Sum 2
        //https://leetcode.com/problems/path-sum-ii/
        //Given a binary tree and a sum, find all root-to-leaf paths where each path's sum equals the given sum.

        public List<List<int>> ans = new List<List<int>>();

        public List<List<int>> pathSum2(Node root, int sum)
        {
            helper(root, sum, new List<int>());
            return ans;
        }

        public void helper(Node node, int sum, List<int> arr)
        {

            if (node == null) return;

            arr.Add(node.Value);

            if (node.Left == null && node.Right == null && sum == node.Value) ans.Add(new List<int>(arr));

            helper(node.Left, sum - node.Value, arr);
            helper(node.Right, sum - node.Value, arr);

            arr.Remove(arr.Count - 1);

        }


        //Path Sum 3
        //https://leetcode.com/problems/path-sum-iii/
        //Find the number of paths that sum to a given value.
        private int Count = 0;
        public int pathSum3(Node root, int sum)
        {
            getPathSum(root, sum);
            return Count;
        }

        private void getPathSum(Node node, int sum)
        {
            if (node == null)
                return;
            getSumIncludingRoot(node, sum);
            getPathSum(node.Left, sum);
            getPathSum(node.Right, sum);
        }


        private void getSumIncludingRoot(Node node, int sum)
        {
            if (node == null)
                return;

            if (node.Value == sum)
            {
                Count++;
            }
            getSumIncludingRoot(node.Left, sum - node.Value);
            getSumIncludingRoot(node.Right, sum - node.Value);
        }



        //Amazon Question
        //17. IsMirror Tree
        public static bool IsMirrorTree(Node first, Node second)
        {
            if (first == null || second == null) return true;
            //if (first.Data != second.Data) return false;
            return first.Data == second.Data && IsMirrorTree(first.Left, second.Right) && IsMirrorTree(first.Right, second.Left);
        }
        //18. IsSame Tree
        //https://leetcode.com/problems/symmetric-tree/
        public static bool IsSameTree(Node first, Node second)
        {
            if (first == null || second == null) return true;
            //if (first.Data != second.Data) return false;
            return first.Data == second.Data && IsSameTree(first.Left, second.Left) && IsSameTree(first.Right, second.Right);
        }

        //Microsoft Question
        //19. Low Anchestor for BST
        public static Node LowestCommonAncestorForBST(Node root, Node first, Node second)
        {
            if (root.Data > Math.Max(first.Data, second.Data))
                return LowestCommonAncestorForBST(root.Left, first, second);
            else if (root.Data < Math.Min(first.Data, second.Data))
                return LowestCommonAncestorForBST(root.Right, first, second);
            else
                return root;
        }

        //Amazon Question
        //Microsoft Question
        //20. Low Anchestor for BT
        public static Node LowestCommonAncestorForBT(Node root, Node first, Node second)
        {
            if (root == null) return null;
            if (root.Data == first.Data || root.Data == second.Data) return root;
            Node left = LowestCommonAncestorForBT(root.Left, first, second);
            Node right = LowestCommonAncestorForBT(root.Right, first, second);
            if (left != null && right != null) return root;
            if (left == null && right == null) return null;
            return left ?? right;
        }

        //Amazon Question
        //Microsoft Question
        //21. Serialize Deserialize the binary Tree

        public static void SeriaizeDeserializeBinaryTree(Node node)
        {
            SerializeBinaryTree(node, -1);
            System.IO.File.Delete(@"C:\log\log.txt");
            string value = System.IO.File.ReadAllText(@"C:\log\log.txt").Trim();
            string[] nodes = value.Split('\t');
            var rnode = DeserializeBinryTree(-1, nodes);
        }

        private static void SerializeBinaryTree(Node node, int skipValue)
        {
            if(node == null)
            {
                System.IO.File.AppendAllText(@"C:\log\log.txt", skipValue.ToString()+"\t");
                return;
            }
            System.IO.File.AppendAllText(@"C:\log\log.txt", node.Data.ToString() + "\t");
            SerializeBinaryTree(node.Left,skipValue);
            SerializeBinaryTree(node.Right, skipValue);
        }
        static int iteration = 0;
        private static Node DeserializeBinryTree(int skipValue,string[] serializevalue)
        {
            var node = serializevalue[iteration++];
            int inode = Convert.ToInt16(node);
            if (inode == skipValue)
                return null;
            var root = new Node(inode);
            root.Left = DeserializeBinryTree(skipValue, serializevalue);
            root.Right = DeserializeBinryTree(skipValue, serializevalue);
            return root;
        }

        //Amazon Question
        //Microsoft Interview Question
        //22. BT is BST
        public static bool IsBST(Node node, int min, int max)
        {
            if (node == null) return true;
            if (node.Value < min || node.Value > max) return false;
            return IsBST(node.Left, min, node.Value) && IsBST(node.Right, node.Value, max);
        }   

        //23. Print BST keys in the given range
        // 20->L->8   8->L->4 8->R->12
        // 20->R->22
        // https://www.geeksforgeeks.org/print-bst-keys-in-the-given-range/
        public static void PrintBSTKeysBetweenRange(Node node, int min, int max)
        {
            if (node == null) return;

            if (node.Data > min)
                PrintBSTKeysBetweenRange(node.Left, min, max);
            if (node.Data >= min && node.Data <= max)
                Console.WriteLine(node.Data);

            if (node.Data < max)
                PrintBSTKeysBetweenRange(node.Right, min, max);
        }

        //24.https://www.geeksforgeeks.org/the-celebrity-problem/

        //25. Convert a given tree to its Sum Tree
        //https://www.geeksforgeeks.org/convert-a-given-tree-to-sum-tree/

        //26. Print nodes at k distance from root
        //https://www.geeksforgeeks.org/print-nodes-at-k-distance-from-root/

        public static void PrintAllNodeFromKDistance(Node node, int k)
        {
            if (node == null) return;
            if (k == 0)
            {
                Console.WriteLine(node.Data);

            }
            else
            {
                PrintAllNodeFromKDistance(node.Left, k - 1);
                PrintAllNodeFromKDistance(node.Right, k - 1);
            }
        }

        //27. Transform in SomeTree
        //https://www.geeksforgeeks.org/convert-a-given-tree-to-sum-tree/
        private static int TransformSumTree(Node node)
        {
            if (node == null) return 0;
            var oldValue = node.Data;
            var newValue = TransformSumTree(node.Left)+ TransformSumTree(node.Right);
            return oldValue + newValue;
        }
        private static void PrintSumTree(Node node)
        {
            if (node == null) return;
            PrintSumTree(node.Left);
            Console.WriteLine(node.Data);
            PrintSumTree(node.Right);

        }

        public static void TransformAndPrintSumTree(Node node)
        {
            TransformSumTree(node);
            PrintSumTree(node);
        }

        //Microsoft Question
        //https://leetcode.com/problems/populating-next-right-pointers-in-each-node/
        //Populating Next Right Pointers in Each Node
        //https://leetcode.com/problems/populating-next-right-pointers-in-each-node/discuss/520202/Accepted-C-(Queue%2BPeek)-solution%3A-Easy-to-understand
        public Node PopulateNextPointer(Node node)
        {
            var queue = new Queue<Node>();
            if (node != null)
                queue.Enqueue(node);
            while(queue.Count > 0)
            {
                var nqueue = new Queue<Node>();
                while(queue.Count > 0)
                {
                    var cnode = queue.Dequeue();
                    cnode.Right = queue.Count == 0 ? null : queue.Peek();
                    if (cnode.Left != null)
                        nqueue.Enqueue(cnode.Left);
                    if (cnode.Right != null)
                        nqueue.Enqueue(cnode.Right);
                }
                queue = nqueue;
            }
            return node;
        }


        //https://www.geeksforgeeks.org/fix-two-swapped-nodes-of-bst/
        //Two of the nodes of a Binary Search Tree(BST) are swapped.Fix(or correct) the BST.
        Node first, middle, last, previous;
        private void CorrectBSTUtil(Node root)
        {
            if (root != null)
            {
                CorrectBSTUtil(root.Left);

                if (previous != null && root.Value < previous.Value)
                {
                    if (first == null)
                    {
                        first = previous;
                        middle = root;
                    }
                    else
                        last = root;
                }
                previous = root;
                CorrectBSTUtil(root.Right);
            }
        }

        public void CorrectBST(Node node)
        {
            first = middle = last = previous = null;
            CorrectBSTUtil(node);
            if (first != null && last != null)
            {
                var temp = first.Value;
                first.Value = last.Value;
                last.Value = temp;
            }
            else if (first != null && middle != null)
            {
                var temp = first.Value;
                first.Value = middle.Value;
                middle.Value = temp;
            }
        }

        public void PrintInOrder(Node node)
        {
            if (node == null) return;
            PrintInOrder(node.Left);
            Console.WriteLine(node.Value);
            PrintInOrder(node.Right);
        }

        //https://leetcode.com/problems/sum-of-left-leaves/
        //Find the sum of all left leaves in a given binary tree.
        public static int SumLeft(Node root, bool isLeft)
        {
            if (root == null)
                return 0;
            if (root.Left == null && root.Right == null && isLeft)
                return root.Value;
            return SumLeft(root.Left, true) + SumLeft(root.Right, false);
        }

        //https://leetcode.com/problems/flatten-binary-tree-to-linked-list/
        //Flatten Binary Tree to Linked List
        public static void Flatten(Node root)
        {
            Help(root);
        }

        public static Node Help(Node root)
        {
            if (root == null) return root;
            if (root.Left == null && root.Right == null) return root;


            Node left = Help(root.Left);
            Node right = Help(root.Right);

            Node tleft = root.Left;
            Node tright = root.Right;

            if (left != null)
            {
                left.Right = tright;
                root.Right = tleft;
                root.Left = null;
            }
            return right != null ? right : left;
        }

        //https://leetcode.com/problems/binary-tree-right-side-view/
        public IList<int> RightSideView(Node root)
        {
            IList<int> res = new List<int>();
            Dictionary<int, int> map = new Dictionary<int, int>();
            Result(root, res, map, 0);
            return res;
        }
        public void Result(Node root, IList<int> res, Dictionary<int, int> map, int level)
        {
            if (root == null) return;
            if (!map.ContainsKey(level))
            {
                map[level] = root.Value;
                res.Add(root.Value);
            }
            level++;
            Result(root.Right, res, map, level);
            Result(root.Left, res, map, level);
        }

        //https://leetcode.com/problems/binary-tree-paths/
        //Binary Tree Path.
        public List<String> binaryTreePaths(Node root)
        {
            List<String> result = new List<string>();
            return printUtil2(root, result, "");
        }
        public List<String> printUtil2(Node root, List<String> result, String path)
        {
            if (root == null)
            {
                return result;
            }
            path = path + root.Value.ToString();
            if (root.Left == null && root.Right == null)
            {
                result.Add(path);
                return result;
            }

            printUtil2(root.Left, result, path + "->");
            printUtil2(root.Right, result, path + "->");

            return result;
        }

        //https://leetcode.com/problems/count-complete-tree-nodes/
        //Given a complete binary tree, count the number of nodes.

        public int CountNodes(Node root)
        {
            if (root == null)
            {
                return 0;
            }
            return 1 + CountNodes(root.Left) + CountNodes(root.Right);
        }

    }
}
