using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geekBrain2_5
{
    class Program
    {
        static void Main(string[] args)
        {
            Node<int> node = Insert(new Node<int>(), 100);
            dff(node, 10);
            bff(node, 10);
            Console.ReadLine();
        }

        static Node<int> GetFreeNode(int value, Node<int> head)
        {
            //Node<int> newNode = null;
            //if (value == 0)
            //    return null;
            //else
            //{
            //    var nl = value / 2;
            //    var nr = value - nl - 1;
            //    newNode = new Node<int>();
            //    newNode.Data = new Random().Next();
            //    newNode.Left = GetFreeNode(nl, newNode);
            //    newNode.Right = GetFreeNode(nr, newNode);
            //}
            //return newNode;
            Node<int> node = new Node<int>();
            node.Data = value-10;
            node.Parent = head;
            return node;
        }

        public static bool dff(Node<int> node, int value)
        {
            Node<int>[] massNobe = new Node<int>[2];
            massNobe[0] = node.Left;
            massNobe[1] = node.Right;
            if (node.Data == value)
            {
                Console.WriteLine($"Находимся в узле {node.Data}, узел СОВПАЛ с искомым значением {value}");
                return true;    
            }
            else if (node.visited) return false;
            Console.WriteLine($"Находимся в узле {node.Data}, узел НЕ совпал с искомым значением {value}");
            node.visited = true;
            foreach (Node<int> nod in massNobe)
            {
                if (nod == null) continue;
                else if (!nod.visited)
                {
                    bool reached = dff(nod, value);
                    if (reached) return true;
                }
            }
            return false;
        }

        public static bool bff(Node<int> node, int value)
        {
            Node<int>[] massNobe = new Node<int>[2];
            massNobe[0] = node.Left;
            massNobe[1] = node.Right;
            var stack = new Stack<Node<int>>();
            stack.Push(node);
            node.visited = true;
            while (stack.Count > 0)
            {
                Node<int> v = stack.Pop();
                foreach (Node<int> nod in massNobe)
                {
                    if (nod == null) continue;
                    if (!nod.visited)
                    {
                        stack.Push(nod);
                        nod.visited = true;
                        if (nod.Data == value) 
                        {
                            Console.WriteLine($"Находимся в узле {nod.Data}, узел СОВПАЛ с искомым значением {value}");
                            return true; 
                        }
                    }
                    Console.WriteLine($"Находимся в узле {nod.Data}, узел НЕ совпал с искомым значением {value}");
                }
            }
            return false;
        }

        public static Node<int> Insert(Node<int> head, int value)
        {
            Node<int> tmp = null;
            if (head == null)
            {
                head = GetFreeNode(value, null);
                return head;
            }
            tmp = head;
            while (tmp != null)
            {
                if (value > tmp.Data)
                {
                    if (tmp.Right != null)
                    {
                        tmp = tmp.Right;
                        continue;
                    }
                    else
                    {
                        tmp.Right = GetFreeNode(value, tmp); ;
                        return head;
                    }
                }
                else if (value < tmp.Data)
                {
                    if (tmp.Left != null)
                    {
                        tmp = tmp.Left;
                        continue;
                    }
                    else
                    {
                        tmp.Left = GetFreeNode(value, tmp);
                        return head;
                    }
                }
                else
                {
                    throw new Exception("Wrong tree state");                  // Дерево построено неправильно
                }
            }
            return tmp;
        }
    }

    public class Node<T>
    {
        public T Data { get; set; }
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }
        public Node<T> Parent { get; set; }
        public bool visited { get; set; }
        public List<T> listForPrint = new List<T>();
    }
}
