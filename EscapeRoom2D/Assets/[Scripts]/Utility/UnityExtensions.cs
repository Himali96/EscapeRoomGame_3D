using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnityExtensions
{
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            (list[k], list[n]) = (list[n], list[k]); // Swap
        }
    }

    public static bool IsEqual(this Color c1, Color c2)
    {
        return (Mathf.Abs(c1.r - c2.r) < 0.01f &&
                Mathf.Abs(c1.g - c2.g) < 0.01f &&
                Mathf.Abs(c1.b - c2.b) < 0.01f);
    }
}
