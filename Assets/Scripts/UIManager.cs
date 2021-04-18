/*****************************************************************************
// File Name :         UIManager.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject winPanel;

    [SerializeField] private Animator doubleJumpAnim;
    [SerializeField] private Animator dashAnim;

    private void OnEnable()
    {
        EventManager.OnEnemyHit += UpdateScoreText;
        EventManager.OnGamePause += TogglePauseMenu;
        EventManager.OnGameWin += ShowWinPanel;

        EventManager.OnPlayerJump += PlayJumpAnim;
        EventManager.OnPlayerDash += PlayDashAnim;
    }

    private void OnDisable()
    {
        EventManager.OnEnemyHit -= UpdateScoreText;
        EventManager.OnGamePause -= TogglePauseMenu;
        EventManager.OnGameWin -= ShowWinPanel;

        EventManager.OnPlayerJump -= PlayJumpAnim;
        EventManager.OnPlayerDash -= PlayDashAnim;
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

    private void PlayJumpAnim(bool maxJumps)
    {

    }

    private void PlayDashAnim()
    {

    }
}