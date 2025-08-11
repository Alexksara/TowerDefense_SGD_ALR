using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenuManager : MenuManager
{
    [SerializeField] private TextMeshProUGUI m_winText;
    [SerializeField] private TextMeshProUGUI m_loseText;

    [SerializeField] private Button m_nextLevelButton;
    [SerializeField] private Button m_restartLevelButton;
    //[SerializeField] private TextMeshProUGUI m
    public void WinMenu()
    {
        m_nextLevelButton.enabled = true;
        m_restartLevelButton.enabled = false;
        m_winText.enabled = true;
        m_loseText.enabled = false;
    }

    public void LoseMenu()
    {
        m_nextLevelButton.enabled = false;
        m_restartLevelButton.enabled = true;
        m_winText.enabled = false;
        m_loseText.enabled = true;
    }

    public void Restart()
    {
        SceneManager.SetActiveScene(SceneManager.GetActiveScene());
    }

    public void NextLevel()
    {
        GameManager.Instance.IncrimentLevel();
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(GameManager.Instance.currentLevel));
        
    }

}
