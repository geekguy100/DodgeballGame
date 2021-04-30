/*****************************************************************************
// File Name :         Settings.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : GameObject that holds our settings.
*****************************************************************************/
using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private GameSettings settings;

    private static Settings instance;
    public static GameSettings s;

    private void Awake()
    {
        // We don't have a settings object initialized yet, so let's use this one!
        if (instance == null)
        {
            instance = this;
            s = settings;
        }

        // We already have an instance of settings, so destroy the duplicate.
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
