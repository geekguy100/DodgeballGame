/*****************************************************************************
// File Name :         ArrayHelper.cs
// Author :            Kyle Grenier
// Creation Date :     03/28/2021
//
// Brief Description : A class that contains some useful methods for manipulating arrays.
*****************************************************************************/
using UnityEngine;

public static class ArrayHelper
{
    /// <summary>
    /// Returns a random element from an array.
    /// </summary>
    /// <typeparam name="T">The object type.</typeparam>
    /// <param name="array">The array to get a random element from.</param>
    /// <returns>A random element from the array.</returns>
    public static T GetRandomElement<T>(T[] array)
    {
        if (array.Length == 0)
            return default;

        int range = array.Length;
        int index = Random.Range(0, range);

        return array[index];
    }
}