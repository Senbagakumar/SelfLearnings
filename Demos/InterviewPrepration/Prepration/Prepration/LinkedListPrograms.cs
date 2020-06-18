using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepration
{
    class LinkedList
    {
        public LinkedList() { }
        public LinkedList(int num)
        {
            AddNode(num);
        }
        //https://leetcode.com/problems/design-linked-list/discuss/378688/Design-Linked-List-Java
        private Node head;
        private int size;
        public Node Head { get { return head; } }

        public void AddNode(int value) // At Tail
        {
            var currentNode = new Node(value);

            if (head == null)
            {
                head = currentNode;
            }
            else
            {
                Node current = head;
                while (current.Next != null)
                    current = current.Next;

                current.Next = currentNode;
            }
            size++;
        }

        public void AddAtHead(int value)
        {
            var cNode = new Node(value);
            if (head == null)
                head = cNode;
            else
            {
                cNode.Next = head;
                head = cNode;
            }
            size++;
        }

        public void AddNodeAtIndex(int value,int index)
        {
            if (index > size)
                return;

            var cNode = new Node(value);
            var current = head;
            for (int i = 0; i < index-1; i++)
                current = current.Next;

            var temp = current.Next; //current Index Value
            current.Next = cNode; // New Value
            cNode.Next = temp; // Assign
        }

        public void DeleteNode(int value)
        {
            var cNode = head;
            Node previous = null;
            while (cNode != null)
            {
                previous = cNode;
                if (cNode.Value == value)
                {
                    previous = cNode.Next;
                    cNode = previous.Next;
                }
                else
                {
                    cNode = cNode.Next;
                }
            }
        }

        public int GetSize()
        {
            return size;
        }
        public Node GetHeadNode()
        {
            return head;
        }
    }
    class LinkedListPrograms
    {
        /*1. Given a singly linked list, group all odd nodes together followed by the even nodes. 
         * Please note here we are talking about the node number and not the value in the nodes
         * Odd Even Linked List */

        public static Node OddEvenLinkedList()
        {
            // Prepare the Inputs
            LinkedList input = new LinkedList();
            input.AddNode(1);
            input.AddNode(2);
            input.AddNode(3);
            input.AddNode(4);
            input.AddNode(5);
            input.AddNode(6);
            //input.AddNode(new Node(7));
            //input.AddNode(new Node(8));


            if (input == null) return null;

            Node head = input.GetHeadNode();

            Node odd = head, even = head.Next, evenHead = even;
            while (odd != null && even != null)
            {
                odd.Next = even.Next;
                if (odd.Next != null)
                {
                    odd = odd.Next;
                }
                even.Next = odd.Next;
                even = even.Next;
            }
            if (odd != null)
                odd.Next = evenHead;

            return head;
        }

        //Amazon Question
        //Microsoft Question
        //2. Merge Sorted two linked list
        public static Node MergeSortedTwoList()
        {
            LinkedList firstList = new LinkedList();
            firstList.AddNode(1);
            firstList.AddNode(3);
            firstList.AddNode(5);
            firstList.AddNode(7);
            firstList.AddNode(9);
            firstList.AddNode(10);

            LinkedList secondList = new LinkedList();
            secondList.AddNode(2);
            secondList.AddNode(4);
            secondList.AddNode(6);
            secondList.AddNode(8);

            Node mergeList = SortedMerge(firstList.GetHeadNode(), secondList.GetHeadNode());
            return mergeList;
        }

        static Node SortedMerge(Node headA, Node headB)
        {

            if (headA == null)
                return headB;

            if (headB == null)
                return headA;

            if (headA.Data <= headB.Data)
            { 
                headA.Next = SortedMerge(headA.Next, headB);
                return headA;
            }
            else
            {
                headB.Next = SortedMerge(headA, headB.Next);
                return headB;
            }
        }

        /* https://leetcode.com/problems/remove-duplicates-from-sorted-list-ii/discuss/320431/Simple-JAVA-solution-beats-100 
         3. Given a sorted linked list, delete all nodes that have duplicate numbers, leaving only distinct numbers from the original list.
         */
        static Node DeleteDuplicates(Node head)
        {
            if (head == null || head.Next == null) return null;
            Node current = head.Next;
            Node previous = head;

            while (current != null)
            {
                if (previous.Value == current.Value)
                {
                    previous = current.Next;
                    current = previous.Next;
                }
                else
                {
                    previous = current;
                    current = current.Next;
                }
            }
            return head;
        }

        /*
         4. Given a non-empty, singly linked list with head node head, return a middle node of linked list.
         If there are two middle nodes, return the second middle node.
         */
        static Node MiddleNodeInTheList(Node head)
        {
            var map = new Dictionary<double, Node>();
            var value = 1.0;
            var current = head;
            while(current!=null)
            {
                map.Add(value, current);
                value++;
                current = current.Next;
            }
            var mid = Math.Ceiling(value / 2);
            return map[mid];
        }

        //https://leetcode.com/problems/reverse-linked-list/
        //Amazon Question
        //Microsoft Question
        /*5. Reverse a singly linked list.*/
        public static Node ReverseLinkedList(Node node)
        {
            if (node == null || node.Next == null) return node;
            var next = node.Next;
            var reverseNode = ReverseLinkedList(next);
            next.Next = node;
            node.Next = null;
            return reverseNode;
        }

        public static Node ReverseLinkedListIterative(Node node)
        {
            Node current = node;
            Node forward = null;
            Node previous = null;

            while(current!=null)
            {
                forward = current.Next;
                current.Next = previous;
                previous = current;
                current = forward;
            }
            return previous;
        }

        //swap two linked list
        //q: https://leetcode.com/problems/swap-nodes-in-pairs/
        //a: https://leetcode.com/problems/swap-nodes-in-pairs/discuss/480539/C-80ms-Runtime.-Fastest-Iterative-Solution-with-Simple-and-Easy-to-understand-code.
        public static Node SwapPairs(Node head)
        {
            if (head == null || head.Next == null) return head;

            Node result = head.Next;
            //the second node will be the head after all swaps, so we keep a variable with reference to it.
            Node firstNode = null;
            Node secondNode = null;
            var li= new LinkedList(0);
            Node previousNode = li.Head;

            while (head != null && head.Next != null)
            {
                firstNode = head;
                secondNode = head.Next;


                firstNode.Next = secondNode.Next;
                
                secondNode.Next = firstNode;
                previousNode.Next = secondNode;
                //secondNode will be the first node in the swapped pair. It is used to link previous and current pairs together.
                previousNode = firstNode;
                //firstNode will be the last node in the swapped pair for the next iteartion.
                head = firstNode.Next;
                //we cannot use head.next here because nodes are modified in every iteration and head.next will give false results.
            }

            return result;
        }

        /*6. Delete a node in linked list
         * https://leetcode.com/problems/delete-node-in-a-linked-list/discuss/388433/Java-100-time-100-space
         */
        static Node DeleteNodeInLinkedList(Node head, int deleteNode)
        {
            var current = head;
            Node previous = null;
            while(current != null)
            {
                previous = current;
                if(current.Value == deleteNode)
                {
                    previous = current.Next;
                    current = previous.Next;
                }
                else
                {
                    current = current.Next;
                }
            }
            return head;
        }

        //https://leetcode.com/problems/linked-list-cycle/
        ////Microsoft Question
        /*7. Detect Linked List 
         */
        static bool DetectListCycle(Node node)
        {
            if (node == null) return false;
            Node fast = node.Next;
            Node slow = node;

            while (slow != null && fast != null)
            {
                if (slow.Value == fast.Value)
                    return true;

                slow = slow.Next;
                fast = fast.Next;
            }
            return false;
        }

        /*8. Palindrome Linked List
         * 1. use Array
           2. call stack
        */
        static bool RecursivePalindromeUsingList(Node node)
        {
            var array = new List<Node>();
            Node current = node;
            while(current != null)
            {
                array.Add(current);
                current = current.Next;
            }
            int length = array.Count;
            for(int i=0; i<length/2;i++)
            {
                if (array[i].Value != array[length-1-i].Value)
                    return false;
            }
            return true;
        }
        //2 
        public class Result
        {
            public bool isPalindrome = true;
        }
        public static Node RecursivePalindrome(Node slow, Node fast,Result result)
        {
            if (fast == null) return slow;
            if (fast.Next == null) return slow.Next;
            Node recur = RecursivePalindrome(slow.Next, fast.Next.Next, result);
            if (recur.Value != slow.Value)
                result.isPalindrome = false;
            return recur.Next;
        }

        //Microsoft Question
        //9. Intersection of Two linked list
        //https://leetcode.com/problems/intersection-of-two-linked-lists/discuss/386874/JAVA-easy-O(n)-solution-beats-~99
        static Node IntersecionPointOfLinkedList(Node first, Node second)
        {
            int fsize = GetLength(first);
            int ssize = GetLength(second);

            Node smaller = fsize > ssize ? second : first;
            Node greater = fsize > ssize ? first : second;

            int gap = fsize - ssize;
            for (int i = 0; i <= Math.Abs(gap); i++)
                greater = greater.Next;

            while(smaller!=null && greater!=null)
            {
                if (smaller.Value == greater.Value)
                    return smaller;
                smaller = smaller.Next;
                greater = greater.Next;
            }
            return null;
        }
        static int GetLength(Node node)
        {
            int i = 0;
            while(node!=null)
            {
                i++;
                node = node.Next;
            }
            return i;
        }

        //https://leetcode.com/problems/convert-sorted-list-to-binary-search-tree/discuss/384314/java-faster-than-100-and-less-than-100
        //Convert Sorted List to Binary Search Tree



        //Remove Duplicates from Sorted List II  
        //Given a sorted linked list, delete all nodes that have duplicate numbers, leaving only distinct numbers from the original list.

        //Amazon Question
        //Microsoft Question
        //10. Sum of Two linked list or Add two liked list
        static int carry = 0;
        public static void SumOfTwoLinkedList()
        {
            var llist = new LinkedList();
            var a = new LinkedList();
            a.AddNode(9); a.AddNode(9); a.AddNode(9);
            var b = new LinkedList();
            b.AddNode(1); b.AddNode(1); //b.AddNode(1);
            //, Node b

            int asize = a.GetSize();
            int bsize = b.GetSize();

            int different = Math.Abs(asize - bsize);

            if(different > 0)
            {
                if(asize < bsize)
                {
                    while(different-- <= 0)
                    {
                        a.AddAtHead(0);
                    }
                }
                else
                {
                    while (different-- <= 0)
                    {
                        b.AddAtHead(0);
                    }
                }
            }

            SumOfLinkedList(a.GetHeadNode(), b.GetHeadNode(), llist);

            if (carry > 0)
                llist.AddAtHead(carry);

        }
        public static void SumOfLinkedList(Node a, Node b, LinkedList li)
        {
            if (a == null && b == null)
                return;
            SumOfLinkedList(a.Next, b.Next,li);
            int sum = a.Value + b.Value + carry;
            carry = sum / 10;
            sum = sum % 10;
            li.AddAtHead(sum);
        }

        //11. Delete middle of linked list
        //https://www.geeksforgeeks.org/delete-middle-of-linked-list/

        public static Node DeleteMiddleNode(Node node) 
        {
            if (node == null || node.Next == null) return null;
            Node fast = node;
            Node slow = node;
            Node previous = null;
            while(fast!=null && fast.Next!=null)
            {
                previous = slow;
                slow = slow.Next;
                fast = fast.Next.Next;
            }
            previous.Next = slow.Next;
            return node;
        }

        //https://leetcode.com/problems/remove-nth-node-from-end-of-list/
        //Given linked list: 1->2->3->4->5, and n = 2.
        //After removing the second node from the end, the linked list becomes 1->2->3->5.

        public Node RemoveNthFromEnd(Node head, int n)
        {
            Node fast = head;
            Node slow = head;
            Node pre = null;

            for (int i = 0; i < n; i++)
                fast = fast.Next;

            while (fast != null)
            {
                fast = fast.Next;
                pre = slow;
                slow = slow.Next;
            }
            pre.Next = slow.Next;
            return head;
        }


        //12. Binary Tree to Doubly Linked List
        //https://www.geeksforgeeks.org/convert-a-given-binary-tree-to-doubly-linked-list-set-4/


        //13. Last 3rd Linked list
        public static Node GetLast3rdLinkedList(Node node, int n=3)
        {
            Node current = node;
            Node fast = node;

            for (int i = 0; i < n; i++)
            {
                fast = fast.Next;
            }

            while(fast!=null)
            {
                current = current.Next;
                fast = fast.Next;
            }
            return current;
        }

        //Amazon Question
        //Microsoft Question
        //Copy lis with Random pointer
        // https://leetcode.com/problems/copy-list-with-random-pointer/
        // https://leetcode.com/problems/copy-list-with-random-pointer/discuss/520109/Java-HashMap-with-Time-O(n)-and-Space-O(n)
        private static Dictionary<Node, Node> map;
        public Node CopyRandomList(Node head)
        {
            // write your code here
            map = new Dictionary<Node, Node>();
            return CopyHelper(head);
        }
        private Node CopyHelper(Node head)
        {
            if (head == null) return null;
            if (map.ContainsKey(head)) return map[head];
            Node newHead = new Node(head.Value);
            map.Add(head, newHead);
            newHead.Next = CopyHelper(head.Next);
            newHead.Random = CopyHelper(head.Random);
            return newHead;
        }

        //https://leetcode.com/problems/reorder-list/
        //Given a singly linked list L: L0→L1→…→Ln-1→Ln,
        //reorder it to: L0→Ln→L1→Ln-1→L2→Ln-2→…
        //Given 1->2->3->4, reorder it to 1->4->2->3.
        //Given 1->2->3->4->5, reorder it to 1->5->2->4->3.
        //https://leetcode.com/problems/reorder-list/discuss/628313/Simple-Java-Solution-faster-than-99.83
        public void ReorderList(Node head)
        {

            if (head == null || head.Next == null)
                return;

            Node slow = head;
            Node fast = head;

            while (fast != null && fast.Next != null)
            {
                slow = slow.Next;
                fast = fast.Next.Next;
            }
            Node reversedSecondHalf = reverse(slow);

            while (head != slow)
            {
                Node nextOfFirstHalf = head.Next;
                head.Next = reversedSecondHalf;

                Node nextOfreversedSecondHalf = reversedSecondHalf.Next;
                reversedSecondHalf.Next = nextOfFirstHalf;

                reversedSecondHalf = nextOfreversedSecondHalf;
                head = nextOfFirstHalf;
            }
            head.Next = null;
        }

        private Node reverse(Node secondHalf)
        {
            Node prev = null;
            while (secondHalf != null)
            {

                Node next = secondHalf.Next;
                secondHalf.Next = prev;
                prev = secondHalf;
                secondHalf = next;
            }
            return prev;
        }

        //https://leetcode.com/problems/convert-binary-search-tree-to-sorted-doubly-linked-list/
        //Convert Binary Search Tree to Sorted Doubly Linked List
        //Convert a Binary Search Tree to a sorted Circular Doubly-Linked List in place.
        //You can think of the left and right pointers as synonymous to the predecessor and successor pointers in a doubly-linked list.
        //For a circular doubly linked list, the predecessor of the first element is the last element, and the successor of the last element is the first element.

        //Input: root = [4,2,5,1,3]  Output: [1,2,3,4,5]

        //        4
        //    2        5
        //1       3
        Node prev;
        public Node TreeToDoublyList(Node root)
        {
            if (root == null) return null;

            Node dummy = new Node(0);
            prev = dummy;

            helper(root);
            prev.Right = dummy.Right;
            dummy.Right.Left = prev;

            return dummy.Right;
        }

        private void helper(Node root)
        {
            if (root == null) return;

            helper(root.Left);

            prev.Right = root;
            root.Left = prev;
            prev = root;

            helper(root.Right);
        }

        
    }
}
