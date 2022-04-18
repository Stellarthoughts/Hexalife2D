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

		public static T PickRandom<T>(this IEnumerable<T> source, Random rnd)
		{
			return source.PickRandom(1, rnd).Single();
		}

		public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count, Random rnd)
		{
			return source.Shuffle(rnd).Take(count);
		}

		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rnd)
		{
			var list = source.ToList();
			int n = list.Count;

			while (n > 1)
			{
				n--;
				int k = rnd.Next(n + 1);
				T value = list[k];
				list[k] = list[n];
				list[n] = value;
			}
			return list;
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
