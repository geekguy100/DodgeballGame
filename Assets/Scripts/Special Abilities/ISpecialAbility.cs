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

    private CharacterAudioManager audioManager;
    [SerializeField] private AudioClip abilitySFX;

    [SerializeField] private AbilityUIElement uiElement;

    // True if the character can use their special ability.
    private bool canUseAbility = true;

    private void Awake()
    {
        audioManager = GetComponent<CharacterAudioManager>();
    }



    /// <summary>
    /// Performs the special ability.
    /// </summary>
    public void Execute(CharacterMotor characterMotor, object args)
    {
        if (canUseAbility)
        {
            ExecuteAbility(characterMotor, args);
            audioManager?.PlayOneShot(abilitySFX);
            uiElement.AbilityUsed();
            StartCoroutine(Cooldown());
        }
    }

    protected abstract void ExecuteAbility(CharacterMotor characterMotor, object args);

    private IEnumerator Cooldown()
    {
        uiElement.AbilityCooldown(cooldownTime);
        canUseAbility = false;
        yield return new WaitForSeconds(cooldownTime);
        canUseAbility = true;
        uiElement.CooldownComplete();
    }
}