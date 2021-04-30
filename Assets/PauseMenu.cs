/*****************************************************************************
// File Name :         PauseMenu.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Toggle invertCamX;
    [SerializeField] private Toggle invertCamY;

    private void Awake()
    {
        invertCamX.onValueChanged.AddListener(Settings.s.InvertCamX);
        invertCamY.onValueChanged.AddListener(Settings.s.InvertCamY);
    }

    private void Start()
    {
        invertCamX.isOn = Settings.s.invertCamX;
        invertCamY.isOn = Settings.s.invertCamY;
    }
}
