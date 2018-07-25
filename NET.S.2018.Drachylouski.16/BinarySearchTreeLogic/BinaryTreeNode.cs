using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTreeLogic
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class BinaryTreeNode<T>
    {
        /// <summary>
        /// The comparer
        /// </summary>
        private static IComparer<T> comparer;

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryTreeNode{T}"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="comparer">The comparer.</param>
        public BinaryTreeNode(T value,IComparer<T> comparer = null)
        {

            if (comparer==null && BinaryTreeNode<T>.comparer==null)
            {
               
               BinaryTreeNode<T>.comparer = Comparer<T>.Default;
            }
            else if (comparer != null)
            {
                BinaryTreeNode<T>.comparer = comparer;
            }

            Value = value;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public T Value { get; private set; }
        /// <summary>
        /// Gets or sets the left tree node.
        /// </summary>
        /// <value>
        /// The left tree node.
        /// </value>
        public BinaryTreeNode<T> LeftTreeNode { get; set; }
        /// <summary>
        /// Gets or sets the right tree node.
        /// </summary>
        /// <value>
        /// The right tree node.
        /// </value>
        public BinaryTreeNode<T> RightTreeNode { get; set; }

        /// <summary>
        /// Compares the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public int Compare(T other)
        {
            return comparer.Compare(Value, other);
        }
    }
}
