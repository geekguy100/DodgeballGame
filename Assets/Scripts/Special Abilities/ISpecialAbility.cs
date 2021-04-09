/*****************************************************************************
// File Name :         SpecialAbility.cs
// Author :            Kyle Grenier
// Creation Date :     04/08/2021
//
// Brief Description : A contract that each special ability must incorporate.
*****************************************************************************/
using UnityEngine;
using System.Collections;

public abstract class ISpecialAbility : MonoBehaviour
{
    [Tooltip("The time to wait in seconds before able to use ability again.")]
    [SerializeField] protected float cooldownTime;

    // True if the character can use their special ability.
    private bool canUseAbility = true;

    /// <summary>
    /// Performs the special ability.
    /// </summary>
    public void Execute(CharacterMotor characterMotor, object args)
    {
        if (canUseAbility)
        {
            ExecuteAbility(characterMotor, args);
            StartCoroutine(Cooldown());
        }
    }

    protected abstract void ExecuteAbility(CharacterMotor characterMotor, object args);

    private IEnumerator Cooldown()
    {
        canUseAbility = false;
        yield return new WaitForSeconds(cooldownTime);
        canUseAbility = true;
    }
}