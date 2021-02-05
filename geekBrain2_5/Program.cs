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
            Node<int> node = new Node<int>();
            Insert(node, 10);
            Insert(node, 12);
            Insert(node, 8);
            Insert(node, 9);
            Insert(node, 7);
            Insert(node, 3);
            Insert(node, 4);
            dff(node, 9);
            bff(node, 9);

            Console.ReadLine();
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

        static void deleteValue(Node<int> node, int value)
        {
            Node<int> target = getNodeByValue(node, value);
            removeNodeByPtr(target);
        }

        static void printTree(Node<int> node, string str, int level)
        {
            if (node != null)
            {

                Console.WriteLine($"{level} {str} {node.Data}");
                printTree(node.Left, "left", level + 1);
                printTree(node.Right, "right", level + 1);
            }
        }

        static Node<int> GetFreeNode(int value, Node<int> head)
        {
            Node<int> tmp = new Node<int>();
            tmp.Left = null;
            tmp.Right = null;
            tmp.Data = value;
            tmp.Parent = head;
            return tmp;

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
                        tmp.Right = GetFreeNode(value, tmp);
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
                    throw new Exception("Wrong tree state");                  // ƒерево построено неправильно
                }
            }
            return head;
        }
        public static Node<int> getMinNode(Node<int> node)
        {
            while (node.Left != null)
            {
                node = node.Left;
            }
            return node;
        }

        public static Node<int> getMaxNode(Node<int> node)
        {
            while (node.Right != null)
            {
                node = node.Right;
            }
            return node;
        }

        public static Node<int> getNodeByValue(Node<int> node, int value)
        {
            while (node != null)
            {
                if (node.Data > value)
                {
                    node = node.Left;
                    continue;
                }
                else if (node.Data < value)
                {
                    node = node.Right;
                    continue;
                }
                else return node;
            }
            return null;
        }

        public static void removeNodeByPtr(Node<int> node)
        {
            if (node.Left != null && node.Right != null)
            {
                Node<int> localMax = getMaxNode(node.Left);
                node.Data = localMax.Data;
                removeNodeByPtr(localMax);
                return;
            }
            else if (node.Left != null)
            {
                if (node == node.Parent.Left)
                {
                    node.Parent.Left = node.Left;
                }
                else
                {
                    node.Parent.Right = node.Left;
                }
            }
            else if (node.Right != null)
            {
                if (node == node.Parent.Right)
                {
                    node.Parent.Right = node.Right;
                }
                else
                {
                    node.Parent.Left = node.Right;
                }
            }
            else
            {
                if (node == node.Parent.Left)
                {
                    node.Parent.Left = null;
                }
                else
                {
                    node.Parent.Right = null;
                }
            }
            return;
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
