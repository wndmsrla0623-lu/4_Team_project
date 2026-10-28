using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHp = 3;
    public int currentHp;
    public Slider hpSlider;
    public GameOverUI gameOverUI;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHp = maxHp;
        UpdateBar();

    }

    public void TakeDamage(int damage)
    {
        if (currentHp <= 0) return;

        currentHp -= damage;
        Debug.Log(name + " HP: " + currentHp);
        UpdateBar();

        if (currentHp <= 0) Die();
    }

    void UpdateBar()
    {
        Debug.Log(name + " Current HP: " + currentHp + " / Max HP: " + maxHp);

        if (hpSlider != null)
            hpSlider.value = (float)currentHp / maxHp;
    }




    void Die()
    {
        if (CompareTag("Player"))
        {
            Debug.Log("게임 오버");

            if (gameOverUI != null)
            {
                gameOverUI.ShowGameOver();
            }
            else
            {
                Time.timeScale = 0f;
            }
        }
        else
        {
            WaveManager waveManager =
                FindFirstObjectByType<WaveManager>();

            if (waveManager != null)
            {
                // 적 처치 시 점수 추가
                waveManager.AddScore(waveManager.scorePerKill);

                // 웨이브 처치 수 증가
                waveManager.EnemyKilled();
            }

            PlayerLevelSystem levelSystem =
                FindFirstObjectByType<PlayerLevelSystem>();

            if (levelSystem != null)
            {
                // 경험치 1 추가
                levelSystem.AddExp(1);
            }

            Destroy(gameObject);
        }
    }

}

