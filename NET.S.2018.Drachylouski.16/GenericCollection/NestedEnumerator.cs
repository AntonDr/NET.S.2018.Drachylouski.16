using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericCollection
{
    public struct NestedEnumerator<T> : IEnumerator<T>
    {
        #region Fields

        /// <summary>
        /// The version
        /// </summary>
        private readonly int version;

        /// <summary>
        /// The current
        /// </summary>
        private T current;

        /// <summary>
        /// The index
        /// </summary>
        private int index;

        /// <summary>
        /// The queue
        /// </summary>
        private Queue<T> queue;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="NestedEnumerator{T}"/> struct.
        /// </summary>
        /// <param name="queue">The queue.</param>
        public NestedEnumerator(Queue<T> queue):this()
        {
            version = queue.Version;
            index = -1;
            this.queue = queue;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the current.
        /// </summary>
        /// <value>
        /// The current.
        /// </value>
        public T Current => current;

        /// <summary>
        /// Gets the current.
        /// </summary>
        /// <value>
        /// The current.
        /// </value>
        object IEnumerator.Current => Current;

        #endregion

        #region Public methods

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose(){}

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>
        /// true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">Collection can't be change when enumerating</exception>
        public bool MoveNext()
        {
            if (++index >= queue.Count)
            {
                return false;
            }

            if (version != queue.Version)
            {
                throw new InvalidOperationException("Collection can't be change when enumerating");
            }

            current = queue[index];

            return true;
        }

        /// <summary>
        /// Sets the enumerator to its initial position, which is before the first element in the collection.
        /// </summary>
        public void Reset()
        {
            current = default(T);
            index = -1;
        }

        #endregion
    }
}