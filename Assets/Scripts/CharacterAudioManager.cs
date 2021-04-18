/*****************************************************************************
// File Name :         CharacterAudioManager.cs
// Author :            Kyle Grenier
// Creation Date :     04/18/2021
//
// Brief Description : Manages playing SFX related to a character and a character's actions.
*****************************************************************************/
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CharacterAudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip landSFX;
    [SerializeField] private AudioClip throwSFX;

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
}