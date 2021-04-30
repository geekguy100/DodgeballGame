using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VolumeControl : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    private void Start()
    {
        volumeSlider.value = Settings.s.masterVolume; 
    }

    //Should adjust master volume
    public void AdjustVolume(float newVolume)
    {
        Settings.s.masterVolume = newVolume;
    }
}
