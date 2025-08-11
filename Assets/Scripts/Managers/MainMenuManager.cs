using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MainMenuManager : MenuManager
{
    private const string m_masterVolumePrefName = "Master Volume";
    private const string m_musicVolumePrefName = "Music Volume";
    private const string m_soundVolumePrefName = "Sound Volume";
    [SerializeField] private InputAction menuInputs;
    [SerializeField] private GameObject optionsMenu;

    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider soundVolumeSlider;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource soundSource;

    private void Awake()
    {
        masterVolumeSlider.value = PlayerPrefs.GetFloat(m_masterVolumePrefName);
        musicVolumeSlider.value = PlayerPrefs.GetFloat(m_musicVolumePrefName);
        soundVolumeSlider.value = PlayerPrefs.GetFloat(m_soundVolumePrefName);
        ChangeMasterVolume();
        ChangeMusicVolume();
        ChangeSoundVolume();
    }

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

    public void ShowOptions()
    {
        m_secondaryScreen.SetActive(false);
        optionsMenu.SetActive(true);
    }
    public void HideOptions()
    {
        m_secondaryScreen.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void ChangeMasterVolume()
    {
        AudioListener.volume = masterVolumeSlider.value;
        PlayerPrefs.SetFloat(m_masterVolumePrefName, masterVolumeSlider.value);
    }

    public void ChangeMusicVolume()
    {
        musicSource.volume = musicVolumeSlider.value;
        PlayerPrefs.SetFloat(m_musicVolumePrefName, musicVolumeSlider.value);
    }
    public void ChangeSoundVolume()
    {
        soundSource.volume = soundVolumeSlider.value;
        PlayerPrefs.SetFloat(m_soundVolumePrefName, soundVolumeSlider.value);
    }

}
