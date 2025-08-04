using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private GameObject m_titleScreen;
    [SerializeField] private GameObject m_mainMenu;

    [SerializeField] private bool m_titleEnabled = true;
    [SerializeField] private bool m_mainMenuEnabled = false;

    [SerializeField] private InputAction menuInputs;

    private void OnEnable()
    {
        menuInputs.Enable();
        menuInputs.performed += ToMainMenu;
    }
    private void OnDisable()
    {
        menuInputs.performed -= ToMainMenu;
        menuInputs.Disable();
    }

    private void ToMainMenu(InputAction.CallbackContext context)
    {
        DisableTitle();
    }

    public void DisableTitle()
    {
        m_titleScreen.SetActive(false);
        m_titleEnabled = false;
        EnableMainMenu();
    }

    public void EnableMainMenu()
    {
        m_mainMenu.SetActive(true);
        m_mainMenuEnabled = true;
    }

    public void DisableMainMenu()
    {
        m_mainMenu.SetActive(false);
        m_mainMenuEnabled = false;
    }


    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
