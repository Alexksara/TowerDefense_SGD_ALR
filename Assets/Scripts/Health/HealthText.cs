using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthText : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] TextMeshProUGUI text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
       if (health != null)
       {
           health.OnHealthChanged += UpdateHealthText;
       }
    }

    private void UpdateHealthText(int currentHealth, int maxHealth)
    {
        text.text = ($"{currentHealth}/{ maxHealth}");
    }
}
