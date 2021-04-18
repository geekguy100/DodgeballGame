/*****************************************************************************
// File Name :         AbilityUIElement.cs
// Author :            Kyle Grenier
// Creation Date :     04/18/2021
//
// Brief Description : A UI Element that responds to the player using ablities.
*****************************************************************************/
using UnityEngine;
using TMPro;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class AbilityUIElement : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private TextMeshProUGUI popupText;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void AbilityUsed(int usesLeft = -1)
    {
        anim.SetTrigger("abilityUsed");

        if (popupText != null)
        {
            StopAllCoroutines();
            StartCoroutine(WaitThenChangeText(usesLeft));
        }
    }

    public void AbilityCooldown(float cooldownTime)
    {
        anim.SetTrigger("cooldownStart");
    }

    public void CooldownComplete()
    {
        anim.SetTrigger("cooldownComplete");
    }

    private IEnumerator WaitThenChangeText(int usesLeft)
    {
        yield return new WaitForSeconds(2f);
        popupText.text = "x" + usesLeft;
    }
}