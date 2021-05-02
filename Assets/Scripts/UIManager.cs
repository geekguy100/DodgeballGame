/*****************************************************************************
// File Name :         UIManager.cs
// Author :            Kyle Grenier
// Creation Date :     04/18/2021
//
// Brief Description : Manages updating and displaying UI in the game.
*****************************************************************************/
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Timer timer;
    [SerializeField] private Image lockOnIcon;

    private void OnEnable()
    {
        EventManager.OnEnemyHit += UpdateScoreText;
        EventManager.OnGamePause += TogglePauseMenu;
        EventManager.OnGameWin += ShowWinPanel;
        EventManager.OnPlayerToggleLockOn += ToggleLockOn;

        if (timer != null)
            timer.OnTimerChange += UpdateTimerText;
    }

    private void OnDisable()
    {
        EventManager.OnEnemyHit -= UpdateScoreText;
        EventManager.OnGamePause -= TogglePauseMenu;
        EventManager.OnGameWin -= ShowWinPanel;

        if (timer != null)
            timer.OnTimerChange -= UpdateTimerText;
    }

    private void Start()
    {
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = GameStats.EnemiesHit + " / " + GameStats.TotalEnemies;
    }

    private void TogglePauseMenu(bool paused)
    {
        pauseMenu.SetActive(paused);
    }

    private void ShowWinPanel()
    {
        winPanel.SetActive(true);
        StartCoroutine(DeactivateAfterTime(winPanel, 5f));
    }

    private IEnumerator DeactivateAfterTime(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
    }

    private void UpdateTimerText(float value)
    {
        timerText.text = string.Format("{0:00.00}s", value);
    }

    private void ToggleLockOn(bool enabled)
    {
        if (enabled)
            lockOnIcon.color = Color.white;
        else
            lockOnIcon.color = Color.black;
    }
}