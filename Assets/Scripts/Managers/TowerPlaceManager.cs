using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TowerPlaceManager : MonoBehaviour
{
    public Camera MainCamera;
    public LayerMask TileLayer;
    public InputAction PlaceTowerAction;

    [SerializeField] private bool isPlacingTower = false;
    [SerializeField] private bool isMouseOnTile = false;
    [SerializeField] private float towerPlacementHeightOffset = .2f;
    private GameObject currentTowerPrefabToSpawn;
    private GameObject towerPreview;
    private Vector3 towerPlacementPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlacingTower)
        {
            Ray ray = MainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hitInfo,Mathf.Infinity,TileLayer))
            {
                towerPlacementPosition =  hitInfo.transform.position + Vector3.up * towerPlacementHeightOffset;
                towerPreview.transform.position = towerPlacementPosition;

                isMouseOnTile = true;
                towerPreview.SetActive(true);
            }
            else
            {
                isMouseOnTile = false;
                towerPreview.SetActive(false);
                
            }
        }
    }

    private void OnEnable()
    {
        PlaceTowerAction.Enable();
        PlaceTowerAction.performed += OnPlaceTower;
    }
    private void OnDisable()
    {
        PlaceTowerAction.performed -= OnPlaceTower;
        PlaceTowerAction.Disable();
    }

    public void StartPlacingTower(GameObject towerPrefab)
    {
        if(currentTowerPrefabToSpawn != towerPrefab)
        {
            currentTowerPrefabToSpawn = towerPrefab;
            if (GameManager.Instance.DoIHaveSufficientMoney(currentTowerPrefabToSpawn.GetComponentInChildren<Tower>().CostToPlace))
            {
                isPlacingTower = true;
                
                if (towerPreview != null) // destroy previous preview 
                {
                    Destroy(towerPreview);
                }
                towerPreview = Instantiate(currentTowerPrefabToSpawn);
                towerPreview.GetComponentInChildren<Tower>().enabled = false;
            }

            
        }
    }

    private void OnPlaceTower(InputAction.CallbackContext context)
    {
        if(isPlacingTower && isMouseOnTile)
        {
            Instantiate(currentTowerPrefabToSpawn, towerPlacementPosition, Quaternion.identity);
            Destroy(towerPreview);
            isPlacingTower = false;
            GameManager.Instance.AddMoney(-currentTowerPrefabToSpawn.GetComponentInChildren<Tower>().CostToPlace);
            currentTowerPrefabToSpawn = null;

        }
        
    }

    public bool IsPlacingTower 
    { get 
        {
            return isPlacingTower; 
        } 
    }
}
