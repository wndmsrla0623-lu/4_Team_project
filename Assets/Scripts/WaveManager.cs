
using UnityEngine;
using TMPro;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    public int currentWave = 1;
    public int baseKillsNeeded = 3;
    public int increasePerWave = 2;

    public int currentKills = 0;
    public int killsNeeded;

    public float waveDelay = 5f;

    public TMP_Text waveText;
    public TMP_Text remainingText;
    public TMP_Text announcementText;

    EnemySpawner spawner;
    bool isWaiting = false;

    void Start()
    {
        killsNeeded = baseKillsNeeded;
        spawner = FindFirstObjectByType<EnemySpawner>();

        UpdateUI();

        if (announcementText != null)
            announcementText.text = "";
    }

    public void EnemyKilled()
    {
        if (isWaiting) return;

        currentKills++;
        UpdateUI();

        if (currentKills >= killsNeeded)
        {
            StartCoroutine(NextWave());
        }
    }

    IEnumerator NextWave()
    {
        isWaiting = true;

        // 적 생성 중단
        if (spawner != null)
            spawner.SetSpawning(false);

        // 남아 있는 몬스터 전부 제거
        GameObject[] enemies =
            GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        // 5초 동안 대기
        for (int i = 5; i > 0; i--)
        {
            if (announcementText != null)
                announcementText.text =
                    "WAVE CLEAR!\n다음 웨이브까지 " + i + "초";

            yield return new WaitForSeconds(1f);
        }

        // 다음 웨이브 시작
        currentWave++;
        currentKills = 0;

        killsNeeded = baseKillsNeeded
            + (currentWave - 1) * increasePerWave;

        UpdateUI();

        if (announcementText != null)
            announcementText.text = "WAVE " + currentWave;

        // 적 생성 재개
        if (spawner != null)
            spawner.SetSpawning(true);

        isWaiting = false;

        yield return new WaitForSeconds(2f);

        if (announcementText != null)
            announcementText.text = "";
    }

    void UpdateUI()
    {
        if (waveText != null)
            waveText.text = "WAVE " + currentWave;

        if (remainingText != null)
        {
            int remaining = Mathf.Max(0, killsNeeded - currentKills);

            remainingText.text =
                "남은 처치 수: " + remaining + " / " + killsNeeded;
        }
    }
}
