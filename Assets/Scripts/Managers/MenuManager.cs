using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Summary 
    // primary is what is enabled on load, secondary is disabled
    
    [SerializeField] protected GameObject m_primaryScreen;
    [SerializeField] protected GameObject m_secondaryScreen;

    [SerializeField] protected bool m_primaryEnabled = true;
    [SerializeField] protected bool m_secondaryEnabled = false;

    


    public void SwitchToSecondary()
    {
        m_primaryScreen.SetActive(false);
        m_primaryEnabled = false;
        m_secondaryScreen.SetActive(true);
        m_secondaryEnabled = true;
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
