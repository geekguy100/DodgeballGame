/*****************************************************************************
// File Name :         EventManager.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using System;

public static class EventManager
{
    public static Action OnEnemyHit;
    public static Action<bool> OnGamePause;
    public static Action OnGameWin;

    public static void EnemyHit()
    {
        OnEnemyHit?.Invoke();
    }

    public static void GamePause(bool paused)
    {
        OnGamePause?.Invoke(paused);
    }

    public static void GameWin()
    {
        OnGameWin?.Invoke();
    }
}