
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TMP_Text gameOverText;
    public TMP_Text finalScoreText;
    public TMP_Text finalWaveText;

    public string mainMenuSceneName = "Main";

    private WaveManager waveManager;

    void Start()
    {
        Time.timeScale = 1f;

        waveManager = FindFirstObjectByType<WaveManager>();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    public void ShowGameOver()
    {
        if (waveManager == null)
            waveManager = FindFirstObjectByType<WaveManager>();

        if (waveManager != null)
        {
            if (finalScoreText != null)
            {
                finalScoreText.text =
                    "최종 점수: " + waveManager.score;
            }
            else
            {
                Debug.LogError("Final Score Text가 연결되지 않았어!");
            }

            if (finalWaveText != null)
            {
                finalWaveText.text =
                    "도달 웨이브: " + waveManager.currentWave;
            }
            else
            {
                Debug.LogError("Final Wave Text가 연결되지 않았어!");
            }
        }
        else
        {
            Debug.LogError("WaveManager를 찾을 수 없어!");
        }

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
