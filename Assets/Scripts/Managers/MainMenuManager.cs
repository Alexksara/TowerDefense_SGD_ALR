using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuManager : MenuManager
{
    [SerializeField] private InputAction menuInputs;

    private void OnEnable()
    {
        menuInputs.Enable();
        menuInputs.performed += ShowMainMenu;
    }
    private void OnDisable()
    {
        menuInputs.performed -= ShowMainMenu;
        menuInputs.Disable();
    }

    private void ShowMainMenu(InputAction.CallbackContext context)
    {
        SwitchToSecondary();
    }
}
