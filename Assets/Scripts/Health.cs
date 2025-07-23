using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 20;
    private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public bool IsDead()
    {
        return currentHealth > 0;
    }    

    public void TakeDamage(int damageamount)
    {
        if(currentHealth > 0)
        { 
            currentHealth = Mathf.Max(currentHealth - damageamount, 0);
        }
        Debug.Log($"Current Health: {currentHealth}");
    }
}
