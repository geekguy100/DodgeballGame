/*****************************************************************************
// File Name :         UIManager.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject pauseMenu;

    private void OnEnable()
    {
        EventManager.OnEnemyHit += UpdateScoreText;
        EventManager.OnGamePause += TogglePauseMenu;
    }

    private void OnDisable()
    {
        EventManager.OnEnemyHit -= UpdateScoreText;
        EventManager.OnGamePause -= TogglePauseMenu;
    }

    private void Start()
    {
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Targets Remaining: " + GameStats.EnemiesHit + " / " + GameStats.TotalEnemies;
    }

    private void TogglePauseMenu(bool paused)
    {
        pauseMenu.SetActive(paused);
    }
}