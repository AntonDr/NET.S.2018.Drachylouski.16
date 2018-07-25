using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTreeLogic
{
    /// <summary>
    /// Binary search tree class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BinarySearchTree<T>
    {
        #region Private fields
        /// <summary>
        /// The head
        /// </summary>
        private BinaryTreeNode<T> head;

        /// <summary>
        /// The version
        /// </summary>
        private int version;

        /// <summary>
        /// The comparer
        /// </summary>
        private IComparer<T> comparer;
        #endregion

        #region Ctors
        /// <summary>
        /// Initializes a new instance of the <see cref="BinarySearchTree{T}"/> class.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        public BinarySearchTree(IComparer<T> comparer)
        {
            this.comparer = comparer;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinarySearchTree{T}"/> class.
        /// </summary>
        public BinarySearchTree() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinarySearchTree{T}"/> class.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        /// <param name="collection">The collection.</param>
        public BinarySearchTree(IComparer<T> comparer, IEnumerable<T> collection)
        {
            this.comparer = comparer;

            foreach (var item in collection)
            {
                Add(item);
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count { get; private set; }

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public int Version { get; private set; }
        #endregion

        #region Public methods
        /// <summary>
        /// Adds the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Add(T value)
        {
            if (head == null)
            {
                head = new BinaryTreeNode<T>(value, comparer);
            }
            else
            {
                AddTo(head, value);
            }

            Count++;
            Version++;
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            head = null;
            Count = 0;
            Version++;
        }

        /// <summary>
        /// Removes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public bool Remove(T value)
        {
            BinaryTreeNode<T> current, parent;

            current = FindWithParent(value, out parent);

            if (current == null)
            {
                return false;
            }

            if (current.RightTreeNode == null)
            {
                if (parent == null)
                {
                    head = current.LeftTreeNode;
                }
                else
                {
                    int result = parent.Compare(current.Value);
                    if (result > 0)
                    {
                        parent.LeftTreeNode = current.LeftTreeNode;
                    }
                    else if (result < 0)
                    {
                        parent.RightTreeNode = current.LeftTreeNode;
                    }
                }
            }
            else if (current.RightTreeNode.LeftTreeNode == null)
            {
                current.RightTreeNode.LeftTreeNode = current.LeftTreeNode;

                if (parent == null)
                {
                    head = current.RightTreeNode;
                }
                else
                {
                    int result = parent.Compare(current.Value);
                    if (result > 0)
                    {
                        parent.LeftTreeNode = current.RightTreeNode;
                    }
                    else if (result < 0)
                    {
                        parent.RightTreeNode = current.RightTreeNode;
                    }
                }
            }
            else
            {
                BinaryTreeNode<T> leftmost = current.RightTreeNode.LeftTreeNode;
                BinaryTreeNode<T> leftmostParent = current.RightTreeNode;

                while (leftmost.LeftTreeNode != null)
                {
                    leftmostParent = leftmost;
                    leftmost = leftmost.LeftTreeNode;
                }

                leftmostParent.LeftTreeNode = leftmost.RightTreeNode;
                leftmost.LeftTreeNode = current.LeftTreeNode;
                leftmost.RightTreeNode = current.RightTreeNode;

                if (parent == null)
                {
                    head = leftmost;
                }
                else
                {
                    int result = parent.Compare(current.Value);
                    if (result > 0)
                    {
                        parent.LeftTreeNode = leftmost;
                    }
                    else if (result < 0)
                    {
                        parent.RightTreeNode = leftmost;
                    }
                }
            }

            Count--;
            Version++;

            return true;
        }

        /// <summary>
        /// Determines whether [contains] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(T value)
        {
            BinaryTreeNode<T> parent;
            return FindWithParent(value, out parent) != null;
        }
        #endregion

        #region Iterator methods
        /// <summary>
        /// Ins the order enumerable.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> InOrderEnumerable()
        {
            version = Version;
            return InOrderEnumerable(head);
        }

        /// <summary>
        /// Pres the order enumerable.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> PreOrderEnumerable()
        {
            version = Version;
            return PreOrderEnumerable(head);
        }

        /// <summary>
        /// Posts the order enumerable.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> PostOrderEnumerable()
        {
            version = Version;
            return PostOrderEnumerable(head);
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Finds the with parent.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="parent">The parent.</param>
        /// <returns></returns>
        private BinaryTreeNode<T> FindWithParent(T value, out BinaryTreeNode<T> parent)
        {
            BinaryTreeNode<T> current = head;
            parent = null;

            while (current != null)
            {
                int result = current.Compare(value);

                if (result > 0)
                {

                    parent = current;
                    current = current.LeftTreeNode;
                }
                else if (result < 0)
                {

                    parent = current;
                    current = current.RightTreeNode;
                }
                else
                {

                    break;
                }
            }

            return current;
        }

        /// <summary>
        /// Adds to.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="value">The value.</param>
        private void AddTo(BinaryTreeNode<T> node, T value)
        {
            if (node.Compare(value) > 0)
            {

                if (node.LeftTreeNode == null)
                {
                    node.LeftTreeNode = new BinaryTreeNode<T>(value);
                }
                else
                {
                    AddTo(node.LeftTreeNode, value);
                }
            }
            else
            {

                if (node.RightTreeNode == null)
                {
                    node.RightTreeNode = new BinaryTreeNode<T>(value);
                }
                else
                {
                    AddTo(node.RightTreeNode, value);
                }
            }
        }

        /// <summary>
        /// Ins the order enumerable.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        private IEnumerable<T> InOrderEnumerable(BinaryTreeNode<T> node)
        {
            if (version != Version)
            {
                throw new InvalidOperationException($"Collection can't be change when enumerating");
            }

            if (node.LeftTreeNode != null)
            {
                foreach (T item in InOrderEnumerable(node.LeftTreeNode)) yield return item;
            }

            yield return node.Value;

            if (node.RightTreeNode != null)
            {
                foreach (T item in InOrderEnumerable(node.RightTreeNode)) yield return item;
            }
        }

        /// <summary>
        /// Posts the order enumerable.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        private IEnumerable<T> PostOrderEnumerable(BinaryTreeNode<T> node)
        {
            if (version != Version)
            {
                throw new InvalidOperationException($"Collection can't be change when enumerating");
            }

            if (node.LeftTreeNode != null)
            {
                foreach (T item in PostOrderEnumerable(node.LeftTreeNode)) yield return item;
            }

            if (node.RightTreeNode != null)
            {
                foreach (T item in PostOrderEnumerable(node.RightTreeNode)) yield return item;
            }

            yield return node.Value;
        }


        /// <summary>
        /// Pres the order enumerable.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        private IEnumerable<T> PreOrderEnumerable(BinaryTreeNode<T> node)
        {
            if (version != Version)
            {
                throw new InvalidOperationException($"Collection can't be change when enumerating");
            }

            yield return node.Value;

            if (node.LeftTreeNode != null)
            {
                foreach (T item in PreOrderEnumerable(node.LeftTreeNode)) yield return item;
            }

            if (node.RightTreeNode != null)
            {
                foreach (T item in PreOrderEnumerable(node.RightTreeNode)) yield return item;
            }
        } 
        #endregion
    }
}