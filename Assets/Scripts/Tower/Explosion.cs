using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private int M_damage;
    [SerializeField] private float M_destroyTimer = 3f;
    [SerializeField] private float m_explosionTimer = .4f;
    private bool M_exploded = false;

    //Summary
    //all enemies within the explosion radius take damage equal to the damage value and the explosion is destroyed
    private void Start()
    {
        Destroy(this.gameObject, M_destroyTimer);
    }

    private void Update()
    {
        m_explosionTimer -= Time.deltaTime;
        if(m_explosionTimer <= 0 )
        {
            this.GetComponent<Collider>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(M_damage);
            if(!M_exploded)
            {
                M_exploded=true;
            }
        }
    }


}
