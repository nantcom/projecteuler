using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace NantCom
{
    public static class LinqExt
    {
        public static IEnumerable<int> To(this int input, int to, int advanceBy = 1)
        {
            if (to > input)
            {
                for (int i = input; i <= to; i += advanceBy)
                {
                    yield return i;
                }
            }
            else
            {
                for (int i = input; i >= to; i -= advanceBy)
                {
                    yield return i;
                }
            }
        }
        public static IEnumerable<long> To(this long input, long to, long advanceBy = 1)
        {
            if (to > input)
            {
                for (long i = input; i <= to; i += advanceBy)
                {
                    yield return i;
                }
            }
            else
            {
                for (long i = input; i >= to; i -= advanceBy)
                {
                    yield return i;
                }
            }
        }

        public static bool Print(this string input)
        {
            Debug.WriteLine(input);
            return true;
        }

        /// <summary>
        /// Do the specified action on the seqeuence
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IEnumerable<T> Iterate<T>(this IEnumerable<T> input, Action<T> action)
        {
            foreach (var item in input)
            {
                action(item);
                yield return item;
            }
        }

        /// <summary>
        /// Stops Lazy Evaluation
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        public static void Evaluate<T>(this IEnumerable<T> input)
        {
            foreach (var item in input)
            {
            }
        }

        /// <summary>
        /// Find the specified target in list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static T Find<T>(this IEnumerable<T> input, T target)
        {
            var dTarget = (dynamic)target;
            foreach (var item in input)
            {
                if (item == dTarget)
                {
                    return item;
                }
            }

            return default(T);
        }

        /// <summary>
        /// Find the specified target in list using a selector
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static T Find<T, Tv>(this IEnumerable<T> input, Tv target, Func<T, Tv> selector)
        {
            var dTarget = (dynamic)target;
            foreach (var item in input)
            {
                var v = selector(item);
                if (v == dTarget)
                {
                    return item;
                }
            }

            return default(T);
        }

        /// <summary>
        /// Try get value from dictionary
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static T TryGet<K, T>(this Dictionary<K, T> input, K key, T def = default(T))
        {
            T value;
            if (input.TryGetValue(key, out value))
            {
                return value;
            }

            return def;
        }

        public static readonly Func<double, double, bool> TakeMore = (a, b) => a > b;
        public static readonly Func<double, double, bool> TakeLess = (a, b) => a < b;

    }
}
