using NantCom.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NantCom.DataStructure.Tree
{

    public class TreeNode<T> : IEnumerable<TreeNode<T>>
        where T : IComparable, IConvertible
    {
        private TreeNode<T> _Left;
        private TreeNode<T> _Right;

        public T Value { get; set; }

        ///<summary>
        ///Get or set the value of Left
        ///</summary>
        public TreeNode<T> Left
        {
            get
            {
                return _Left;
            }
            set
            {
                _Left = value;
                _Left.Parent = this;
            }
        }

        ///<summary>
        ///Get or set the value of Right
        ///</summary>
        public TreeNode<T> Right
        {
            get
            {
                return _Right;
            }
            set
            {
                _Right = value;
                _Right.Parent = this;
            }
        }

        /// <summary>
        /// Parent of this Node
        /// </summary>
        public TreeNode<T> Parent { get; private set; }

        /// <summary>
        /// Discover all nodes from this node
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TreeNode<T>> Discover()
        {
            HashSet<TreeNode<T>> unique = new HashSet<TreeNode<T>>();
            Queue<TreeNode<T>> toVisit = new Queue<TreeNode<T>>();

            // BFS to all nodes from this node
            toVisit.Enqueue(this);
            unique.Add(this);

            while (toVisit.Count() > 0)
            {
                var current = toVisit.Dequeue();
                yield return current;

                foreach (var node in current)
                {
                    // the node may contain more than one vertices leading to it
                    // we only need unique set of nodes
                    if (!unique.Contains(node))
                    {
                        toVisit.Enqueue(node);
                        unique.Add(node);
                    }
                }
            }
        }

        public class FindPathMode
        {
            public double InitialWeight { get; private set; }

            public Func<double, double, bool> Comparer { get; private set; }

            private FindPathMode() { }

            public static readonly FindPathMode Shorter = new FindPathMode()
            {
                InitialWeight = double.MaxValue,
                Comparer = (a, b) => a < b
            };

            public static readonly FindPathMode Longer = new FindPathMode()
            {
                InitialWeight = 0,
                Comparer = (a, b) => a > b
            };
        }

        /// <summary>
        /// Find path using given comparer, the algorithm is Dijkstra
        /// if using "TakeMore", set initial
        /// </summary>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public Dictionary<TreeNode<T>, TreeNode<T>> FindPath(FindPathMode mode)
        {
            var initialWeight = mode.InitialWeight;
            var comparer = mode.Comparer;

            var dist = new Dictionary<TreeNode<T>, double>();
            var previous = new Dictionary<TreeNode<T>, TreeNode<T>>();
            var pq = new PriorityQueue<TreeNode<T>>(LinqExt.TakeMore);

            foreach (var n in this.Discover())
            {
                dist[n] = initialWeight;
                previous[n] = null;
                pq.Enqueue(n, initialWeight);
            }
            dist[this] = 0;

            while (pq.Count > 0)
            {
                var u = pq.Dequeue();

                foreach (var v in u)
                {
                    var alt = (double)(dist[u] + (dynamic)v.Value); // make the adding works
                    if (comparer(alt, dist[v]))
                    {
                        dist[v] = alt;
                        previous[v] = u;
                        pq.ChangePriority(v, alt);
                    }
                }
            }

            return previous;
        }

        /// <summary>
        /// List Nodes in the given Previous List
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public IEnumerable<TreeNode<T>> FollowPath(Dictionary<TreeNode<T>, TreeNode<T>> input)
        {
            if (!input.ContainsKey(this))
            {
                yield break;
            }

            var current = this;
            yield return this;

            while (current != null)
            {
                var prev = input.TryGet(current);
                if (prev != null)
                {
                    yield return prev;

                    current = prev;
                    continue;
                }

                break;
            }
        }

        #region Allow foreach and linq

        private IEnumerable<TreeNode<T>> Vertices()
        {
            if (this.Left != null)
            {
                yield return this.Left;
            }

            if (this.Right != null)
            {
                yield return this.Right;
            }
        }

        public IEnumerator<TreeNode<T>> GetEnumerator()
        {
            return this.Vertices().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.Vertices().GetEnumerator();
        }

        #endregion


        public override string ToString()
        {
            return string.Format("Node: {0}", this.Value);
        }
    }
}
