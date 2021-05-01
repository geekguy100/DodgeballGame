/*****************************************************************************
// File Name :         CharacterAudioManager.cs
// Author :            Kyle Grenier
// Creation Date :     04/18/2021
//
// Brief Description : Manages playing SFX related to a character and a character's actions.
*****************************************************************************/
using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CharacterAudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip landSFX;
    [SerializeField] private AudioClip throwSFX;
    [SerializeField] private AudioClip jumpSFX;
    [SerializeField] private AudioClip targetedSFX;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayLandSFX()
    {
        audioSource.PlayOneShot(landSFX);
    }

    public void PlayThrowSFX()
    {
        audioSource.PlayOneShot(throwSFX);
    }

    public void PlayOneShot(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    internal void PlayJumpSFX()
    {
        audioSource.PlayOneShot(jumpSFX, 0.25f);
    }

    internal void PlayTargetedSFX()
    {
        audioSource.PlayOneShot(targetedSFX);
    }
}