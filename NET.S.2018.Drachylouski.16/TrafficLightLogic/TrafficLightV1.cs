using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficLightLogic
{
    public class TrafficLightV1<T>
    {
        private Queue<T> queue;

        public TrafficLightV1(IEnumerable<T> collection)
        {
            queue=new Queue<T>(collection);
        }

        public T Current { get; private set ; }

        public void Update()
        {
            Current = queue.Dequeue();
                
            queue.Enqueue(Current);
        }

        public IEnumerable<T> Iteration()
        {
            int k = queue.Count;

            IEnumerable<T> InsideIteration()
            {
                while (k != 0)
                {
                    Update();
                    yield return Current;
                    k--;
                }
                Update();
                yield return Current;
            }

            return InsideIteration();
        }

    }
}
