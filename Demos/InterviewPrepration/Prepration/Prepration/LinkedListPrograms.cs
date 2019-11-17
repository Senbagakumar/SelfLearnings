using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepration
{
    class LinkedList
    {
        //https://leetcode.com/problems/design-linked-list/discuss/378688/Design-Linked-List-Java
        private Node head;
        private int size;

        public void AddNode(int value) // At Tail
        {
            var currentNode = new Node(value);

            if (head == null)
                head = currentNode;
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

        /*5. Reverse a singly linked list.*/
        static Node ReverseLinkedList(Node node)
        {
            if (node == null || node.Next == null) return node;
            var next = node.Next;
            var reverseNode = ReverseLinkedList(next);
            next.Next = node;
            node.Next = null;
            return reverseNode;
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
        static Node RecursivePalindrome(Node slow, Node fast,Result result)
        {
            if (fast == null) return slow;
            if (fast.Next == null) return slow.Next;
            Node recur = RecursivePalindrome(slow.Next, fast.Next.Next, result);
            if (recur.Value != slow.Value)
                result.isPalindrome = false;
            return recur.Next;
        }
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

        //10. Sum of Two linked list
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
                    while (different-- > 0)
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
    }
}
