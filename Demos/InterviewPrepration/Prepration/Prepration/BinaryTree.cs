using System;
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

        //this below method calculate the Height.
        public int BTHeight(Node rootNode)
        {
            if (rootNode == null) return 0;
            if (rootNode.Left == null && rootNode.Right == null)
                return 1;

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

        //4. Construct the binary tree
        public static Node ConstructBinaryTree(int[] inorder, int[] postorder)
        {

            var map = new Dictionary<int, int>();
            for (int i = 0; i < inorder.Length; i++)
                map.Add(inorder[i], i);

            var node = Construct(postorder, 0, postorder.Length - 1, 0, map);
            return node;
        }

        public static Node Construct(int[] p, int s, int e, int s_, Dictionary<int, int> inorder)
        {
            if (s > e) return null;
            var root = p[e];
            var index = inorder[root];
            var node = new Node(root);
            var len = index - s_;
            node.Left = Construct(p, s, s + len - 1, s_, inorder);
            node.Right = Construct(p, s + len, e - 1, index + 1, inorder);
            return node;
        }

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

            for (int i = 0; i < inputs.Length; i++)
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
                        current.Left.Value = 2 * i;
                        q.Enqueue(current.Left);
                    }

                    if (current.Right != null)
                    {
                        current.Right.Value = 2 * i + 1;
                        q.Enqueue(current.Right);
                    }

                }
            }
            return max;
        }

        //10. Binary Search Tree to Greater Sum Tree
        //https://leetcode.com/problems/binary-search-tree-to-greater-sum-tree/discuss/381336/Simple-Java-recursion-solution-100-or-100
        //https://leetcode.com/problems/binary-search-tree-to-greater-sum-tree/discuss/323012/java-100-efficient-soln-approach1

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

        //15. InOrder traversal
        public static void InOrderTraversal(Node node)
        {
            var st = new Stack<Node>();
            while (true)
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
                    node = node.Right;
                }
            }
        }

        //16. RootToLeaf=Sum 
        public static bool RootToLeafSum(Node node, int target, List<int> path)
        {
            if (node == null) return false;
            if (node.Left == null || node.Right == null)
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

        //17. IsMirror Tree
        public static bool IsMirrorTree(Node first, Node second)
        {
            if (first == null || second == null) return true;
            //if (first.Data != second.Data) return false;
            return first.Data == second.Data && IsMirrorTree(first.Left, second.Right) && IsMirrorTree(first.Right, second.Left);
        }
        //18. IsSame Tree
        public static bool IsSameTree(Node first, Node second)
        {
            if (first == null || second == null) return true;
            //if (first.Data != second.Data) return false;
            return first.Data == second.Data && IsSameTree(first.Left, second.Left) && IsSameTree(first.Right, second.Right);
        }
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

        

    }
}
