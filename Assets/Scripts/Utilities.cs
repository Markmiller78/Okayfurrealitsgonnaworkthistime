using UnityEngine;
using System.Collections;

public class Utilities
{
    public static void ArraySwap(GameObject[] arr, int a, int b)
    {
        GameObject temp = arr[a];
        arr[a] = arr[b];
        arr[b] = temp;
    }

    public static void ArrayShuffle(GameObject[] arr, int times = 100)
    {
        for (int i = 0; i < times; i++)
        {

            int b = Random.Range(1, arr.Length);
            ArraySwap(arr, 0, b);
        }
    }
}
