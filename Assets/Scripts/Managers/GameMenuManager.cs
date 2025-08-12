using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenuManager : MenuManager
{
    [SerializeField] private TextMeshProUGUI m_winText;
    [SerializeField] private TextMeshProUGUI m_loseText;
    [SerializeField] private TextMeshProUGUI m_playerProgress;
    [SerializeField] private GameObject m_upgradeUI;
    [SerializeField] private Image m_upgradeImage;

    [SerializeField] private GameObject m_nextLevelButton;
    [SerializeField] private GameObject m_restartLevelButton;
    [SerializeField] private GameObject m_startWaveButton;
    //[SerializeField] private TextMeshProUGUI m
    public void WinMenu()
    {
        PlayerProgress();
        HideUpgradeMenu();
        m_nextLevelButton.SetActive(true);
        m_restartLevelButton.SetActive(false);
        m_winText.enabled = true;
        m_loseText.enabled = false;
    }

    public void LoseMenu()
    {
        PlayerProgress();
        HideUpgradeMenu();
        m_nextLevelButton.SetActive(false);
        m_restartLevelButton.SetActive(true);
        m_winText.enabled = false;
        m_loseText.enabled = true;
    }

    public void Restart()
    {
        Debug.Log("trying to restart");
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(GameManager.Instance.currentLevel);
    }

    public void NextLevel()
    {
        GameManager.Instance.IncrimentLevel();
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(GameManager.Instance.currentLevel);
        
    }

    public void ShowUpgradeMenu(Sprite towerImage)
    {
        m_upgradeImage.sprite = towerImage;
        m_upgradeUI.SetActive(true);
    }
    public void HideUpgradeMenu()
    {
        m_upgradeUI.SetActive(false);
    }

    public void HideStartWave()
    {
        m_startWaveButton.SetActive(false);
    }

    private void PlayerProgress()
    {
        m_playerProgress.text = $"You defeated {GameManager.Instance.enemiesKilled} enemies! \nYou compleated {GameManager.Instance.wavesCompleted} waves!";
    }

}
