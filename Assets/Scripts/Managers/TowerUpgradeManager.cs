using NUnit.Framework;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;



[System.Serializable]
public struct UpgradeData
{
    public GameObject lvlOneTower;
    public GameObject lvlTwoTower;
    public GameObject lvlThreeTower;
    public Sprite towerImage;
}

public enum Towers
{
    BallistaTower,
    CannonTower,
    CatapultTower

}


public class TowerUpgradeManager : MonoBehaviour
{
    private const string m_raycastTowerName = "Tower";
    private const int m_towerMask = 7;
    [SerializeField] private InputAction m_upgradeInputs;
    [SerializeField] private TowerPlaceManager m_towerPlaceManager;
    [SerializeField] private Camera m_mainCamera;
    [SerializeField] private GameMenuManager m_menuManager;
    [SerializeField] private GameObject m_upgradeMenu;

    [SerializeField] private List<UpgradeData> m_towerUpgradeData;

    private bool m_isUpgrading = false;
    private Towers m_towerTypeToUpgrade;
    private Tower m_selectedTowerToUpgrade;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void OnEnable()
    {
        m_upgradeInputs.Enable();
        m_upgradeInputs.performed += OnUpgradeTower;
    }
    private void OnDisable()
    {
        m_upgradeInputs.performed -= OnUpgradeTower;
        m_upgradeInputs.Disable();
    }

    private void OnUpgradeTower(InputAction.CallbackContext context)
    {
        Debug.Log("Clicked");
        if (!m_towerPlaceManager.IsPlacingTower && !m_isUpgrading)
        {
            Ray ray = m_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
            {
                
                m_selectedTowerToUpgrade = hitInfo.collider.GetComponent<Tower>();
                if(m_selectedTowerToUpgrade != null)
                {
                    m_isUpgrading = true;
                    switch (m_selectedTowerToUpgrade)
                    {
                        case BallistaTower: Debug.Log("ballista"); m_towerTypeToUpgrade = Towers.BallistaTower; break;
                        case CannonTower: Debug.Log("Cannon");m_towerTypeToUpgrade = Towers.CannonTower; break;
                        case CatapultTower: Debug.Log("Catapult");m_towerTypeToUpgrade = Towers.CatapultTower; break;
                    }
                        m_menuManager.ShowUpgradeMenu(m_towerUpgradeData[((int)m_towerTypeToUpgrade)].towerImage);
                }


            }

        }
    }

    public void UpgradeSelectedTower()
    {
        Debug.Log("upgrading");
        if (GameManager.Instance.DoIHaveSufficientMoney(m_selectedTowerToUpgrade.CostToUpgrade))
        {
            switch (m_selectedTowerToUpgrade.CurrentLevel)
            {
                case 1:
                    Instantiate(m_towerUpgradeData[(int)m_towerTypeToUpgrade].lvlTwoTower, m_selectedTowerToUpgrade.transform.position, m_selectedTowerToUpgrade.transform.rotation);
                    break;
                case 2:
                    Instantiate(m_towerUpgradeData[(int)m_towerTypeToUpgrade].lvlThreeTower, m_selectedTowerToUpgrade.transform.position, m_selectedTowerToUpgrade.transform.rotation);
                    break;

                default: Debug.Log("Unable to upgrade"); break;
            }
            GameManager.Instance.AddMoney(-m_selectedTowerToUpgrade.CostToUpgrade);
            Destroy(m_selectedTowerToUpgrade.gameObject);
            StopUpgrading();
        }

        return;
    }

    public bool IsUpgrading
    {get
        {
            return m_isUpgrading;
        }
    }

    public void StopUpgrading()
    {
        m_isUpgrading = false;
    }
}
