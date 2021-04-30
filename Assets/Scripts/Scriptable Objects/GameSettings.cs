/*****************************************************************************
// File Name :         GameSettings.cs
// Author :            Kyle Grenier
// Creation Date :     04/30/2021
//
// Brief Description : Keeps track of the game's settings.
*****************************************************************************/
using UnityEngine;

[CreateAssetMenu(fileName = "Game Settings", menuName = "Scriptable Objects/Settings/Game Settings")]
public class GameSettings : ScriptableObject
{
    [SerializeField] private bool _invertCamX;
    [SerializeField] private bool _invertCamY;

    public bool invertCamX { get { return _invertCamX; } set { _invertCamX = value; } }
    public bool invertCamY { get { return _invertCamY; } set { _invertCamY = value; } }

    [Range(0, 1)]
    [SerializeField] private float _masterVolume = 1.0f;
    public float masterVolume
    {
        get { return _masterVolume; }
        set { SetMasterVolume(value); }
    }

    private void Awake()
    {
        Debug.Log("SETTING AWAKE CALLED");
        SetMasterVolume(_masterVolume);
    }

    public void SetMasterVolume(float volume)
    {
        // Clamp the value.
        _masterVolume = Mathf.Clamp01(volume);
        AudioListener.volume = _masterVolume;
    }
}
