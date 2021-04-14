/*****************************************************************************
// File Name :         BallThrowerSettings.cs
// Author :            Kyle Grenier
// Creation Date :     04/14/2021
//
// Brief Description : Settings for a BallInteractor.
*****************************************************************************/
using UnityEngine;

[CreateAssetMenu(fileName = "Ball Thrower Settings", menuName = "Scriptable Objects/Ball Thrower Settings")]
public class BallThrowerSettings : ScriptableObject
{
    public float throwForce;
    public float windUpTime;
}