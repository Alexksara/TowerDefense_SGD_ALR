using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Health playerHealth;
    public int currentLevel = 1;
    public int enemiesKilled = 0;
    public int wavesCompleted = 0;

    [SerializeField] private GameMenuManager gameMenuManager;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private int currentMoney = 0;


    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource soundSource;

    [SerializeField] private TowerPlaceManager towerPlaceManager;
    [SerializeField] private TowerUpgradeManager towerUpgradeManager;

    private const string m_masterVolumePrefName = "Master Volume";
    private const string m_musicVolumePrefName = "Music Volume";
    private const string m_soundVolumePrefName = "Sound Volume";

    

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        playerHealth = GetComponent<Health>();
        AddMoney(0);
        LoadVolumeSettings();
    }

    public void AddMoney(int m)
    {
        currentMoney += m;
        moneyText.text = $"${currentMoney}";
    }

    public int GetMoney()
    {
        return currentMoney;
    }

    public bool DoIHaveSufficientMoney(int cost)
    {
        if(currentMoney >= cost)
        {
            return true;
        }
        return false;
    }

    public void GameLoss()
    {
        gameMenuManager.LoseMenu();
        DisableGameplayInputs();
        gameMenuManager.SwitchToSecondary();
        Time.timeScale = 0;
    }

    public void GameWon()
    {
        gameMenuManager.WinMenu();
        DisableGameplayInputs();
        gameMenuManager.SwitchToSecondary();
        Time.timeScale = 0;
    }

    private void DisableGameplayInputs()
    {
        towerPlaceManager.enabled = false;
        towerUpgradeManager.enabled = false;
    }

    public void IncrimentLevel()
    {
        currentLevel++;
    }

    private void LoadVolumeSettings()
    {
        AudioListener.volume = PlayerPrefs.GetFloat(m_masterVolumePrefName);
        musicSource.volume = PlayerPrefs.GetFloat(m_musicVolumePrefName);
        soundSource.volume = PlayerPrefs.GetFloat(m_soundVolumePrefName);
    }

    public void PlaySound()
    {
        soundSource.Play();
    }

    public void IncrimentEnemiesKilled()
    {
        enemiesKilled++;
    }

    public void IncrementWavesCompleted()
    {
        wavesCompleted++;
    }
}
