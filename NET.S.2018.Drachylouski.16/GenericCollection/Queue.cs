using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericCollection
{
    public class Queue<T> : IEnumerable<T>
    {
        #region Private fields

        /// <summary>
        /// The list
        /// </summary>
        private List<T> list;

        /// <summary>
        /// The equality comparer
        /// </summary>
        private IEqualityComparer<T> equalityComparer;

        #endregion

        #region Ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="Queue{T}"/> class.
        /// </summary>
        /// <param name="equalityComparer">The equality comparer.</param>
        /// <exception cref="System.ArgumentNullException">equalityComparer</exception>
        public Queue(IEqualityComparer<T> equalityComparer)
        {
            list = new List<T>();
            this.equalityComparer = equalityComparer ??
                                    throw new ArgumentNullException($"{nameof(equalityComparer)} can't be null");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Queue{T}"/> class.
        /// </summary>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="equalityComparer">The equality comparer.</param>
        /// <exception cref="System.ArgumentNullException">
        /// equalityComparer
        /// or
        /// enumerable
        /// </exception>
        public Queue(IEnumerable<T> enumerable, IEqualityComparer<T> equalityComparer)
        {
            this.equalityComparer = equalityComparer ??
                                    throw new ArgumentNullException($"{nameof(equalityComparer)} can't be null");

            if (enumerable == null)
            {
                throw new ArgumentNullException($"{nameof(enumerable)} can'tbe null");
            }

            list = new List<T>(enumerable);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count => list.Count;

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public int Version { get; private set; }

        /// <summary>
        /// Gets the <see cref="T"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="T"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException">index</exception>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index > list.Count - 1)
                {
                    throw new ArgumentOutOfRangeException($"{nameof(index)} out of range");
                }

                return list[index];
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Determines whether [contains] [the specified item].
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified item]; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(T item) => list.Contains(item, equalityComparer);

        /// <summary>
        /// Dequeues this instance.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        public T Dequeue()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException($"Queue already empty");
            }

            T item = list.First();
            list.Remove(item);
            Version++;
            return item;
        }

        /// <summary>
        /// Peeks this instance.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        public T Peek()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException($"Queue is  empty");
            }

            return list.First();
        }

        /// <summary>
        /// Enqueues the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Enqueue(T item)
        {
            list.Add(item);
            Version++;
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            list.Clear();
            Version++;
        }

        #endregion

        #region GetEnumerator methods

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns></returns>
        public NestedEnumerator<T> GetEnumerator()
        {
            return new NestedEnumerator<T>(this);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
