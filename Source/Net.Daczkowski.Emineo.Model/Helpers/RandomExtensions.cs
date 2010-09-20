namespace Net.Daczkowski.Emineo.Model.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Text;

    public static class RandomExtensions
    {
        public static bool NextBool(this Random random)
        {
            return random.Next(2) % 2 == 0;
        }

        public static DateTime NextDate(this Random random)
        {
            int year = random.Next(2010, 2011);
            int month = random.Next(1, 12);
            int day = random.Next(1, 28);
            return new DateTime(year, month, day);
        }

        public static decimal NextDecimal(this Random random, decimal minValue, decimal maxValue)
        {
            //// TODO [merdacz] support decimals correctly
            return random.Next((int)minValue, (int)maxValue);
        }

        public static decimal? NextNullableDecimal(this Random random, int minValue, int maxValue)
        {
            if (random.NextBool())
            {
                return null;
            }

            return random.NextDecimal(minValue, maxValue);
        }

        public static string NextString(this Random random, int minSize, int maxSize)
        {
            Contract.Assert(minSize >= 0);
            Contract.Assert(minSize <= maxSize);

            var builder = new StringBuilder();
            var size = random.Next(minSize, maxSize);
            char ch;
            var wordSize = random.Next(1, 30);
            for (int i = 0, j = 0; i < size; i++, j++)
            {
                if (j == wordSize)
                {
                    ch = ' ';
                    wordSize = random.Next(1, 30);
                    j = 0;
                }
                else
                {
                    ch = Convert.ToChar(Convert.ToInt32(Math.Floor((26 * random.NextDouble()) + 65)));
                }

                builder.Append(ch);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Randomly calls (or not) the provided action.
        /// </summary>
        /// <param name="random">The random.</param>
        /// <param name="action">The action.</param>
        public static void NextCall(this Random random, Action action)
        {
            if (random.NextBool())
            {
                action();
            }
        }

        /// <summary>
        /// Randomly calls (or not) the provided function and returns its result.
        /// </summary>
        /// <typeparam name="T">Function result type. </typeparam>
        /// <param name="random">The random.</param>
        /// <param name="func">The function to be called.</param>
        /// <returns>Result of provided function if called; default value of this type otherwise. </returns>
        public static T NextCall<T>(this Random random, Func<T> func)
        {
            if (random.NextBool())
            {
                return func();
            }

            return default(T);
        }

        /// <summary>
        /// Randomly picks up single item from the provided collection.
        /// </summary>
        /// <typeparam name="T">Type of list item.</typeparam>
        /// <param name="random">The random.</param>
        /// <param name="list">The list to pick from.</param>
        /// <returns>Randomly picked element.</returns>
        public static T PickRandomListItem<T>(this Random random, IList<T> list)
        {
            return list[random.Next(list.Count)];
        }

        /// <summary>
        /// Randomly picks up to specified amount of items from the list. 
        /// </summary>
        /// <typeparam name="T">Type of list item.</typeparam>
        /// <param name="random">The random.</param>
        /// <param name="items">The items.</param>
        /// <param name="maxCount">The max count.</param>
        /// <returns>Elements picked. </returns>
        public static IEnumerable<T> PickSomeInRandomOrder<T>(this Random random, IEnumerable<T> items, int maxCount)
        {
            IList<T> result = new List<T>();
            IList<T> original = new List<T>(items);

            while (original.Count != 0 && result.Count < maxCount)
            {
                var item = random.PickRandomListItem<T>(original);
                original.Remove(item);
                result.Add(item);
            }

            return result;
        }

        /// <summary>
        /// Randomly picks up to specified amount of items from the list. 
        /// </summary>
        /// <typeparam name="T">Type of list item.</typeparam>
        /// <param name="random">The random.</param>
        /// <param name="items">The items.</param>
        /// <param name="maxCount">The max count.</param>
        /// <param name="minCount">The min count</param>
        /// <returns>Elements picked. </returns>
        public static IEnumerable<T> PickSomeInRandomOrder<T>(this Random random, IEnumerable<T> items, int maxCount, int minCount)
        {
            IList<T> result = new List<T>();
            IList<T> original = new List<T>(items);

            while (original.Count != minCount && result.Count < maxCount)
            {
                var item = random.PickRandomListItem<T>(original);
                original.Remove(item);
                result.Add(item);
            }

            return result;
        }
    }
}