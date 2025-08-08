using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Health playerHealth;
    public int currentLevel = 1;

    [SerializeField] private GameMenuManager gameMenuManager;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private int currentMoney = 0;

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

    public bool CompareMoney(int cost)
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
        gameMenuManager.SwitchToSecondary();
    }

    public void GameWon()
    {
        gameMenuManager.WinMenu();
        gameMenuManager.SwitchToSecondary();
    }

    public void IncrimentLevel()
    {
        currentLevel++;
    }
}
