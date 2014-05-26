using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NantCom
{
    public static class MathExt
    {
        public static bool IsPrime(this int input)
        {
            return MathExt.IsPrime((long)input);
        }

        /// <summary>
        /// Determine whether given number is prime
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsPrime(this long input)
        {
            if (input < 0)
            {
                return false;
            }

            if (input == 2)
            {
                return true;
            }

            if (input % 2 == 0)
            {
                return false; // even numbers are not prime
            }

            var hasFactor = from number in 2L.To((long)Math.Ceiling(Math.Sqrt(input)))
                            where input % number == 0
                            select number;

            return hasFactor.Count() == 0;
        }
        
        /// <summary>
        /// Find all factors of input
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static IEnumerable<long> Factors(this long input)
        {
            yield return 1;
            yield return input;

            // Factor is up to Sqrt(input) round up
            for (int i = 2; i < Math.Ceiling(Math.Sqrt(input)); i++)
            {
                if (input % i == 0) // i is factor
                {
                    yield return i;

                    // find out which number made it divisible,
                    // that is also factor
                    yield return input / i;
                }
            }
        }

        /// <summary>
        /// Find products of input
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int Product(this IEnumerable<int> input)
        {
            var p = 1;
            foreach (var item in input)
            {
                p = p * item;
            }

            return p;
        }

        /// <summary>
        /// Find products of input
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static long Product(this IEnumerable<long> input)
        {
            var p = 1L;
            foreach (var item in input)
            {
                p = p * item;
            }

            return p;
        }

    }
}
