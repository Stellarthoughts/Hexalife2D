using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPT4Project.Extension
{
    public static class EnumerableExtension
    {
        public static T PickRandom<T>(this IEnumerable<T> source)
        {
            return source.PickRandom(1).Single();
        }

        public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count)
        {
            return source.Shuffle().Take(count);
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.OrderBy(x => Guid.NewGuid());
        }

        public static T? RandomElementByWeight<T>(this IEnumerable<T> sequence, Dictionary<T, double> weightSelector, Random rnd)
        {
			var values = weightSelector.Values.ToList();
            double totalWeight = values.Sum(x => x);
            
            double itemWeightIndex = rnd.NextDouble() * totalWeight;
            double currentWeightIndex = 0;

            foreach (var item in from weightedItem in sequence select new { Value = weightedItem, Weight = weightSelector.GetValueOrDefault(weightedItem) })
            {
                currentWeightIndex += item.Weight;

                if (currentWeightIndex >= itemWeightIndex)
                    return item.Value;
            }
            return default(T);
        }
    }
}
