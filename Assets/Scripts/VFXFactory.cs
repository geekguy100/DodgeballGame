/*****************************************************************************
// File Name :         VFXFactory.cs
// Author :            Kyle Grenier
// Creation Date :     04/30/2021
//
// Brief Description : A simple factory used to retrieve VFX.
*****************************************************************************/
using UnityEngine;

public static class VFXFactory
{
    private static Object[] vfx;

    [RuntimeInitializeOnLoadMethod]
    private static void RetrieveVFX()
    {
        vfx = Resources.LoadAll("VFX");

        if (vfx == null)
            Debug.LogWarning("Could not load VFX! NULL");
        else if (vfx.Length == 0)
            Debug.LogWarning("0 length");
        else
        {
            foreach (Object o in vfx)
                Debug.Log(o.name);
        }
    }

    public static GameObject GetRandomVFXPrefab()
    {
        return (GameObject)vfx[Random.Range(0, vfx.Length)];
    }
}
