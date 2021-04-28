/*****************************************************************************
// File Name :         ICannonState.cs
// Author :            Kyle Grenier
// Creation Date :     04/28/2021
//
// Brief Description : Defines a contract for implementing a new state into the Cannon's state machine.
*****************************************************************************/
using UnityEngine;

public abstract class ICannonState : MonoBehaviour
{
    protected Cannon cannon;

    protected virtual void Awake()
    {
        cannon = GetComponent<Cannon>();
    }

    public abstract void SurveyArea();
    public abstract void FireAtTarget(GameObject target);
    public abstract void TargetLost(GameObject target);
    public abstract void TargetAquired(GameObject target);
}