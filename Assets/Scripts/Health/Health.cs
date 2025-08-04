using UnityEngine;
using System;

public class Health : MonoBehaviour
{

    public event Action<int, int> OnHealthChanged;

    [SerializeField] private int maxHealth = 20;
    private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        TakeDamage(0);
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }    

    

    public void TakeDamage(int damageamount)
    {
        
        currentHealth = Mathf.Max(currentHealth - damageamount, 0);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        
        if(IsDead())
        {
            GameManager.Instance.GameLoss();
        }
            Debug.Log($"Current Health: {currentHealth}");
    }
}
