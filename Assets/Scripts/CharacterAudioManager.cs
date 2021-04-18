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
    [SerializeField] private AudioClip jumpPadSFX;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayJumpPadSFX()
    {
        audioSource.PlayOneShot(jumpPadSFX);
    }
}
