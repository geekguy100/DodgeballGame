/*****************************************************************************
// File Name :         Cannon.cs
// Author :            Kyle Grenier
// Creation Date :     04/28/2021
//
// Brief Description : A state machine for the Cannon enemy.
*****************************************************************************/
using UnityEngine;

[RequireComponent(typeof(CannonIdle))]
[RequireComponent(typeof(CannonFire))]
public class Cannon : MonoBehaviour
{
    [Header("The Cannon's Body")]
    [SerializeField] private Transform cannonBody;

    [Header("Origin of Detection")]
    [SerializeField] private Transform _origin;
    public Transform origin { get { return _origin; } }

    public Transform CannonBody { get { return cannonBody; } }

    #region --- States and State Methods ---
    public ICannonState currentState { get; set; }

    private ICannonState idleState;
    public ICannonState IdleState { get { return idleState; } }

    private ICannonState fireState;
    public ICannonState FireState { get { return fireState; } }


    public void SurveyArea()
    {
        currentState.SurveyArea();
    }

    public void FireAtTarget(GameObject target)
    {
        currentState.FireAtTarget(target);
    }

    public void TargetLost(GameObject target)
    {
        currentState.TargetLost(target);
    }

    public void TargetAcquired(GameObject target)
    {
        currentState.TargetAquired(target);
    }

    #endregion

    private void Start()
    {
        idleState = GetComponent<CannonIdle>();
        fireState = GetComponent<CannonFire>();

        currentState = idleState;
        SurveyArea();
    }
}