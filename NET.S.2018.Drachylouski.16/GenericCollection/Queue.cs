using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GenericCollection
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="System.Collections.Generic.IEnumerable{T}" />
    public class Queue<T> : IEnumerable<T>
    {
        #region Private fields

        /// <summary>
        /// The default capacity
        /// </summary>
        private readonly int DefaultCapacity = 8;
        /// <summary>
        /// The grow factor
        /// </summary>
        private readonly int GrowFactor = 2;

        private T[] array;
        private int size = 0;
        private int head = 0;
        private int tail = 0;

        #endregion

        #region Ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="Queue{T}"/> class.
        /// </summary>
        public Queue()
        {
            array=new T[DefaultCapacity];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Queue{T}"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <exception cref="System.ArgumentException">capacity</exception>
        public Queue(int capacity)
        {
            if (capacity<0)
            {
                throw new ArgumentException($"{nameof(capacity)} can't be negative");
            }

            this.Capacity = capacity;
            array = new T[Capacity];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Queue{T}"/> class.
        /// </summary>
        /// <param name="enumerable">The enumerable.</param>
        /// <exception cref="System.ArgumentNullException">enumerable</exception>
        public Queue(IEnumerable<T> enumerable)
        {
            if (enumerable == null)
            {
                throw new ArgumentNullException($"{nameof(enumerable)} can'tbe null");
            }

            Capacity = enumerable.Count();

            size = Capacity;

            array =enumerable.ToArray();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the capacity.
        /// </summary>
        /// <value>
        /// The capacity.
        /// </value>
        public int Capacity { get; private set; }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count => size;

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
        /// Determines whether [contains] [the specified item].
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="equalityComparer">The equality comparer.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified item]; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(T item, IEqualityComparer<T> equalityComparer = null)
        {
            if (equalityComparer == null)
            {
                equalityComparer = EqualityComparer<T>.Default;
            }

            return array.Contains(item, equalityComparer);
        }

        /// <summary>
        /// Dequeues this instance.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        public T Dequeue()
        {
            if (size == 0)
            {
                throw new InvalidOperationException($"Queue already empty");
            }

            T removed = array[head];
            array[head] = default(T);
            head = (head + 1) % array.Length;
            size--;

            Version++;

            return removed;
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

            return array[head];
        }

        /// <summary>
        /// Enqueues the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Enqueue(T item)
        {
            if (size == array.Length)
            {
                int newcapacity = array.Length * GrowFactor;
                T[] newarray = new T[newcapacity];

                if (head < tail)
                {
                    Array.Copy(array, head, newarray, 0, size);
                }
                else
                {
                    Array.Copy(array, head, newarray, 0, array.Length - head);
                    Array.Copy(array, 0, newarray, array.Length - head, tail);
                }

                array = newarray;
                head = 0;
                tail = (size == newcapacity) ? 0 : size;
            }

            array[tail] = item;
            tail = (tail + 1) % array.Length;
            size++;
            Version++;
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            if (head < tail)
                Array.Clear(array, head, size);
            else
            {
                Array.Clear(array,head,array.Length - head);
                Array.Clear(array, 0, tail);
            }

            head = 0;
            tail = 0;
            size = 0;
            Version++;
        }

        /// <summary>
        /// To the array.
        /// </summary>
        /// <returns></returns>
        public T[] ToArray()
        {
            T[] tempArray = array.ToArray();
            return tempArray;
        }

        /// <summary>
        /// Gets the element.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">index</exception>
        internal T GetElement(int index)
        {
            if (index<0)
            {
                throw new ArgumentException($"{nameof(index)} can't be less 0");
            }
            return array[(head + index) % array.Length];
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
