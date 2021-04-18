/*****************************************************************************
// File Name :         SFXManager.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : Plays SFX that should be heard by the player, regardless of their in-game position.
*****************************************************************************/
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXManager : MonoBehaviour
{
    private AudioSource audioSource;
    [Header("Sounds Effects")]
    [SerializeField] private AudioClip boinkSFX;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        EventManager.OnEnemyHit += PlayBoinkSFX;
    }

    private void OnDisable()
    {
        EventManager.OnEnemyHit -= PlayBoinkSFX;
    }

    private void PlayBoinkSFX()
    {
        audioSource.PlayOneShot(boinkSFX);
    }
}