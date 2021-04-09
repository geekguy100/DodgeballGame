/*****************************************************************************
// File Name :         DashAbility.cs
// Author :            Kyle Grenier
// Creation Date :     04/08/2021
//
// Brief Description : A dash ability that thrusts the character in the direction they are facing.
*****************************************************************************/
using UnityEngine;
using System.Collections;

public class DashAbility : ISpecialAbility
{
    [Tooltip("The strength of the dash. A higher number means a further dash.")]
    [SerializeField] private float dashStrength;

    [Tooltip("The time in seconds to wait before controls are restored to the character.")]
    [SerializeField] private float dashTime;

    protected override void ExecuteAbility(CharacterMotor characterMotor, object args)
    {
        StartCoroutine(PerformDash(characterMotor, args));
    }

    private IEnumerator PerformDash(CharacterMotor characterMotor, object args)
    {
        Vector3 input = (Vector3)args;

        characterMotor.CanMove = false;
        characterMotor.AddForce(input * dashStrength, ForceMode.VelocityChange);
        yield return new WaitForSeconds(dashTime);
        characterMotor.CanMove = true;
    }
}
