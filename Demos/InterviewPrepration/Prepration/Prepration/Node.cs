using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepration
{
    public class Node
    {
        public int Value;
        public Node(int value)
        {
            this.Value = value;
        }
        public int Data { get { return Value; } }
        public Node Left = null;  //Binary Tree Left
        public Node Right = null; //Binary Tree Ritht
        public Node Next = null;  //Linked List
    }   
}
