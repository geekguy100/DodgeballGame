/*****************************************************************************
// File Name :         Timer.cs
// Author :            Kyle Grenier
// Creation Date :     04/30/2021
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
    public Action<float> OnTimerChange;

    private float time;
    private bool runTimer = true;

    private void OnEnable()
    {
        EventManager.OnGameWin += StopTimer;
    }

    private void OnDisable()
    {
        EventManager.OnGameWin -= StopTimer;
    }

    private void Update()
    {
        if (runTimer)
            UpdateTimer();
    }

    private void StopTimer()
    {
        runTimer = false;
    }

    private void UpdateTimer()
    {
        time += Time.deltaTime;
        OnTimerChange?.Invoke(time);
    }
}
