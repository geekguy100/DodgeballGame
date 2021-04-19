using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class VolumeControl : MonoBehaviour
{
    //Should adjust master volume
    public void AdjustVolume(float newVolume)
    {
        AudioListener.volume = newVolume;
    }
}
