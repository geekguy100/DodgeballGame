/*****************************************************************************
// File Name :         GameStats.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;

public static class GameStats
{
    private static int enemiesHit;
    public static int EnemiesHit { get { return enemiesHit; } }

    private static int totalEnemies;
    public static int TotalEnemies { get { return totalEnemies; } }

    public static bool paused { get; set; }

    public static void Init()
    {
        Debug.Log("Game Stats Initialized...");
        enemiesHit = 0;
        totalEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    public static void EnemyHit()
    {
        enemiesHit++;
        if (enemiesHit >= totalEnemies)
            EventManager.GameWin();
    }
}
