
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerLevelSystem : MonoBehaviour
{
    [Header("캐릭터 이미지")]
    public Image characterPortrait;
    public Sprite level1Image;
    public Sprite level2Image;
    public Sprite level4Image;

    public int level = 1;
    public int currentExp = 0;
    public int requiredExp = 5;

    public Slider expSlider;
    public TMP_Text levelText;
    public TMP_Text expText;

    public GameObject skillPanel;
    public TMP_Text skillTitle;

    public PlayerShooter shooter;
    public PlayerController playerController;

    bool isChoosingSkill = false;

    void Start()
    {
        Time.timeScale = 1f;

        // 플레이어에 붙어 있는 컴포넌트 자동 연결
        if (shooter == null)
            shooter = FindFirstObjectByType<PlayerShooter>();

        if (playerController == null)
            playerController = FindFirstObjectByType<PlayerController>();

        // 강화 패널 숨기기
        if (skillPanel != null)
            skillPanel.SetActive(false);

        // 게임 시작 시 마우스 잠금
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        UpdateUI();
    }

    public void AddExp(int amount)
    {
        if (isChoosingSkill) return;

        currentExp += amount;

        if (currentExp >= requiredExp)
        {
            currentExp -= requiredExp;
            level++;
            requiredExp += 3;

            isChoosingSkill = true;

            // 게임 일시 정지
            Time.timeScale = 0f;

            // 마우스 잠금 해제 및 표시
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // 강화 패널 표시
            if (skillPanel != null)
                skillPanel.SetActive(true);
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        if (levelText != null)
            levelText.text = "LV. " + level;

        if (expText != null)
            expText.text = currentExp + " / " + requiredExp;

        if (expSlider != null)
        {
            expSlider.maxValue = requiredExp;
            expSlider.value = currentExp;
        }

        if (skillTitle != null)
            skillTitle.text = "LEVEL UP!";

        if (characterPortrait != null)
        {
            if (level >= 4)
                characterPortrait.sprite = level4Image;
            else if (level >= 2)
                characterPortrait.sprite = level2Image;
            else
                characterPortrait.sprite = level1Image;
        }
    }

    public void UpgradeDamage()
    {
        if (shooter != null && shooter.bulletPrefab != null)
        {
            Bullet bullet =
                shooter.bulletPrefab.GetComponent<Bullet>();

            if (bullet != null)
                bullet.damage += 1;
        }

        FinishSkillSelection();
    }

    public void UpgradeAttackSpeed()
    {
        if (shooter != null)
        {
            shooter.fireCooldown =
                Mathf.Max(0.05f, shooter.fireCooldown * 0.8f);
        }

        FinishSkillSelection();
    }

    public void UpgradeMoveSpeed()
    {
        if (playerController != null)
            playerController.moveSpeed += 1f;

        FinishSkillSelection();
    }

    void FinishSkillSelection()
    {
        // 강화 패널 숨기기
        if (skillPanel != null)
            skillPanel.SetActive(false);

        isChoosingSkill = false;

        // 게임 재개
        Time.timeScale = 1f;

        // 마우스 다시 잠그기
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        UpdateUI();
    }

}
