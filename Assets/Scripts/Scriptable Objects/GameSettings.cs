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
    public bool invertCamX { get { return _invertCamX; } }
    public bool invertCamY { get { return _invertCamY; } }

    public void InvertCamX(bool invert)
    {
        _invertCamX = invert;
    }
    public void InvertCamY(bool invert)
    {
        _invertCamY = invert;
    }

    public int invertCamXModifier
    {
        get
        {
            return _invertCamX ? -1 : 1;
        }
    }
    public int invertCamYModifier
    {
        get
        {
            return _invertCamY ? -1 : 1;
        }
    }


    [Range(0, 1)]
    [SerializeField] private float _masterVolume = 1.0f;
    public float masterVolume
    {
        get { return _masterVolume; }
        set { SetMasterVolume(value); }
    }

    public void SetMasterVolume(float volume)
    {
        // Clamp the value.
        _masterVolume = Mathf.Clamp01(volume);
        AudioListener.volume = _masterVolume;
    }
}
