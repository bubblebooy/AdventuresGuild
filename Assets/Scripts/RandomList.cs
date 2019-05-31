using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomList
{
    public static int[] FixedSum(int n, int sum)
    {
        int[] rand = new int[n];
        for (int i = 0; i < n - 1; i++)
        {
            rand[i] = Random.Range(0, sum);
        }
        rand[n - 1] = 0;
        System.Array.Sort(rand);
        for (int i = n - 1; i >= 0; i--)
        {
            rand[i] = sum - rand[i];
            sum = sum - rand[i];
        }
        return rand;
    }
}
