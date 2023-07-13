using System;
using System.Collections.Generic;
using System.Text;

public static class CollectionExtensions
{
    private const string Coma = ", ";


    public static bool IsNullOrEmpty<T, TV>(this IDictionary<T, TV> dictionary) => dictionary == null || dictionary.Count == 0;
    public static bool DoesntContainKey<T, TV>(this IDictionary<T, TV> dictionary, T key) => !dictionary.ContainsKey(key);

    public static bool IsNullOrEmpty<T>(this ICollection<T> collection)
    {
        return collection == null || collection.Count == 0;
    }

    public static string ToStringDebug<T>(this ICollection<T> list)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("Count: ");
        sb.Append(list.Count);
        sb.AppendLine();

        foreach (var item in list)
        {
            sb.Append(item);
            sb.Append(", ");
        }

        return sb.ToString();
    }

    public static bool TryFind<T>(this ICollection<T> collection, Func<T, bool> condition, out T foundItem)
    {
        foreach (var item in collection)
        {
            if (condition(item))
            {
                foundItem = item;

                return true;
            }
        }

        foundItem = default;

        return false;
    }


    public static bool TryGetValue<T, TV>(this IDictionary<T, TV> dictionary, T key, out TV val)
    {
        if (!dictionary.ContainsKey(key))
        {
            val = default;

            return false;
        }

        val = dictionary[key];

        return true;
    }

    public static string ToStringItems<T>(this ICollection<T> list)
    {
        return string.Join(Coma, list);
    }
}