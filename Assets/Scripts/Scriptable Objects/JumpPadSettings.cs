/*****************************************************************************
// File Name :         JumpPadSettings.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : Scriptable object to control jump pad settings.
*****************************************************************************/
using UnityEngine;

[CreateAssetMenu(fileName = "Jump Pad Settings", menuName = "Scriptable Objects/Jump Pad Settings")]
public class JumpPadSettings : ScriptableObject
{
    public float forwardForce;
    public float upwardForce;
}