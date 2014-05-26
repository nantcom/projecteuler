using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NantCom.Collections
{
    public class PriorityQueue<T> : IEnumerable<T>
    {
        private LinkedList<Tuple<T, double>> _list = new LinkedList<Tuple<T, double>>();
        Func<double, double, bool> _comparer;

        public PriorityQueue(Func<double, double, bool> comparer)
        {
            _comparer = comparer;
        }

        /// <summary>
        /// Adds item to queue, sorted by given priority function
        /// </summary>
        /// <param name="item"></param>
        public void Enqueue(T item, double priority)
        {
            if (_list.Count == 0)
            {
                _list.AddLast(new Tuple<T, double>(item, priority));
                return;
            }

            if (_list.Find(item, t => t.Item1) != null)
            {
                throw new InvalidOperationException("Item already exists");
            }

            // comparer will determine whether item should go before
            var target = _list.First;
            while (target != null)
            {
                // same priority
                // see if there is another with same priority
                while (priority == target.Value.Item2)
                {
                    if (target.Next == null)
                    {
                        goto exit;
                    }
                    target = target.Next;
                }

                if (_comparer(priority, target.Value.Item2))
                {
                    break;
                }

                target = target.Next;
            }
        exit:

            if (target != null)
            {
                if (target.Value.Item2 == priority) // same priority, go after
                {
                    _list.AddAfter(target, new Tuple<T, double>(item, priority));
                }
                else
                {
                    _list.AddBefore(target, new Tuple<T, double>(item, priority));
                }
            }
            else
            {
                _list.AddFirst(new Tuple<T, double>(item, priority));
            }
        }

        /// <summary>
        /// Adds item to queue, sorted by given priority function (allow duplicates)
        /// </summary>
        /// <param name="item"></param>
        public void EnqueueAllowDuplicate(T item, double priority)
        {
            if (_list.Count == 0)
            {
                _list.AddLast(new Tuple<T, double>(item, priority));
                return;
            }

            if (_list.Find(item, t => t.Item1) != null)
            {
                this.ChangePriority(item, priority);
                return;
            }

            // comparer will determine whether item should go before
            var target = _list.First;
            while (target != null)
            {
                if (_comparer(priority, target.Value.Item2))
                {
                    break;
                }

                target = target.Next;
            }

            if (target != null)
            {
                _list.AddBefore(target, new Tuple<T, double>(item, priority));
            }
            else
            {
                _list.AddFirst(new Tuple<T, double>(item, priority));
            }
        }

        /// <summary>
        /// Gets item with highest prioriy
        /// </summary>
        /// <returns></returns>
        public T Dequeue()
        {
            if (_list.Count > 0)
            {
                var v = _list.First.Value;
                _list.RemoveFirst();

                return v.Item1;
            }

            throw new InvalidOperationException("Queue is Empty");
        }

        /// <summary>
        /// Change priority of item
        /// </summary>
        /// <param name="item"></param>
        public void ChangePriority(T item, double newPriority)
        {
            var nodeWithItem = _list.Find(item, t => t.Item1);

            if (nodeWithItem == null)
            {
                this.Enqueue(item, newPriority);
                return;
                //throw new InvalidOperationException("item is not in this list");
            }

            _list.Remove(nodeWithItem);
            this.Enqueue(item, newPriority);

        }

        /// <summary>
        /// Number of items
        /// </summary>
        public int Count
        {
            get
            {
                return _list.Count;
            }
        }

        #region Make it compatible with foreach/linq

        public IEnumerator<T> GetEnumerator()
        {
            return (from node in _list
                    select node.Item1).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (from node in _list
                    select node.Item1).GetEnumerator();
        }

        #endregion
    }
}
