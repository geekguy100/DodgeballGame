/*****************************************************************************
// File Name :         CharacterAudioManager.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CharacterAudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip landSFX;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayLandSFX()
    {
        audioSource.PlayOneShot(landSFX);
    }
}