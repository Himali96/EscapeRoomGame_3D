using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnityExtensions
{
    public static void Shuffle<T>(this IList<T> list)
    {
        int listCount = list.Count;
        while (listCount > 1)
        {
            listCount--;
            int k = Random.Range(0, listCount + 1);
            (list[k], list[listCount]) = (list[listCount], list[k]); // Swap
        }
    }

    public static bool IsEqual(this Color c1, Color c2)
    {
        return (Mathf.Abs(c1.r - c2.r) < 0.01f &&
                Mathf.Abs(c1.g - c2.g) < 0.01f &&
                Mathf.Abs(c1.b - c2.b) < 0.01f);
    }
}