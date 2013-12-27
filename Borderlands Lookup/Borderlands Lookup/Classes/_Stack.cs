using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borderlands_Lookup.Classes
{
    class _Stack : System.Windows.Data.Binding
    {

        // My simple version of a stack based on the one taught in CSC 212 Data Structures

        public class Node
        {
            public object data { get; set; }
            public Node next { get; set; }
            public Node(object content, Node nextNode)
            {
                if (content != null)
                    this.data = content;
                else
                    throw new ArgumentNullException("A node must have content");
                this.next = nextNode;
            }
        }

        private Node head;
        private Node tail;
        public int Count { get; set; }

        public _Stack()
        {
            head = null;
            tail = null;
        }

        private bool CheckStack(Node check)
        {
            if (head != null) // list is not empty
            {
                Node start = head;
                while (start != null)
                {
                    if (start.data == check.data)
                        return true; // found
                    else
                        start = start.next;
                }
            }
            return false; // stack is empty, head = null
        }

        private void Recount()
        {
            if (head != null)
            {
                this.Count = 0;
                Node temp = head;
                while (temp != null)
                {
                    this.Count++;
                    temp = temp.next;
                }
            }
        }

        public void AddItem(object item)
        {
            if (head == null && item != null)
            {
                head = new Node(item, null);
                tail = new Node(item, null);
                this.Count++;
                return;
            }
            if (head != null && item != null)
            {
                if (!CheckStack(new Node(item, null)))
                {
                    Node temp = head;
                    head = new Node(item, null);
                    head.next = temp;
                    this.Count++;
                }
                if (this.Count > 10) // remove tail node if list is bigger than 10 items
                {
                    Node curr = head;
                    while (curr.next.next != null)
                    {
                        curr = curr.next;
                    }
                    tail = curr;
                    tail.next = null;
                    Recount();
                }
            }
        }

        public void ClearStack()
        {
            if (head != null)
            {
                while (head != tail)
                {
                    head = head.next;
                }
                head = tail = null;
                Recount();
            }
        }

        public new string ToString()
        {
            if (head != null)
            {
                string output = "";
                Node curr = head;
                while (curr != null)
                {
                    output += "\n" + (string)curr.data;
                    curr = curr.next;
                }
                return output;
            }
            return "Stack is empty";
        }
    }
}
